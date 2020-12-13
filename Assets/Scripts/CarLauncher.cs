using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarLauncher : MonoBehaviour
{

    public List<GameObject> prefabList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CarLauncherLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CarLauncherLoop()
    {
        while (true)
        {
            GameObject carP = Instantiate(prefabList[Random.Range(0,4)],
                transform.position + new Vector3(0, 0, Random.Range(-7f, 7f)),
                transform.rotation);
            int rc = Random.Range(0, 9);
            carP.GetComponent<LaunchableCar>().mRenderer
                .materials[carP.GetComponent<LaunchableCar>().mIndex].SetColor("_BaseColor",
                    GameManager.GameInstance.colors[rc]);

            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }
}
