using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTween : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ShowTime());
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ShowTime()
    {
        RectTransform rt = GetComponent<RectTransform>();
        for (int i=0; i < 10f; i++)
        {
            LeanTween.rotateX(gameObject, 90f, 0f);
            yield return new WaitForSeconds(0.1f);
            
            LeanTween.rotateX(gameObject, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.SetActive(false);

        yield return null;
    }
}
