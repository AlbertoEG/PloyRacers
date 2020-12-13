using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class TrackPathCreator : MonoBehaviour
{
    [SerializeField] CircuitController circuit;

    [SerializeField] GameObject pathPrefab;
    private GameObject dummyPlacer;
    [SerializeField] float pathDistance;
    private float lastPathTime;
    private int pathNumber;
    private int currentCheckpoint;
    private bool startPlacing;

    public void CreateAiTrackPath() 
    {
        dummyPlacer = GameObject.Find("DUMMY");
        if(dummyPlacer == null)
        {
            dummyPlacer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            DestroyImmediate(dummyPlacer.GetComponent<Collider>());
            dummyPlacer.gameObject.name = "DUMMY";
        }
        currentCheckpoint = 0;
        dummyPlacer.transform.position = circuit.waypoints[currentCheckpoint].transform.position;
        pathNumber = 1;
        lastPathTime = Time.time + pathDistance;
        startPlacing = true;
    }

    private void PlaceTrackPath()
    {
        GameObject tp = Instantiate(pathPrefab);
        tp.transform.position = dummyPlacer.transform.position;
        tp.transform.rotation = dummyPlacer.transform.rotation;
        tp.transform.parent = this.transform;
        tp.name = pathNumber + " AiPath";
        if(pathNumber == 1) circuit.aiPaths.Clear();
        circuit.aiPaths.Add(tp.GetComponent<AiPath>());
        pathNumber++;
    }

    private void Update()
    {
        if (!startPlacing) return;

        Quaternion rotation = Quaternion.LookRotation(circuit.waypoints[currentCheckpoint].transform.position
            - dummyPlacer.transform.position);

        dummyPlacer.transform.rotation = Quaternion.Slerp(dummyPlacer.transform.rotation, rotation, Time.deltaTime * 2);
        dummyPlacer.transform.Translate(0,0,1);

        if(Vector3.Distance(dummyPlacer.transform.position, circuit.waypoints[currentCheckpoint].transform.position) < 1)
        {
            currentCheckpoint++;
            if (currentCheckpoint >= circuit.waypoints.Length) currentCheckpoint = 0;
        }

        if(lastPathTime < Time.time)
        {
            PlaceTrackPath();
            lastPathTime = Time.time + pathDistance;
        }
        EditorApplication.QueuePlayerLoopUpdate();
    }
}
#endif
