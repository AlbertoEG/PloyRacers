using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsiveForce : MonoBehaviour
{
    // force is how forcefully we will push the player away from the enemy.
    public float force = 10000f;
    
    void OnCollisionEnter(Collision c)
    {
        Debug.Log("EEEEEE");
        if (c.gameObject.CompareTag("Obstacle")) 
        {
            Debug.Log("EEEEEE2");
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            transform.parent.GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
}
