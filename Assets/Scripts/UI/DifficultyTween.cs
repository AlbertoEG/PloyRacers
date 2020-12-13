using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyTween : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IDeselectHandler
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject border;
    
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        sound.Play();
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        ShowText(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);
        //GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        ShowText(false);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        ShowText(false);
    }

    private void ShowText(bool _b)
    {
        if (_b)
        {
            LeanTween.scale(text, new Vector3(1f, 1f, 1f), 0.1f);
            LeanTween.scale(panel, new Vector3(1f, 1f, 1f), 0.1f);
            LeanTween.scale(border, new Vector3(1f, 1f, 1f), 0.1f);
        }
        else
        {
            LeanTween.scale(text, new Vector3(0f, 0f, 0f), 0.1f);
            LeanTween.scale(panel, new Vector3(0f, 0f, 0f), 0.1f);
            LeanTween.scale(border, new Vector3(0f, 0f, 0f), 0.1f);
        }
    }
}
