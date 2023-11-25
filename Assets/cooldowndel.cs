using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cooldowndel : MonoBehaviour
{
    [SerializeField]private float time = 3f;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
           Destroy(gameObject);
        }
    }
}
