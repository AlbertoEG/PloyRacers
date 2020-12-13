using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager GameInstance
    {
        get { return instance; }
    }
    
    public GameObject[] playerPrefabList;
    public GameObject[] aiPrefabList;
    
    public Color[] colors = {new Color(0.4117f, 0.4784f, 0.8470f), new Color(0.2392f, 0.8470f, 0.5058f), new Color(0.2383f, 0.2206f, 0.2735f), 
        new Color(1f, 0.2274f, 0.3019f), new Color(0.9764f, 0.7686f, 0.2745f), new Color(0.8980f, 0.9058f, 0.9686f), 
        new Color(1f, 0.2274f, 0.6057f), new Color(1f, 0.5835f, 0.3019f), new Color(0.6965f, 0.4784f, 0.8470f)};
    
    public string[][] names = {new string[] {"Captain Americar", "Bubblebee", "Blue Fox", "Boomer", "Poseidon"}, 
        new string[] {"Dr Zoom", "Francesco Virgolini", "Wild Duck", "Carterra", "Plantpatine"}, new string[] {"Dark Lagoon", "Black Skull", "Phantom", "Voldamort", "Speedin' Demon"},
        new string[] {"Red Bull", "Poppy", "Phoenix", "Lighting McKing", "Wraith"}, new string[] {"Thunder", "Spark Wolve", "Golden Carrot", "Transporter", "Solar Eclipse"},
        new string[] {"Steel Maiden", "Silver Bullet", "Old Smoky", "Goose", "Sugar Rush"}, new string[] {"Pinky", "Rosebud", "Moon Shadow", "Betty", "Karma"}, 
        new string[] {"Mr Vroom Vroom", "Madd Max", "Texas Ranger", "Orange Box", "Couper"}, new string[] {"Depp Purple", "Poison", "Warlock", "Venus", "Von Carma"}
    };

    public List<bool> carFininshRace;

    public int trackPlayed;

    public AudioClip loseClip;
    public AudioClip winClip;
    private AudioSource aSource;
    
    public enum Difficulty {Easy, Normal}
    public Difficulty diff;
    
    public enum GameMode {Career, Versus, TimeTrial}
    public GameMode mode;
    
    public delegate void OnFinishRace(int id);

    public static event OnFinishRace OnFinishRaceHandler;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
        carFininshRace = new List<bool>();

        trackPlayed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopRestCars()
    {
        for(int i=0; i<carFininshRace.Count; i++)
        {
            if(!carFininshRace[i]) OnFinishRaceHandler?.Invoke(i);
        }
    }

    public void EndRaceCall(int _id)
    {
        OnFinishRaceHandler?.Invoke(_id);

        if (_id == 0)
        {
            FindObjectOfType<MusicController>().StopMusic();
            string position = Laderboard.GetPosition(_id);
            aSource = GetComponent<AudioSource>();
            if (int.Parse(position) <= 3)
            {
                aSource.clip = winClip;
            }
            else
            {
                aSource.clip = loseClip;
            }
            aSource.Play();
        }
    }
}
