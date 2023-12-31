using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Health2 : MonoBehaviour
{
    public int health = 5;
    private int maxHealth;
    public int numOfheart = 5;
    private float invicible = 2.9f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Vector3 respawnPoint;
    [SerializeField] private AudioSource sfx; 
    [SerializeField] private AudioSource sfxTakedam;
    public Animator animator;


    private bool isInvicible = false;
    private float invicibletime;
    public bool isDeath = false;


    private void Start()
    {
        maxHealth = health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isInvicible == true)
        {
            invicibletime -= Time.deltaTime;
            if (invicibletime <= 0)
            {
                isInvicible = false;
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfheart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(health <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = transform.position;
        }
        if (other.CompareTag("Lava"))
        {
            TakeDamage(1);
        }
        if (other.CompareTag("Heal"))
        {
            if (health < maxHealth) {
                sfx.Play(0);
                health += 1;
                Destroy(other.gameObject);
            }
        }
    }

    private void Death()
    {
        isDeath = true;
        gameObject.SetActive(false);
    }

    public void Resetplayer()
    {
        
        gameObject.SetActive(true);
        isDeath = false;
        health = 5;
    }

    public void TakeDamage(int damage)
    {
        if(isInvicible != true) { 
            animator.SetTrigger("isDamage");
            sfxTakedam.Play(0);
            health -= damage;
            isInvicible = true;
            invicibletime = invicible;
        }
    }
}
