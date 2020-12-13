using System;
using System.Collections.Generic;
using DebugScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CarScripts
{
    public class AiCarController : Car
    {
        public AiPath currentAiPath;
        public float steerGap;
        public float steerVel;
        
        [Header("Sensors")] 
        [SerializeField] private float sensorLenght = 5f;
        [SerializeField] private Vector3 frontSensorPos = new Vector3(0f, 0.5f, 0.5f);
        [SerializeField] private float frontSideSensorPos = 0.5f;
        [SerializeField] private float frontSideSensorAngle = 30f;
        [SerializeField] private bool avoiding;
        private float lastTimeMoving = 0f;
        private float steerMod = 1f;
        private string state = "";
        
        [Header("Debug Settings")]
        [HideInInspector] public bool monitoring;

        public TextMeshProUGUI name;
        private void OnEnable()
        {
            GameManager.OnFinishRaceHandler += CantMove;
            StartRace.onRaceStart += CanMove;
        }

        private void OnDisable()
        {
            StartRace.onRaceStart -= CanMove;
            GameManager.OnFinishRaceHandler -= CantMove;
        }
        
        protected override void CantMove(int _id)
        {
            if (id == _id)
            {
                canMove = false;
                
                if (monitoring)
                {
                    Debug.Log(lapTimes[0] + "|" + lapTimes[1] + "|" + lapTimes[2]);
                    
                    AiMonitor.WriteCarMetrics(lapTimes);
                }
                
                int newPoints = 0;
                switch (Laderboard.GetPosition(id))
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
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            //if (monitoring) AiMonitor.GetCarTextPath(this, carModel);
            
            rb = GetComponent<Rigidbody>();
            rb.mass = mass;
            rb.maxAngularVelocity = 1f;

            if (centerOfMass) rb.centerOfMass = new Vector3(rb.centerOfMass.x, centerOfMass.position.y, rb.centerOfMass.z);

            decelerationForce = maxMotorTorque / 2f;
            brakeTorque = maxMotorTorque;
            steerFactor = 100f;
            turnAmount = 1000f;
            accelerationForce = maxMotorTorque;
            currentSteerAngle = 0;

            lap = 0;

            lapTimes = new List<float>();

            //id = Laderboard.RegisterCar("AICar", gameObject);
            //name.text = Laderboard.getCarName(id);
            
            //Get meshes and colliders
            meshes = GetComponentsInChildren<MeshRenderer>();

            invul = false;

            //if (SceneManager.GetActiveScene().name.Equals("NASCARCircuit")) steerMod = 1.5f;
        }

        void FixedUpdate()
        {
            Vector2 values = ObstacleAvoidance2();
            values = AiDriving4(values);
            motor = (maxMotorTorque * values.x * Time.deltaTime) * getPosMultiplier();
            float steering = maxSteeringAngle * values.y * steerMod;

            if (canMove)
            {
                if (rb.velocity.magnitude > 0.25f) lastTimeMoving = Time.timeSinceLevelLoad;
                if(Time.timeSinceLevelLoad - 3f > lastTimeMoving + 1f && !invul) ReturnToCheckpoint();

                //TRUCO PARA QUE EL COCHE NO CAIGA EN PICADO
                if (!GetAllWheelsGrounded())
                    AirStable(0.5f);
                else AirStable(0);

                if (getPosMultiplier() > 1f && Vector3.Distance(transform.position, Camera.main.transform.position) > 150f)
                {
                    DrivingBoost();
                }
                else
                {
                    for (int i = 0; i < axleInfos.Count; i++)
                    {
                        if (GetAllWheelsGrounded())
                        {
                            NewSteering(steering);

                            if (axleInfos[i].motor)
                            {
                                Acceleration(axleInfos[i], motor, Mathf.Sign(motor));
                            }
                        }

                        ApplyLocalPositionToVisuals(axleInfos[i], steering);
                    }
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
            
            SetProjectedCarPosition();
            Laderboard.SetPosition(id, lap, checkpoint.id, distToCheck);
        }
        
        protected void NewSteering(float _steering)
        {
            rb.AddTorque(transform.up * (_steering * steerFactor));

            float tMod = Mathf.InverseLerp(10f, 40f,
                Vector3.Magnitude(new Vector3(rb.velocity.x, 0, rb.velocity.z)));

            float sMod = Mathf.Lerp(0, _steering, tMod);
                
            //Debug.Log(tMod + "-" + sMod);
            
            tMod = 1 - tMod;
            
            Vector3 steerForward = -(Quaternion.Euler(0, sMod, 0) * transform.forward);

            turnAmount = (1000f * tMod) + 1000f;

            rb.AddForce(steerForward * (turnAmount * rb.angularVelocity.y));
        }

        public void AiPathUpdate(AiPath _aiPath)
        {
            currentAiPath = _aiPath;
        }

        private Vector2 AiDriving3(Vector2 _prevValues)
        {
            if (avoiding) return _prevValues;
                
            float alpha = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[0].transform.position - transform.position).normalized, Vector3.up);
            alpha = Mathf.Clamp(alpha, -steerGap, steerGap);
            alpha /= steerGap;

            float beta = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[1].transform.position - transform.position).normalized, Vector3.up);
            beta = Mathf.Clamp(beta, -steerGap, steerGap); 
            beta /= steerGap;
            
            float distA = Vector3.Distance(currentAiPath.nextPaths[0].transform.position, transform.position); 
            float distB = Vector3.Distance(currentAiPath.nextPaths[1].transform.position, transform.position);
            
            float percA = distA / distB; 
            float percB = 1f - percA;
            
            alpha *= percA; 
            beta *= percB;
            
            float _motor = Mathf.Lerp(0, 1f, beta);
            _motor = 1 - _motor;
            if (_motor <= steerVel) _motor = steerVel;

            return new Vector2(_motor, (alpha + beta));
        }

        private Vector2 AiDriving4(Vector2 _prevValues)
        {
            if (avoiding) return _prevValues;
            
            float _motor = 0;
            
            switch (currentAiPath.nextPaths[2].curveType)
            {
                case AiPath.CurveType.ClippingPoint:
                    break;
                case AiPath.CurveType.EntryPoint:
                    state = "slow";
                    break;
                case AiPath.CurveType.ExitPoint:
                    state = "fast";
                    break;
                case AiPath.CurveType.None:
                    break;
            }
            
            switch (currentAiPath.curveType)
            {
                case AiPath.CurveType.ClippingPoint:
                    state = "fast";
                    break;
                case AiPath.CurveType.EntryPoint:
                    state = "slower";
                    break;
                case AiPath.CurveType.ExitPoint:
                    state = "faster";
                    break;
                case AiPath.CurveType.None:
                    break;
            }

            switch (state)
            {
                case "slow": _motor = 0.5f;
                    break;
                case "slower": _motor = 0.4f;
                    break;
                case "fast": _motor = 0.75f;
                    break;
                case "faster": _motor = 1f;
                    break;
                default: _motor = 1f;
                    break;
            }
            
            float alpha = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[0].transform.position - transform.position).normalized, Vector3.up);
            alpha = Mathf.Clamp(alpha, -steerGap, steerGap);
            alpha /= steerGap;

            float beta = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[1].transform.position - transform.position).normalized, Vector3.up);
            beta = Mathf.Clamp(beta, -steerGap, steerGap); 
            beta /= steerGap;
            
            float distA = Vector3.Distance(currentAiPath.nextPaths[0].transform.position, transform.position); 
            float distB = Vector3.Distance(currentAiPath.nextPaths[1].transform.position, transform.position);
            
            float percA = distA / distB; 
            float percB = 1f - percA;
            
            alpha *= percA; 
            beta *= percB;
            
            return new Vector2(_motor, (alpha + beta));
        }

        ///SEGUNDO INTENTO OBSTACLE AVOIDANCE
        private Vector2 ObstacleAvoidance2()
        {

            int layerMask = 1 << 9;
            RaycastHit hit;
            Vector3 sensorStartPos = transform.position;
            sensorStartPos += transform.forward * frontSensorPos.z;
            sensorStartPos += transform.up * frontSensorPos.y;
            float avoidMultiplier = 0;
            avoiding = false;

            float newSensorLenght = Mathf.Clamp(rb.velocity.magnitude, 15f, 30f);
            newSensorLenght = Mathf.InverseLerp(15f, 30f, newSensorLenght);
            newSensorLenght = sensorLenght + (sensorLenght * newSensorLenght);
            float newSensorAngle = Mathf.Clamp(rb.velocity.magnitude, 15f, 30f);
            newSensorAngle = Mathf.InverseLerp(15f, 30f, newSensorLenght);
            newSensorAngle = frontSideSensorAngle - (newSensorAngle * frontSideSensorAngle / 2f);
            
            //Front right sensor
            sensorStartPos += transform.right * frontSideSensorPos;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, newSensorLenght, layerMask))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoidMultiplier -= 1f;
                avoiding = true;
            }
            
            //Front right angle sensor
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(newSensorAngle, transform.up) * transform.forward, out hit, newSensorLenght, layerMask))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoidMultiplier -= 0.5f;
                avoiding = true;
            }
            
            //Front left sensor
            sensorStartPos -= transform.right * (2f * frontSideSensorPos);
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, newSensorLenght, layerMask))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoidMultiplier += 1f;
                avoiding = true;
            }
            
            //Front left angle sensor
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-newSensorAngle, transform.up) * transform.forward, out hit, newSensorLenght, layerMask))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoidMultiplier += 0.5f;
                avoiding = true;
            }
            
            //Front center sensor
            if (avoidMultiplier == 0)
            {
                sensorStartPos += transform.right * frontSideSensorPos;
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, newSensorLenght, layerMask))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    if (hit.normal.x < 0) avoidMultiplier = -1;
                    else avoidMultiplier = 1;
                    avoiding = true;
                }
            }

            float _motor;
            if (avoiding) _motor = steerVel;
            else _motor = 1;

            return new Vector2(_motor, avoidMultiplier);
        }

        private float getPosMultiplier()
        {
            string pos = Laderboard.GetPosition(id);
            string pPos = Laderboard.GetPosition(0);

            if (int.Parse(pos) > int.Parse(pPos))
            {
                switch (pos)
                {
                    case "1": return 1f;
                    case "2": return 1.15f;
                    case "3": return 1.25f;
                    case "4": return 1.3f;
                    case "5": return 1.4f;
                    case "6": return 1.5f;
                    default: return 1f;
                }
            }
            else
            {
                switch (pos)
                {
                    case "1": return 0.85f;
                    case "2": return 0.875f;
                    case "3": return 0.9f;
                    case "4": return 0.95f;
                    case "5": return 0.975f;
                    case "6": return 0.99f;
                    default: return 1f;
                }
            }
        }

        private void DrivingBoost()
        {
            float alpha = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[0].transform.position - transform.position).normalized, Vector3.up);
            float beta = Vector3.SignedAngle(transform.forward,
                (currentAiPath.nextPaths[1].transform.position - transform.position).normalized, Vector3.up);
            float distA = Vector3.Distance(currentAiPath.nextPaths[0].transform.position, transform.position); 
            float distB = Vector3.Distance(currentAiPath.nextPaths[1].transform.position, transform.position);
            
            float percA = distA / distB; 
            float percB = 1f - percA;
            
            alpha *= percA; 
            beta *= percB;
            
            transform.Rotate(Vector3.up, alpha + beta);
            
            motor = (maxMotorTorque * 1f * Time.deltaTime) * getPosMultiplier();
            
            for (int i = 0; i < axleInfos.Count; i++)
            {
                if (axleInfos[i].motor)
                {
                    Acceleration(axleInfos[i], motor, Mathf.Sign(motor));
                }
            }
        }

        public void SetMaxTorque(float _diffScale)
        {
            maxMotorTorque = maxMotorTorque * _diffScale;
        }
    }
}
