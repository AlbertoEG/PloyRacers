using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public Gradient SpeedGradient;
    private int maxVel;
    [SerializeField] Image velocityTextBack;
    
    private CarControllerV2 playerCar;
    private CircuitController circuit;

    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] TextMeshProUGUI lapTimeText;
    [SerializeField] TextMeshProUGUI lapText;
    [SerializeField] TextMeshProUGUI raceTimeText;
    [SerializeField] TextMeshProUGUI positionText;

    private void OnEnable()
    {
        Car.OnSpeedChangeHandler += VelocityToText;
        CircuitController.OnTimeChangeHandler += SetRaceTime;
    }

    private void OnDisable()
    {
        Car.OnSpeedChangeHandler -= VelocityToText;
        CircuitController.OnTimeChangeHandler -= SetRaceTime; 
    }

    // Start is called before the first frame update
    void Awake()
    {
        maxVel = 300;
        
        circuit = FindObjectOfType<CircuitController>();
        switch (GameManager.GameInstance.mode)
        {
            case GameManager.GameMode.TimeTrial: lapText.text =  "1/" + circuit.TrialLaps;
                break;
            default: lapText.text =  "1/" + circuit.maxLaps;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCar) playerCar = FindObjectOfType<CarControllerV2>();
        else if (Laderboard.GetFinalPos(playerCar.id) == 0) positionText.text = Laderboard.GetPosition(playerCar.id);
        else positionText.text = Laderboard.GetFinalPos(playerCar.id) + "";
    }

    public void VelocityToText(float _v)
    {
        int vel = (int)(_v * 5.75f);
        Color c = SpeedGradient.Evaluate((float)vel / maxVel);
        velocityText.color = c;
        velocityTextBack.color = c;
        velocityText.text = vel + " Km/h";
    }

    public void SetLapTime(float _lapTime)
    {
        lapTimeText.text = Utillities.FormatTime(_lapTime);
        lapTimeText.gameObject.SetActive(true);
        if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
        {
            if (playerCar.lap < circuit.TrialLaps) lapText.text = (playerCar.lap + 1) + "/" + circuit.TrialLaps;
        }
        else
        {
            if (playerCar.lap < circuit.maxLaps) lapText.text = (playerCar.lap + 1) + "/" + circuit.maxLaps;
        }
    }

    public void SetRaceTime(float _t)
    {
        raceTimeText.text = Utillities.FormatTime(_t);
    }
}
