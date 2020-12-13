using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarFrontLights : MonoBehaviour
{
    
    [SerializeField] GameObject[] lights;
    [SerializeField] Material material;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("OwlPlainsCircuit"))
        {
            lights[0].SetActive(true);
            lights[1].SetActive(true);

            material.EnableKeyword("_EMISSION");
        }
        else
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
            
            material.DisableKeyword("_EMISSION");
        }
    }
}
