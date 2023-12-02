using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Explotion : MonoBehaviour
{
    private float upwardsModifier = 10;
    private bool hiton = false;
    public GameObject bombparticlePrefab;
    public ThrowingTutorial player_bomb;
    private void Start()
    {
        if (!bombparticlePrefab)
        {
            bombparticlePrefab = Resources.Load<GameObject>("bombparticle");
        }
    }
        void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
        }
        else hiton = true;
    }

    private void Update()
    {
        if (hiton == true)
        {
            {
                Explode();
            }
        }
    }
    private void Explode()
    {
        Instantiate(bombparticlePrefab, transform.position, transform.rotation);
        Collider[] hitColliders = new Collider[10];
        // player_bomb.setBombdamto1();
        Destroy(gameObject);
    }
}