using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    private CarControllerV2 playerCar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCar) playerCar = FindObjectOfType<CarControllerV2>();
        else transform.position = playerCar.transform.position + new Vector3(0f, 10f, 0f);
    }
}
