using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimiter : MonoBehaviour
{
    public float maxspeed;
    public float speedreduction = 1f;
    private Rigidbody rb;

    private void Awake()
    {
        maxspeed = 6f;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 Adjustment = Vector3.zero;
        if (rb.velocity.y > maxspeed)
        {
            Adjustment.y += -speedreduction;
        }
        rb.velocity += Adjustment;
    }
}
