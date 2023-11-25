using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public AudioSource sfx;
  private void OnTriggerEnter(Collider collision)
    {   
        
        if(collision.tag == "DZ")
        {   
            transform.position = respawnPoint;
        }

        if(collision.tag == "Lava")
        {
            sfx.Play(0);
            transform.position = respawnPoint;
        }

        else if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
}
