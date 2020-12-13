using System.Collections;
using System.Collections.Generic;
using CarScripts;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    public int id;
    public bool mandatory;
    public bool goal;

    public UpdateCheckpoint nextCheckpoint;

    [SerializeField] CircuitController circuitController;
    [SerializeField] UIController uiController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.parent.gameObject.CompareTag("Car"))
            {
                other.transform.parent.gameObject.GetComponent<Car>().UpdateCheckpoint(this);
            }
        }

        if (goal)
        {
            if (other.CompareTag("CarGoal"))
            {
                if(other.transform.parent.gameObject.TryGetComponent(out CarControllerV2 car)){
                    float lap = car.lap;
                    Vector2 lapAndTime = circuitController.checkLap(car);
                    car.lap += (int) lapAndTime.x;
                    if (lap != car.lap)
                    {
                        float time = lapAndTime.y;
                        if (car.lapTimes.Count != 0)
                        {
                            for (int i = 0; i < car.lapTimes.Count; i++)
                            {
                                time -= (int) car.lapTimes[i];
                            }
                        }

                        car.SetLapTime(time);
                        uiController.SetLapTime(time);
                        car.ResetCheckPoints();
                        if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
                        {
                            if (car.lap == circuitController.TrialLaps) circuitController.FinishRace(car.id);
                        }
                        else
                        {
                            if (car.lap == circuitController.maxLaps) circuitController.FinishRace(car.id);
                        }
                    }
                }
                else if (other.transform.parent.gameObject.TryGetComponent(out AiCarController aiCar))
                {
                    float lap = aiCar.lap;
                    Vector2 lapAndTime = circuitController.checkLap(aiCar);
                    aiCar.lap += (int) lapAndTime.x;
                    if (lap != aiCar.lap)
                    {
                        float time = lapAndTime.y;
                        if (aiCar.lapTimes.Count != 0)
                        {
                            for (int i = 0; i < aiCar.lapTimes.Count; i++)
                            {
                                time -= (int) aiCar.lapTimes[i];
                            }
                        }
                        aiCar.SetLapTime(time);
                        aiCar.ResetCheckPoints();
                        if (aiCar.lap == circuitController.maxLaps) circuitController.FinishRace(aiCar.id);
                    }
                }
            }
        }
    }
}
