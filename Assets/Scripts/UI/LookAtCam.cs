using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCam : MonoBehaviour
{
    private Transform cam;
    private TextMeshProUGUI text;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>().transform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dist = Vector3.Distance(cam.position, transform.position);
        if (dist > 15f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }else if (dist <= 15f && dist > 8f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0.7f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
        }
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }
        transform.LookAt(cam.position, cam.rotation * Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
    }
}
