using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Health2 : MonoBehaviour
{
    public int health = 5;
    private int maxHealth;
    public int numOfheart = 5;
    private float invicible = 2f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Vector3 respawnPoint;
    [SerializeField] private AudioSource sfx;
    public Animator animator;


    private bool isInvicible = false;
    private float invicibletime;


    private void Start()
    {
        maxHealth = health;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
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
        if (other.CompareTag("Lava") && isInvicible != true)
        {
            animator.SetTrigger("isDamage");
            health -= 1;
            isInvicible = true;
            invicibletime = invicible;
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
        gameObject.SetActive(false);
    }

    public void Resetplayer()
    {
        gameObject.SetActive(true);
        health = 5;
    }
}
