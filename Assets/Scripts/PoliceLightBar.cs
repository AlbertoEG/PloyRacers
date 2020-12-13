using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PoliceLightBar : MonoBehaviour
{
    [SerializeField] GameObject[] lights;
    [SerializeField] Material[] materials;
    private bool isActive;
    
    //INPUTS
    private PlayerInputActions inputActions;
    
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        lights[0].SetActive(false);
        lights[1].SetActive(false);
        lights[2].SetActive(false);
        lights[3].SetActive(false);
        materials[0].DisableKeyword("_EMISSION");
        materials[1].DisableKeyword("_EMISSION");

        isActive = false;
        
        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.CButton.performed += ctx => ActivateLights();
    }

    void ActivateLights()
    {
        if (isActive) isActive = false;
        else isActive = true;
        
        if(isActive) StartCoroutine(LightFlickering());
    }

    public IEnumerator LightFlickering()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(0.5f);

            lights[0].SetActive(true);
            lights[1].SetActive(false);
            lights[2].SetActive(true);
            lights[3].SetActive(false);
            
            materials[0].EnableKeyword("_EMISSION");
            materials[1].DisableKeyword("_EMISSION");

            yield return new WaitForSeconds(0.5f);

            lights[0].SetActive(false);
            lights[1].SetActive(true);
            lights[2].SetActive(false);
            lights[3].SetActive(true);
            
            materials[0].DisableKeyword("_EMISSION");
            materials[1].EnableKeyword("_EMISSION");
        }
        
        lights[0].SetActive(false);
        lights[1].SetActive(false);
        lights[2].SetActive(false);
        lights[3].SetActive(false);
        
        materials[0].DisableKeyword("_EMISSION");
        materials[1].DisableKeyword("_EMISSION");

        yield return null;
    }
}
