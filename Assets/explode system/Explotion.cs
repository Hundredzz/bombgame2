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
    [SerializeField] private Heart_Health2 heart_Health2;
    [SerializeField] private int selfExDamage = 1;
    [SerializeField] private AudioSource sfx;
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
        sfx.Play(0);
        Instantiate(bombparticlePrefab, transform.position, transform.rotation);
        Collider[] hitColliders = new Collider[10];
        player_bomb.setBombdamto1();
        Destroy(gameObject);
    }
    public void selfExplode(Transform playerTransform)
    {
        sfx.Play(0);
        Instantiate(bombparticlePrefab, playerTransform.position, playerTransform.rotation);
        Collider[] hitColliders = new Collider[10];
        player_bomb.setBombdamto1();
        heart_Health2.health -= selfExDamage;
    }
}