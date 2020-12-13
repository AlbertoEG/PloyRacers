using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerUtillities : MonoBehaviour
{

    //INPUTS
    private PlayerInputActions inputActions;
    
    //PLAYER STATS UI COMPONETS
    [SerializeField] private GameObject statsUI;
    [SerializeField] private TextMeshProUGUI namePosText;
    [SerializeField] private TextMeshProUGUI[] timeTexts;
    [SerializeField] private TextMeshProUGUI[] nameLadderTexts;
    [SerializeField] private TextMeshProUGUI[] nameLadderTexts2;
    [SerializeField] private TextMeshProUGUI[] pointLadderTexts;

    //OTHER
    public Animator transition;
    public GameObject pauseMenu;

    public static bool isGamePause;
    
    [SerializeField] private Volume volume;
    private DepthOfField dof;
    
    private AudioSource sound;

    private void OnEnable()
    {
        inputActions.Enable();
        GameManager.OnFinishRaceHandler += StopSound;
        CarControllerV2.OnEndStatsHandler += RaceStats;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        GameManager.OnFinishRaceHandler -= StopSound;
        CarControllerV2.OnEndStatsHandler -= RaceStats;
    }

    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.PauseButton.performed += ctx => PauseGame();

        isGamePause = false;
        
        sound = GetComponent<AudioSource>();
    }
    public void ChangeScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (GameManager.GameInstance.mode == GameManager.GameMode.Versus)
        {
            ReturnToSelector();
        }
        else
        {
            if (GameManager.GameInstance.trackPlayed < 4)
            {
                GameManager.GameInstance.trackPlayed++;
                switch (scene)
                {
                    case "NASCARCircuit":
                        StartCoroutine(LoadLevel("8Circuit"));
                        break;
                    case "8Circuit":
                        StartCoroutine(LoadLevel("OwlPlainsCircuit"));
                        break;
                    case "NoNameCircuit":
                        StartCoroutine(LoadLevel("NASCARCircuit"));
                        break;
                    case "OwlPlainsCircuit":
                        StartCoroutine(LoadLevel("NoNameCircuit"));
                        break;
                }
            }
            else
            {
                GameManager.GameInstance.trackPlayed = 0;

                sound.Play();

                if (Time.timeScale != 1f) Time.timeScale = 1f;

                transition.SetTrigger("ChangeScene");

                SceneManager.LoadScene("PolePosition");
            }
        }
    }

    IEnumerator LoadLevel(string _levelIndex)
    {
        sound.Play();
        
        if(Time.timeScale != 1f) Time.timeScale = 1f; 

        transition.SetTrigger("ChangeScene");

        yield return new WaitForSeconds(1f);

        Laderboard.ClearLaderboard();

        SceneManager.LoadScene(_levelIndex);

        yield return null;
    }

    public void PauseGame()
    {
        sound.Play();
        if(isGamePause)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isGamePause = false;
            
            if (dof)
            {
                dof.gaussianStart.Override(300f);
                dof.gaussianEnd.Override(450f);
            }
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isGamePause = true;
            
            volume = FindObjectOfType<Volume>();
            if (volume.profile.TryGet(out dof))
            {
                dof.gaussianStart.Override(0f);
                dof.gaussianEnd.Override(0f);
            }
        }
    }

    public void QuitGame()
    {
        sound.Play();
        Application.Quit();
    }

    private void RaceStats(CarControllerV2 _car)
    {
        namePosText.text = Laderboard.GetFinalPos(_car.id) + "º Place";
        for (int i=0; i < _car.lapTimes.Count; i++)
        {
            timeTexts[i].text = "Lap " + (i + 1) + " - Time:" + Utillities.FormatTime(_car.lapTimes[i]);
        }
        statsUI.SetActive(true);
        volume = FindObjectOfType<Volume>();
        if (volume.profile.TryGet(out dof))
        {
            dof.gaussianStart.Override(0f);
            dof.gaussianEnd.Override(0f);
        }
        
        LadderStats();
    }

    public void RepeatCircuit()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    public void ReturnToMenu()
    {
        GameManager.GameInstance.trackPlayed = 1;
        Laderboard.resetLadder();
        StartCoroutine(LoadLevel("MainMenu"));
    }
    
    public void ReturnToSelector()
    {
        GameManager.GameInstance.trackPlayed = 0;
        Laderboard.resetLadder();
        StartCoroutine(LoadLevel("TrackSelector"));
    }

    public void SelectTrack(string _track)
    {
        StartCoroutine(LoadLevel(_track));
    }

    public void SelectDifficulty(int _diff)
    {
        if(_diff == 0) GameManager.GameInstance.diff = GameManager.Difficulty.Easy;
        else GameManager.GameInstance.diff = GameManager.Difficulty.Normal;
        StartCoroutine(LoadLevel("CarSelector"));
    }
    
    public void SelectMode(int _mode)
    {
        switch (_mode)
        {
            case 0:
                GameManager.GameInstance.mode = GameManager.GameMode.Career;
                StartCoroutine(LoadLevel("DifficultySelector"));
                break;
            case 1: GameManager.GameInstance.mode = GameManager.GameMode.Versus;
                StartCoroutine(LoadLevel("DifficultySelector"));
                break;
            case 2: GameManager.GameInstance.mode = GameManager.GameMode.TimeTrial;
                StartCoroutine(LoadLevel("CarSelector"));
                break;
            default: Debug.Log("???");
                break;
        }
    }

    public void BackToLastScene()
    {
        if (SceneManager.GetActiveScene().name == "CarSelector")
        {
            if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
                StartCoroutine(LoadLevel("ModeSelector"));
            else StartCoroutine(LoadLevel("DifficultySelector"));
        }
    }

    public void SelectCar(int _car)
    {
        PlayerPrefs.SetInt("CurrentCar", _car);
        if (GameManager.GameInstance.mode == GameManager.GameMode.Career) StartCoroutine(LoadLevel("NASCARCircuit"));
        else StartCoroutine(LoadLevel("TrackSelector"));
    }

    public void LadderStats()
    {
        PlayerStats[] cars = Laderboard.getOrderedCars();
        //Debug.Log(cars.Length + "coches");

        for (int i = 0; i < cars.Length; i++)
        {
            nameLadderTexts[i].text = cars[i].name;
            if(cars[i].id == 0) nameLadderTexts[i].color = Color.white;
        }
        
        cars = Laderboard.getOrderedCarsPoints();
        
        for (int i=0; i < cars.Length; i++)
        {
            nameLadderTexts2[i].text = cars[i].name;
            if(cars[i].id == 0) nameLadderTexts2[i].color = Color.white;
            switch (i)
            {
                case 0: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 1: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 2: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 3: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 4: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 5: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                default: pointLadderTexts[i].text = "???";
                    break;
            }
        }
    }

    private void StopSound(int _id)
    {
        if(_id == 0) sound.Stop();
    }
}
