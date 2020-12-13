using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDummy : MonoBehaviour
{

    private int id;
    private float speed;

    public List<UpdateCheckpoint> checkpoints;
    public UpdateCheckpoint checkpoint;
    public int lap;
    public float distToCheck;

    public GameObject projectedPosition;

    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.1f;

        dir = (checkpoint.nextCheckpoint.transform.position - transform.position).normalized;

        id = Laderboard.RegisterCar("Dummy", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed;
        
        SetProjectedCarPosition();
        Laderboard.SetPosition(id, lap, checkpoint.id, distToCheck);
    }

    public void UpdateCheckpoint(UpdateCheckpoint _checkpoint)
    {
        checkpoint = _checkpoint;
        dir = (checkpoint.nextCheckpoint.transform.position - transform.position).normalized;
    }

    private void SetProjectedCarPosition()
    {
        float dc = Vector2.Distance(new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.z),
            new Vector2(checkpoint.nextCheckpoint.transform.position.x, checkpoint.nextCheckpoint.transform.position.z));
        float dcc1 = Vector2.Distance(new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.z),
            new Vector2(transform.position.x, transform.position.z));
        float dcc2 = Vector2.Distance(new Vector2(checkpoint.nextCheckpoint.transform.position.x, checkpoint.nextCheckpoint.transform.position.z),
            new Vector2(transform.position.x, transform.position.z));
        distToCheck = (Mathf.Pow(dcc1, 2) - Mathf.Pow(dcc2, 2) + Mathf.Pow(dc, 2)) / (2 * dc);
        ///DEBUG
        //Debug.Log("X:" + x + " DCC1: " + dcc1 + " DCC2: " + dcc2 + " DC: " + dc);
        //Vector3 _dir = checkpoint.nextCheckpoint.transform.position - checkpoint.transform.position;
        //projectedPosition.transform.position = checkpoint.transform.position + (_dir.normalized * distToCheck);
    }
}
