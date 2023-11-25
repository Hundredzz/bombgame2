using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public ThrowingTutorial script;
    void OnCollisionEnter(Collision other) //Parameter
    {
        if (script.cangetbomb == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        else { }
    }
}
