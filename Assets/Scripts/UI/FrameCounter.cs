using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrameCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float timer;
    private float hudRefreshRate = 1;

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps + " FPS";
            timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}
