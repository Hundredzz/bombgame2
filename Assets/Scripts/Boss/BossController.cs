using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 3f;
    public float attackDistance = 5f;
    public int damageAmount = 1;
    public bool isAttack = false;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            transform.LookAt(player);
            animator.SetBool("isAttack", false);
            isAttacking = false; // Reset the flag when the player is out of attack range
        }
        else if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            transform.LookAt(player);
            animator.SetBool("isAttack", true);
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true; // Set the flag to true to prevent multiple coroutine calls

        yield return new WaitForSeconds(1);
        isAttack = true;
        yield return new WaitForSeconds(0.4f);
        isAttack = false;

        isAttacking = false; // Reset the flag after the attack is complete
    }
}

