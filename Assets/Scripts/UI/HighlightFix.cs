using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Selectable))]
public class HighlightFix : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sound.Play();
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        //GetComponentInChildren<TextMeshProUGUI>().color = GetComponentInChildren<TextMeshProUGUI>().color * 2f;
        LeanTween.scale(gameObject, new Vector3(1.25f, 1.25f, 1f), 0.1f).setIgnoreTimeScale(true);
    }

    /*public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
        GetComponentInChildren<TextMeshProUGUI>().color = GetComponentInChildren<TextMeshProUGUI>().color / 2f;
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
    }*/
    
    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //GetComponentInChildren<TextMeshProUGUI>().color = GetComponentInChildren<TextMeshProUGUI>().color / 2f;;
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);
    }
}
