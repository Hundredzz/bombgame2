using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManeger : MonoBehaviour
{
    public float Health;
    private float maxHealth;
    public float halfHealth;
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
        halfHealth = maxHealth / 2;
        healthbar1.UpdateHealthbar(Health, maxHealth);
    }

    private void Update()
    {
        if(Health <= 0)
        { 
            if(wallbreak != null) { 
                Instantiate(wallbreak, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
