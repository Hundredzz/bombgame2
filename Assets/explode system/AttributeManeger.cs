using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManeger : MonoBehaviour
{
    public float Health;
    private float maxHealth;
    public ThrowingTutorial script;


    [SerializeField] private Healthbar1 healthbar1;
    [SerializeField] private Transform wallbreak;

    private void DealDamage(int damage)
    {
        Health -= damage;
        healthbar1.UpdateHealthbar(Health, maxHealth);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bomb")
        {
            DealDamage(script.bombBaseDamage);
        }

    }

    private void Start()
    {
        maxHealth = Health;
        healthbar1.UpdateHealthbar(Health, maxHealth);
    }

    private void Update()
    {
        if(Health <= 0)
        { 
            Instantiate(wallbreak, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
