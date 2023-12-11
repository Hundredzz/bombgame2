using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 3f;
    public float attackDistance = 5f;
    public int damageAmount = 1;

    private Transform player;
    private Animator animator;

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
        }
        else if (distanceToPlayer <= attackDistance)
        {
            transform.LookAt(player);
            animator.SetBool("isAttack", true);
            

        }
    }

}
