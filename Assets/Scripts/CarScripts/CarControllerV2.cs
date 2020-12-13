using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CarControllerV2 : Car
{
    //INPUTS
    private PlayerInputActions inputAction;

    private Vector2 steerInput;
    private Vector2 motorInput;
    
    //CAMERAS
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera[] cams;

    private CinemachineVirtualCamera cam;
    
    //OTHER
    public delegate void OnEndStatsDelegate(CarControllerV2 car);

    public static event OnEndStatsDelegate OnEndStatsHandler;

    private void OnEnable()
    {
        GameManager.OnFinishRaceHandler += CantMove;
        StartRace.onRaceStart += CanMove;
        inputAction.Enable();
    }

    private void OnDisable()
    {
        StartRace.onRaceStart -= CanMove;
        GameManager.OnFinishRaceHandler -= CantMove;
        inputAction.Disable();
    }
    
    protected override void CantMove(int _id)
    {
        if (id == _id)
        {
            canMove = false;
            cams[0].m_Priority = 10;
            cams[1].m_Priority = 11;
            
            int newPoints = 0;
            string position = Laderboard.GetPosition(id);
            switch (position)
            {
                case "1":
                    newPoints = 8;
                    break;
                case "2":
                    newPoints = 6;
                    break;
                case "3":
                    newPoints = 4;
                    break;
                case "4":
                    newPoints = 3;
                    break;
                case "5":
                    newPoints = 2;
                    break;
                case "6":
                    newPoints = 1;
                    break;
                default:
                    newPoints = 0;
                    break;
            }
            Laderboard.setFinalPos(id, int.Parse(Laderboard.GetPosition(id)));
            Laderboard.setRaceStats(id, newPoints);
            GameManager.GameInstance.carFininshRace[id] = true;
            GameManager.GameInstance.StopRestCars();
            
            OnEndStatsHandler.Invoke(this);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Input creation
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Steer.performed += ctx => steerInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.Motor.performed += ctx => motorInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.Respawn.performed += ctx => ReturnToCheckpoint();
        
        //Camera initiation
        cams[0].m_Priority = 11;
        cams[1].m_Priority = 10;
        cam = cams[0].GetComponent<CinemachineVirtualCamera>();

        //Car variables initiation
        rb = GetComponent<Rigidbody>();
        if (centerOfMass) rb.centerOfMass = new Vector3(rb.centerOfMass.x, centerOfMass.position.y, rb.centerOfMass.z);
        rb.mass = mass;
        rb.maxAngularVelocity = 1f;

        decelerationForce = maxMotorTorque / 2f;
        brakeTorque = maxMotorTorque;
        steerFactor = 100f;
        turnAmount = 1000f;
        accelerationForce = maxMotorTorque;
        currentSteerAngle = 0;

        lap = 0;

        lapTimes = new List<float>();
        
        //Get meshes and colliders
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor = maxMotorTorque * motorInput.y * Time.deltaTime;
        float steering = maxSteeringAngle * steerInput.x;
        Speed = transform.InverseTransformDirection(new Vector3(rb.velocity.x, 0 , rb.velocity.z)).magnitude;

        if (canMove)
        {
            //TRUCO PARA QUE EL COCHE NO CAIGA EN PICADO
            if (!GetAllWheelsGrounded())
                AirStable(0.5f);
            else AirStable(0);


            for (int i = 0; i < axleInfos.Count; i++)
            {
                if (GetAllWheelsGrounded())
                {
                    Steering(steering, motorInput.y);

                    if (axleInfos[i].motor)
                    {
                        Acceleration(axleInfos[i], motor, motorInput.y);
                    }
                }
                ApplyLocalPositionToVisuals(axleInfos[i], steering);
            }
        }
        else
        {
            for (int i = 0; i < axleInfos.Count; i++)
            {
                axleInfos[i].leftWheelCol.brakeTorque = brakeTorque;
                axleInfos[i].rightWheelCol.brakeTorque = brakeTorque;
                axleInfos[i].leftWheelCol.motorTorque = 0;
                axleInfos[i].rightWheelCol.motorTorque = 0;
            }
        }
        float mod = Mathf.Clamp(Speed, 0f, 40f);
        cam.m_Lens.FieldOfView = 70 + Mathf.InverseLerp(0f, 40f, mod) * 5f;
        SetProjectedCarPosition();
        Laderboard.SetPosition(id, lap, checkpoint.id, distToCheck);
    }
}
