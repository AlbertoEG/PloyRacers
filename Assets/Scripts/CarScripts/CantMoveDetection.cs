using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMoveDetection : MonoBehaviour
{

    public AxleInfo frontWheels;
    public AxleInfo backWheels;

    private bool countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = false;
    }

    // Update is called once per frame
    void Update()
    {
        ///COMPROBAR SI ESTA DADO LA VUELTA (NO ESTA HACIENDO FALTA)
        /*int layerMask = 1 << 8;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10, Color.white);
            //Debug.Log("Did not Hit");
        }*/

        ///COMPROBAR SI ESTA ATASCADO
        int layerMask = 1 << 9;

        RaycastHit frontHit;
        bool frontHitbool;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out frontHit, 2.25f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * frontHit.distance, Color.yellow);
            frontHitbool = true;
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2.25f, Color.white);
            frontHitbool = false;
            //Debug.Log("Did not Hit");
        }

        RaycastHit backHit;
        bool backHitbool;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out backHit, 2.25f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * backHit.distance, Color.yellow);
            backHitbool = true;
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2.25f, Color.white);
            backHitbool = false;
            //Debug.Log("Did not Hit");
        }

        if (frontHitbool && backHitbool)
        {
            Debug.Log("No puedo avanzar");
        }


        ///COMPROBAR SI NO TE PUEDES MOVER
    }
}
