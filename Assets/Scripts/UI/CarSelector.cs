using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IDeselectHandler
{
    [SerializeField] private GameObject car;
    private bool inside = false;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject stats;
    
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        sound.Play();
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        //GetComponent<Image>().color = new Color(1f, 1f, 1f);
        LeanTween.scale(gameObject, new Vector3(1.25f, 1.25f, 1f), 0.1f);
        LeanTween.scale(car, new Vector3(0.75f, 0.75f, 0.75f), 0.1f);
        inside = true;
        ShowStats(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);
        //GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
        LeanTween.scale(car, new Vector3(0.65f, 0.65f, 0.65f), 0.1f);
        inside = false;
        ShowStats(false);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
        LeanTween.scale(car, new Vector3(0.65f, 0.65f, 0.65f), 0.1f);
        inside = false;
        ShowStats(false);
    }

    private void Update()
    {
        car.transform.position = cam.transform.position + cam.transform.forward * 3f + cam.transform.up * -0.25f;
        if(inside) car.transform.Rotate (Vector3.up * 50 * Time.deltaTime, Space.Self);
    }

    private void ShowStats(bool _b)
    {
        if(_b) LeanTween.scale(stats, new Vector3(1f, 1f, 1f), 0.1f);
        else LeanTween.scale(stats, new Vector3(0f, 0f, 1f), 0.1f);
    }
}
