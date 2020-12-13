using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchableCar : MonoBehaviour
{
    public MeshRenderer mRenderer;
    public int mIndex;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 5000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
