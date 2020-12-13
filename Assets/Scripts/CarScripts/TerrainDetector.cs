using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetector : MonoBehaviour
{
    private Car carController;
    private CarAnimManager carAnimManager;

    private string tag;

    // Start is called before the first frame update
    void Awake()
    {
        carController = GetComponent<Car>();
        carAnimManager = GetComponent<CarAnimManager>();
        tag = "";
    }

    private void Update()
    {
        WheelHit hit;
        if(carController.axleInfos[0].leftWheelCol.GetGroundHit(out hit))
        {
            string newTag = hit.collider.tag;
            if (!tag.Equals(newTag))
            {
                tag = newTag;
                switch (tag)
                {
                    case "Carretera":
                        carController.SetCurrenTerrain(0);
                        carController.SetMaxTorque("carretera");
                        carAnimManager.DisableGrassParticles();
                        carAnimManager.DisableSandParticles();
                        break;
                    case "Cesped":
                        carController.SetCurrenTerrain(1);
                        carController.SetMaxTorque("other");
                        carAnimManager.ActiveGrassParticles();
                        carAnimManager.DisableSandParticles();
                        break;
                    case "Arena":
                        carController.SetCurrenTerrain(2);
                        carController.SetMaxTorque("other");
                        carAnimManager.DisableGrassParticles();
                        carAnimManager.ActiveSandParticles();
                        break;
                    default:
                        carController.SetCurrenTerrain(10);
                        carController.SetMaxTorque("carretera");
                        carAnimManager.DisableGrassParticles();
                        carAnimManager.DisableSandParticles();
                        break;
                }
            }
        }
    }
}
