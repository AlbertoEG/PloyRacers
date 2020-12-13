using System.Collections;
using System.Collections.Generic;
using CarScripts;
using UnityEngine;

public class Podium : MonoBehaviour
{
    [SerializeField] private GameObject[] pCars;
    
    [SerializeField] private Transform[] positions;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerStats[] cars = Laderboard.getOrderedCarsPoints();

        for(int i=0; i< positions.Length; i++)
        {
            if (cars[i].carColor == -1)
            {
                GameObject aiCar = Instantiate(pCars[cars[i].carNum], positions[i].position, positions[i].rotation);
                aiCar.GetComponent<PodiumCar>().id = cars[i].id;
                aiCar.GetComponent<PodiumCar>().name.text = Laderboard.getCarName(aiCar.GetComponent<PodiumCar>().id);
                aiCar.GetComponent<PodiumCar>().name.color = Color.white;
            }
            else
            {
                GameObject aiCar = Instantiate(pCars[cars[i].carNum], positions[i].position, positions[i].rotation);
                aiCar.GetComponent<PodiumCar>().mRenderer.materials[aiCar.GetComponent<PodiumCar>().mIndex].SetColor("_BaseColor",
                    GameManager.GameInstance.colors[cars[i].carColor]);
                aiCar.GetComponent<PodiumCar>().id = cars[i].id;
                aiCar.GetComponent<PodiumCar>().name.text = Laderboard.getCarName(aiCar.GetComponent<PodiumCar>().id);
                aiCar.GetComponent<PodiumCar>().name.color = Color.black;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
