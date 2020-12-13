using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using CarScripts;
using DebugScripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CircuitController : MonoBehaviour
{
    public GameObject[] waypoints;
    public List<UpdateCheckpoint> mandatoryCheckpointList;
    public Transform[] startingPosList;

    public List<AiPath> aiPaths;

    private bool startTimer;

    private float raceTime;

    private int endCarCount;

    [Header("Debug Settings")]
    [SerializeField] private bool monitoring = false;
    private enum CarModel {Furgo, Sedan, Police, Taxi}
    [SerializeField] private CarModel carModel;
    [SerializeField] private bool writeMedian;
    private float RTime
    {
        get { return raceTime; }
        set
        {
            if (Mathf.Abs(raceTime - value) < float.Epsilon) return;
            if (!monitoring) OnTimeChangeHandler?.Invoke(raceTime = value);
            else raceTime = value;
        }
    }

    public delegate void OnTimeChangeDelegate(float newVal);

    public static event OnTimeChangeDelegate OnTimeChangeHandler;

    public int maxLaps;
    public int TrialLaps;

    private void OnEnable()
    {
        StartRace.onRaceStart += StartTimer;
        GameManager.OnFinishRaceHandler += StopTimer;
    }

    private void OnDisable()
    {
        StartRace.onRaceStart -= StartTimer;
        GameManager.OnFinishRaceHandler -= StopTimer;
    }

    private void StartTimer()
    {
        startTimer = true;
        endCarCount = Laderboard.getCarCount();
    }

    private void StopTimer(int _id)
    {
        if(_id == 0) startTimer = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        raceTime = 0;

        SetNextWaypointAndId();
        SetAiCarPath();
        SpawnCars();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer) 
        {
            if(monitoring && RTime > 300f) AiMonitor.RestartLevel();
            RTime += Time.deltaTime;
        }
    }

    public Vector2 checkLap(Car _car)
    {
        bool goodLap = true;
        if (_car.checkpoints.Count == mandatoryCheckpointList.Count)
        {
            for (int i = 0; i < mandatoryCheckpointList.Count; i++)
            {
                if (!mandatoryCheckpointList[i].Equals(_car.checkpoints[i]))
                {
                    goodLap = false;
                    break;
                }
            }
        }
        else
        {
            goodLap = false;
        }

        if (goodLap)
        {
            return new Vector2(1, raceTime);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    private void DrawGizmos(bool _selected)
    {
        if (!_selected) return;
        if (waypoints.Length > 1)
        {
            Vector3 prev = waypoints[0].transform.position;
            for (int i = 1; i < waypoints.Length; i++)
            {
                Vector3 next = waypoints[i].transform.position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, waypoints[0].transform.position);
        }
        if (aiPaths.Count > 1)
        {
            Gizmos.color = Color.magenta;
            Vector3 prev = aiPaths[0].transform.position;
            for (int i = 1; i < aiPaths.Count; i++)
            {
                Vector3 next =aiPaths[i].transform.position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, aiPaths[0].transform.position);
        }
    }

    private void SetNextWaypointAndId()
    {
        UpdateCheckpoint prev = waypoints[0].GetComponent<UpdateCheckpoint>();
        prev.id = 0;
        for (int i=1; i < waypoints.Length; i++)
        {
            prev.nextCheckpoint = waypoints[i].GetComponent<UpdateCheckpoint>();
            prev = waypoints[i].GetComponent<UpdateCheckpoint>();
            prev.id = i;
        }
        prev.nextCheckpoint = waypoints[0].GetComponent<UpdateCheckpoint>();
    }

    private void SetAiCarPath()
    {
        AiPath prev = aiPaths[0];
        for (int i = 1; i < aiPaths.Count-2; i++)
        {
            prev.nextPaths = new AiPath[3];
            prev.nextPaths[0] = aiPaths[i];
            prev.nextPaths[1] = aiPaths[i+1];
            prev.nextPaths[2] = aiPaths[i+2];
            prev = aiPaths[i];
        }
        prev.nextPaths = new AiPath[3];
        prev.nextPaths[0] = aiPaths[aiPaths.Count - 2];
        prev.nextPaths[1] = aiPaths[aiPaths.Count - 1];
        prev.nextPaths[2] = aiPaths[0];
        prev = aiPaths[aiPaths.Count - 2];
        prev.nextPaths = new AiPath[3];
        prev.nextPaths[0] = aiPaths[aiPaths.Count - 1];
        prev.nextPaths[1] = aiPaths[0];
        prev.nextPaths[2] = aiPaths[1];
        prev = aiPaths[aiPaths.Count - 1];
        prev.nextPaths = new AiPath[3];
        prev.nextPaths[0] = aiPaths[0];
        prev.nextPaths[1] = aiPaths[1];
        prev.nextPaths[2] = aiPaths[2];
    }

    public void FinishRace(int _id)
    {
        GameManager.GameInstance.EndRaceCall(_id);
        endCarCount--;
        if(endCarCount == 0) startTimer = false;
    }

    private void SpawnCars()
    {
        if (!monitoring)
        {
            GameManager.GameInstance.carFininshRace = new List<bool>();
            //Debug.Log(Laderboard.getCarCount());
            if (Laderboard.getCarCount() == 0)
            {
                int playerCar = PlayerPrefs.GetInt("CurrentCar");
                GameObject playerCarP;
                if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
                {
                    playerCarP = Instantiate(GameManager.GameInstance.playerPrefabList[playerCar],
                        startingPosList[1].position,
                        startingPosList[1].rotation);
                }
                else
                {
                    playerCarP = Instantiate(GameManager.GameInstance.playerPrefabList[playerCar],
                        startingPosList[5].position,
                        startingPosList[5].rotation);
                }
                playerCarP.GetComponent<CarControllerV2>().checkpoint = waypoints[0].GetComponent<UpdateCheckpoint>();
                playerCarP.GetComponent<CarControllerV2>().id = Laderboard.RegisterCar("Player", playerCar, -1);

                GameManager.GameInstance.carFininshRace.Add(false);

                if (GameManager.GameInstance.mode != GameManager.GameMode.TimeTrial)
                {

                    float diffScale = 1f;
                    if (GameManager.GameInstance.diff == GameManager.Difficulty.Easy) diffScale = 0.75f;

                    int carNum;
                    for (int i = 0; i < 5; i++)
                    {
                        carNum = Random.Range(0, 4);
                        GameObject aiCar = Instantiate(GameManager.GameInstance.aiPrefabList[carNum],
                            startingPosList[i].position,
                            startingPosList[i].rotation);
                        aiCar.GetComponent<AiCarController>().checkpoint =
                            waypoints[0].GetComponent<UpdateCheckpoint>();
                        aiCar.GetComponent<AiCarController>().currentAiPath = aiPaths[0];
                        int rc = Random.Range(0, 9);
                        aiCar.GetComponent<AiCarController>().mRenderer
                            .materials[aiCar.GetComponent<AiCarController>().mIndex].SetColor("_BaseColor",
                                GameManager.GameInstance.colors[rc]);
                        aiCar.GetComponent<AiCarController>().id =
                            Laderboard.RegisterCar(GameManager.GameInstance.names[rc][i], carNum, rc);
                        aiCar.GetComponent<AiCarController>().name.text =
                            Laderboard.getCarName(aiCar.GetComponent<AiCarController>().id);
                        aiCar.GetComponent<AiCarController>().SetMaxTorque(diffScale);

                        GameManager.GameInstance.carFininshRace.Add(false);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<int, PlayerStats> player in Laderboard.getCars())
                {
                    if (player.Key == 0)
                    {
                        GameObject playerCarP;
                        if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
                        {
                            playerCarP = Instantiate(GameManager.GameInstance.playerPrefabList[player.Value.carNum],
                                startingPosList[1].position,
                                startingPosList[1].rotation);
                        }
                        else
                        {
                            playerCarP = Instantiate(
                                GameManager.GameInstance.playerPrefabList[player.Value.carNum],
                                startingPosList[startingPosList.Length - 1 - player.Key].position,
                                startingPosList[startingPosList.Length - 1 - player.Key].rotation);
                        }

                        playerCarP.GetComponent<CarControllerV2>().checkpoint =
                            waypoints[0].GetComponent<UpdateCheckpoint>();
                        playerCarP.GetComponent<CarControllerV2>().id = player.Key;
                        
                        GameManager.GameInstance.carFininshRace.Add(false);
                    }
                    else if(GameManager.GameInstance.mode != GameManager.GameMode.TimeTrial)
                    {
                        GameObject aiCar = Instantiate(GameManager.GameInstance.aiPrefabList[player.Value.carNum],
                            startingPosList[startingPosList.Length - 1 - player.Key].position,
                            startingPosList[startingPosList.Length - 1 - player.Key].rotation);
                        aiCar.GetComponent<AiCarController>().checkpoint =
                            waypoints[0].GetComponent<UpdateCheckpoint>();
                        aiCar.GetComponent<AiCarController>().currentAiPath = aiPaths[0];
                        aiCar.GetComponent<AiCarController>().mRenderer
                            .materials[aiCar.GetComponent<AiCarController>().mIndex].SetColor("_BaseColor",
                                GameManager.GameInstance.colors[player.Value.carColor]);
                        aiCar.GetComponent<AiCarController>().id = player.Key;
                        aiCar.GetComponent<AiCarController>().name.text =
                            Laderboard.getCarName(aiCar.GetComponent<AiCarController>().id);
                        
                        GameManager.GameInstance.carFininshRace.Add(false);
                    }
                }
            }
        }
        else
        {
            GameObject aiCar;
            int carNum = 0;
            switch (carModel)
            {
                case CarModel.Furgo:
                    carNum = 0;
                    break;
                case CarModel.Sedan:
                    carNum = 1;
                    break;
                case CarModel.Police:
                    carNum = 2;
                    break;
                case CarModel.Taxi:
                    carNum = 3;
                    break;
            }
            aiCar = Instantiate(GameManager.GameInstance.aiPrefabList[carNum],
                startingPosList[0].position,
                startingPosList[0].rotation);
            aiCar.GetComponent<AiCarController>().checkpoint = waypoints[0].GetComponent<UpdateCheckpoint>();
            aiCar.GetComponent<AiCarController>().currentAiPath = aiPaths[0];
            aiCar.GetComponent<AiCarController>().id =
                Laderboard.RegisterCar("IA", carNum, -1);
            aiCar.GetComponent<AiCarController>().name.text =
                Laderboard.getCarName(aiCar.GetComponent<AiCarController>().id);
            aiCar.GetComponent<AiCarController>().monitoring = true;
            AiMonitor.writeMedian = writeMedian;
            AiMonitor.GetCarTextPath(aiCar.GetComponent<AiCarController>(), carModel.ToString());
            
            GameManager.GameInstance.carFininshRace.Add(false);
        }
    }

    public AiPath GetNextPath(AiPath path)
    {
        return aiPaths[aiPaths.IndexOf(path) + 1];
    }
}
