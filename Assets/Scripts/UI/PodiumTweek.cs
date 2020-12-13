using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PodiumTweek : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ranking;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI[] nameLadderTexts;
    [SerializeField] private TextMeshProUGUI[] pointLadderTexts;

    public void nextMenu()
    {
        LadderStats();
        
        LeanTween.moveLocal(ranking, Vector3.zero, 2f);
        button.interactable = false;
        button.enabled = false;
        button.image.color = new Color(0.75f, 0.75f, 0.75f, 0.75f);
    }
    
    public void LadderStats()
    {
        PlayerStats[] cars = Laderboard.getOrderedCarsPoints();

        for (int i=0; i < cars.Length; i++)
        {
            if(cars[i].id == 0) nameLadderTexts[i].color = Color.white;
            else nameLadderTexts[i].color = Color.black;
            nameLadderTexts[i].text = cars[i].name;
            switch (i)
            {
                case 0: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 1: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 2: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 3: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 4: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                case 5: pointLadderTexts[i].text = cars[i].points + "";
                    break;
                default: pointLadderTexts[i].text = "???";
                    break;
            }
        }
    }
}
