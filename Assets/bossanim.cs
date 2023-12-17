using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossanim : MonoBehaviour
{
    private Animator animator;
    private AttributeManeger attributeManeger;
    private BossController bossController;
    void Start()
    {
        animator = GetComponent<Animator>();
        attributeManeger = GetComponent<AttributeManeger>();
        bossController = GetComponent<BossController>();
    }

    private void Update()
    {
        if (!bossController.phaseTwo) { 
            if(attributeManeger.Health <= attributeManeger.halfHealth)
            {
                animator.SetTrigger("Phase2");
                animator.SetBool("IsPhase2", true);
                bossController.phaseTwo = true;
            }
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bomb")
        {
            animator.SetTrigger("GetHit");
        }

    }
}
