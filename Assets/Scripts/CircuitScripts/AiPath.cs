using System.Collections;
using System.Collections.Generic;
using CarScripts;
using UnityEngine;

public class AiPath : MonoBehaviour
{

    public AiPath[] nextPaths;
    public enum CurveType {EntryPoint, ClippingPoint, ExitPoint, None}
    public CurveType curveType; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.parent.gameObject.CompareTag("Car"))
            {
                if(other.transform.parent.gameObject.TryGetComponent(out AiCarController aiCar)) aiCar.AiPathUpdate(this);
            }
        }
    }
}
