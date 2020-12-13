using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfBounds : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.CompareTag("Car"))
        {
            StartCoroutine(TimerCheck(other));
        }
    }

    private IEnumerator TimerCheck(Collider other)
    {
        other.transform.parent.gameObject.GetComponent<Car>().canMove = false;
        yield return new WaitForSeconds(1f);
        other.transform.parent.gameObject.GetComponent<Car>().ReturnToCheckpoint();
        other.transform.parent.gameObject.GetComponent<Car>().canMove = true;
        yield return null;
    }
}
