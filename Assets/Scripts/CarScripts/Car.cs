using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    //VARIABLES INTERNAS DEL COCHE
    protected float steerFactor;
    protected float turnAmount;

    protected float accelerationForce;

    protected float currentSteerAngle;
    protected enum Terrain { Carretera, Cesped, Arena };
    protected int currentTerrain;
    
    protected MeshRenderer[] meshes;
    
    protected bool invul;

    [Header("Debug Settings")]
    public bool canMove;
    
    //VARIABLES PUBLICAS DEL COCHE
    [Header("Car Components")]
    public List<AxleInfo> axleInfos;
    public UpdateCheckpoint checkpoint;
    public GameObject projectedPosition;
    public Transform centerOfMass;

    [Header("Car Stats")]
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float mass;
    public MeshRenderer mRenderer;
    public int mIndex;

    [HideInInspector] public List<UpdateCheckpoint> checkpoints;
    [HideInInspector] public float motor;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public float decelerationForce;
    [HideInInspector] public float brakeTorque;
    [HideInInspector] public int lap;
    [HideInInspector] public List<float> lapTimes;
    [HideInInspector] public float distToCheck;
    [HideInInspector] public int id;

    protected float currentSpeed = 0;

    public float Speed
    {
        get { return currentSpeed; }
        set
        {
            if (Mathf.Abs(currentSpeed - value) < float.Epsilon) return;
            OnSpeedChangeHandler?.Invoke(currentSpeed = value);
        }
    }

    public delegate void OnSpeedChangeDelegate(float newVal);

    public static event OnSpeedChangeDelegate OnSpeedChangeHandler;
    
    protected void CanMove()
    {
        canMove = true;
    }

    protected virtual void CantMove(int _id)
    {
        if(id == _id) canMove = false;
    }

    // Start is called before the first frame update
    void Awake() { }

    // Update is called once per frame
    void FixedUpdate() { }

    protected void Steering(float _steering, float _msign)
    {
        if (_msign < 0) _steering = -_steering;
        rb.AddTorque(transform.up * (_steering * steerFactor));

        Vector3 steerForward = -(Quaternion.Euler(0, _steering, 0) * transform.forward);

        rb.AddForce(steerForward * (turnAmount * rb.angularVelocity.y));
    }


    protected void Acceleration(AxleInfo _axleInfo, float _motor, float _sign)
    {
        if (_motor != 0)
        {
            _axleInfo.rightWheelCol.brakeTorque = 0;
            _axleInfo.leftWheelCol.brakeTorque = 0;
            _axleInfo.rightWheelCol.motorTorque = _motor;
            _axleInfo.leftWheelCol.motorTorque = _motor;

            if (_sign > 0 && rb.velocity.magnitude > 1) rb.AddForce(transform.forward * (accelerationForce / rb.velocity.magnitude));
            else if (_sign > 0) rb.AddForce(transform.forward * accelerationForce);
            else if (_sign < 0 && rb.velocity.magnitude > 1) rb.AddForce(-transform.forward * (decelerationForce / rb.velocity.magnitude));
            else if (_sign < 0) rb.AddForce(-transform.forward * decelerationForce);
        }
        else
        {
            Deceleration(_axleInfo);
        }
    }

    protected void Deceleration(AxleInfo _axleInfo)
    {
        _axleInfo.rightWheelCol.motorTorque = 0;
        _axleInfo.leftWheelCol.motorTorque = 0;
        _axleInfo.rightWheelCol.brakeTorque = decelerationForce;
        _axleInfo.leftWheelCol.brakeTorque = decelerationForce;
    }

    protected void AirStable(float _drag)
    {
        rb.angularDrag = _drag;
    }

    public void ApplyLocalPositionToVisuals(AxleInfo _axleInfo, float _steeringAngle)
    {
        Vector3 position;
        Quaternion rotation;

        _axleInfo.leftWheelCol.GetWorldPose(out position, out rotation);
        _axleInfo.leftWheelMesh.transform.position = position;

        _axleInfo.rightWheelCol.GetWorldPose(out position, out rotation);
        _axleInfo.rightWheelMesh.transform.position = position;

        if (_axleInfo.steering)
        {
            currentSteerAngle = Mathf.LerpAngle(currentSteerAngle, _steeringAngle, Time.deltaTime * 5);
            _axleInfo.leftWheelMesh.transform.rotation =
                Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + currentSteerAngle + 180, rotation.eulerAngles.z);
            _axleInfo.rightWheelMesh.transform.rotation =
                Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + currentSteerAngle, rotation.eulerAngles.z);
        }
    }

    public void ReturnToCheckpoint()
    {
        if (canMove)
        {
            transform.position = checkpoint.transform.position;
            transform.rotation = checkpoint.transform.rotation;
            for (int i = 0; i < axleInfos.Count; i++)
            {
                axleInfos[i].leftWheelCol.brakeTorque = 0;
                axleInfos[i].rightWheelCol.brakeTorque = 0;
                axleInfos[i].leftWheelCol.motorTorque = 0;
                axleInfos[i].rightWheelCol.motorTorque = 0;
            }
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

            StartCoroutine(InvulTime());
        }
    }

    public void UpdateCheckpoint(UpdateCheckpoint _checkpoint)
    {
        if (checkpoints.Count != 0)
        {
            if (!checkpoints.Contains(_checkpoint) && _checkpoint.mandatory) checkpoints.Add(_checkpoint);
        }
        else
        {
            if (_checkpoint.mandatory) checkpoints.Add(_checkpoint);
        }

        checkpoint = _checkpoint;
    }

    public void SetLapTime(float _lapTime)
    {
        lapTimes.Add(_lapTime);
    }

    public int GetCurrentTerrain()
    {
        return currentTerrain;
    }

    public void SetCurrenTerrain(int _terrain)
    {
        currentTerrain = _terrain;
    }

    public void SetMaxTorque(string _terrain)
    {
        switch (_terrain)
        {
            case "carretera":
                rb.drag = 0;
                break;
            case "other":
                rb.drag = 0.5f;
                break;
            default:
                rb.drag = 0;
                break;
        }
    }

    public void ResetCheckPoints()
    {
        checkpoints.Clear();
    }

    public bool GetAllWheelsGrounded()
    {
        bool grounded = true;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (!axleInfo.leftWheelCol.isGrounded)
            {
                grounded = false;
                break;
            }

            if (!axleInfo.rightWheelCol.isGrounded)
            {
                grounded = false;
                break;
            }
        }

        return grounded;
    }

    protected void SetProjectedCarPosition()
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
        //Vector3 dir = checkpoint.nextCheckpoint.transform.position - checkpoint.transform.position;
        //projectedPosition.transform.position = checkpoint.transform.position + (dir.normalized * distToCheck);
    }

    public IEnumerator InvulTime()
    {
        invul = true;
        
        for (int i=0; i<10; i++) {
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
            yield return new WaitForSeconds(0.1f);

            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        invul = false;

        yield return null;
    }
}
