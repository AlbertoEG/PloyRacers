using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarImpact : MonoBehaviour
{
    [SerializeField] private float impactForce = 1000f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.parent.gameObject.CompareTag("Car"))
            {
                //Debug.Log("Colision");
                
                Rigidbody rb1 = transform.parent.GetComponent<Rigidbody>();
                Rigidbody rb2 = other.transform.parent.GetComponent<Rigidbody>();

                Vector3 dir = other.transform.parent.transform.position - transform.parent.transform.position;

                if ((rb1.mass - rb2.mass) >= 0)
                {
                    float massDiff = rb1.mass - rb2.mass;
                    rb2.AddForce(dir * (((Math.Abs(massDiff)/1000f) + 1) * rb1.velocity.magnitude) * impactForce);
                }
                else
                {
                    rb2.AddForce(dir * rb1.velocity.magnitude * impactForce);
                }
            }
        }
    }
}
