using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb2 : MonoBehaviour
{
    public ThrowingTutorial script;
    void OnCollisionEnter(Collision other) //Parameter
    {
        if (script.cangetbomb2 == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        else { }
    }
}
