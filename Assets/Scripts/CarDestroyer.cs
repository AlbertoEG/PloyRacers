using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EEEE");
        Destroy(other.transform.parent.gameObject);
    }
}
