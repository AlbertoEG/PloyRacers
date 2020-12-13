using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrackSelector : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler
{
    private AudioSource sound;
    [SerializeField] private GameObject text;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        sound.Play();
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        GetComponent<Image>().color = GetComponentInChildren<Image>().color * 2f;
        LeanTween.scale(gameObject, new Vector3(1.25f, 1.25f, 1f), 0.1f);
        LeanTween.scale(text, new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        /*this.GetComponent<Selectable>().OnPointerExit(null);
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);*/
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        
        EventSystem.current.SetSelectedGameObject(null);
        GetComponent<Image>().color = GetComponentInChildren<Image>().color / 2f;
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
        LeanTween.scale(text, new Vector3(0f, 0f, 0f), 0.1f);
    }
}
