using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] myObjects;
    void Start()
    {
    }
    IEnumerator SpawnAfterDelay()
    {
        while (true)
        { 
            yield return new WaitForSeconds(6);
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(60, 90), 1, Random.Range(-10, -40));
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
    void OnTriggerEnter(Collider Hitmark)
    {
        if (Hitmark.CompareTag("Player"))
        {
            StartCoroutine(SpawnAfterDelay());
        }
    }
}
