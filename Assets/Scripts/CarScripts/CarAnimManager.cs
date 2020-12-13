using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimManager : MonoBehaviour
{
    private CarControllerV2 carController;
    private Rigidbody carRb;

    public List<ParticleSystem> smokeParticles;
    private bool smokeActive;

    public List<ParticleSystem> grassParticles;

    public List<ParticleSystem> sandParticles;

    // Start is called before the first frame update
    void Awake()
    {
        carController = GetComponent<CarControllerV2>();
        carRb = GetComponent<Rigidbody>();

        //Debug.Log(smokeParticles.Count);
        if (smokeParticles.Count != 0) DisableSmokeParticles();
        smokeActive = false;
        if (grassParticles.Count != 0) DisableGrassParticles();
        if (sandParticles.Count != 0) DisableSandParticles();
    }

    // Update is called once per frame
    void Update()
    {
        if (!smokeActive && (carRb.angularVelocity.y >= carRb.maxAngularVelocity/2 || carRb.angularVelocity.y <= -carRb.maxAngularVelocity / 2))
            ActiveSmokeParticles();
        else if (smokeActive && (carRb.angularVelocity.y <= carRb.maxAngularVelocity / 2 && carRb.angularVelocity.y >= -carRb.maxAngularVelocity / 2)) 
            DisableSmokeParticles();
    }

    void DisableSmokeParticles()
    {
        smokeActive = false;

        foreach (ParticleSystem smoke in smokeParticles)
        {
            smoke.Stop();
        }
    }

    void ActiveSmokeParticles()
    {
        smokeActive = true;

        foreach (ParticleSystem smoke in smokeParticles)
        {
            smoke.Play();
        }
    }

    public void DisableGrassParticles()
    {
        foreach (ParticleSystem grass in grassParticles)
        {
            grass.Stop();
        }
    }

    public void ActiveGrassParticles()
    {
        foreach (ParticleSystem grass in grassParticles)
        {
            grass.Play();
        }
    }

    public void DisableSandParticles()
    {
        foreach (ParticleSystem sand in sandParticles)
        {
            sand.Stop();
        }
    }

    public void ActiveSandParticles()
    {
        foreach (ParticleSystem sand in sandParticles)
        {
            sand.Play();
        }
    }
}
