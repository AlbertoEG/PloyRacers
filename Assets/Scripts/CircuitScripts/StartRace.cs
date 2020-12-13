using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{

    public List<GameObject> leds;

    public delegate void OnRaceStart();
    public static event OnRaceStart onRaceStart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i=0; i<leds.Count; i++)
        {
            yield return new WaitForSeconds(1f);

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetColor("_BaseColor", new Color(0, 1f, 0));
            leds[i].GetComponent<Renderer>().SetPropertyBlock(block);
            leds[i].GetComponentInChildren<Light>().color = new Color(0, 1f, 0);
            leds[i].GetComponent<AudioSource>().Play();
        }

        if (onRaceStart != null) onRaceStart();

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < leds.Count; i++)
        {
            leds[i].GetComponent<Renderer>().material.color = new Color(0.25f, 0.25f, 0.25f);
            leds[i].GetComponentInChildren<Light>().intensity = 0;
        }

        yield return null;
    }
}
