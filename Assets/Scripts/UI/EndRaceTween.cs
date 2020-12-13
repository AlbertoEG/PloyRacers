using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRaceTween : MonoBehaviour
{
    [SerializeField] private GameObject times;
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject posRanking;
    [SerializeField] private GameObject pointsRanking;

    private void OnEnable()
    {
        LeanTween.moveLocal(times, new Vector3(0f, 0f, 0f), 1f);
        LeanTween.moveLocal(name, Vector3.zero, 1f);
        LeanTween.moveLocal(buttons, new Vector3(0f, -282f, 0f), 1f);
    }

    public void nextMenu()
    {
        if (GameManager.GameInstance.mode == GameManager.GameMode.TimeTrial)
        {
            FindObjectOfType<PlayerUtillities>().ReturnToSelector();
        }
        else
        {
            LeanTween.moveLocal(times, new Vector3(1500f, 0f, 0f), 1f);
            LeanTween.moveLocal(name, new Vector3(1500f, 0f, 0f), 1f);
            LeanTween.moveLocal(buttons, new Vector3(1320f, -282f, 0f), 1f);

            LeanTween.moveLocal(posRanking, Vector3.zero, 1f);
        }
    }
    
    public void nextMenu2()
    {
        if (GameManager.GameInstance.mode == GameManager.GameMode.Versus)
        {
            FindObjectOfType<PlayerUtillities>().ReturnToSelector();
        }
        else
        {
            LeanTween.moveLocal(posRanking, new Vector3(1500f, 0f, 0f), 1f);

            LeanTween.moveLocal(pointsRanking, Vector3.zero, 1f);
        }
    }
}
