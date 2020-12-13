using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheelCol;
    public WheelCollider rightWheelCol;
    public GameObject leftWheelMesh;
    public GameObject rightWheelMesh;
    public bool motor;
    public bool steering;
}
