using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] myObjects;
    public GameObject Heal;
    public int stop = 0;
    public float x1;
    public float x2;
    public float z1;
    public float z2;

    void Start()
    {
    }
    IEnumerator SpawnAfterDelay()
    {
        while (true)
        { 
            yield return new WaitForSeconds(3);
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(x1, x2), 1, Random.Range(z1, z2));
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            if (stop == 1)
            {
                break;
            }
        }
    }
    IEnumerator SpawnAfterHealDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            int randomIndex = Random.Range(0, 2);
            if(randomIndex ==1) { 
                Vector3 randomSpawnPosition = new Vector3(Random.Range(x1, x2), 1, Random.Range(z1, z2));
                Instantiate(Heal, randomSpawnPosition, Quaternion.identity);
            }
            if(stop == 1) {
                break;
            }
        }
    }
    void OnTriggerEnter(Collider Hitmark)
    {
        if (Hitmark.CompareTag("Player"))
        {
            StartCoroutine(SpawnAfterDelay());
            StartCoroutine(SpawnAfterHealDelay());
        }
    }
}
