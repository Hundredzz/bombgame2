using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAttack : MonoBehaviour
{
    public float time = 0;
    private float setTime = 1.1f;
    private float setTimeAfter = 3f;
    private float setTimeAfter2 = 4f;
    private float attackCount = 0;
    private bool isEnter = false;
    public Heart_Health2 health;
    public BossController bossController;
    

    void Update()
    {
        
        if (health.isDeath == true)
        {
            isEnter = false;
        }
        if (isEnter)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Debug.Log("enter");
                if(bossController.phaseTwo == false) { 
                    health.TakeDamage(1);
                    time = setTimeAfter - attackCount;
                }
                else
                {
                    health.TakeDamage(2);
                    time = setTimeAfter2 - attackCount;
                }
                attackCount += 0.1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("isEnter");
            time = setTime;
            isEnter = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("isExit");
            time = 0;
            isEnter = false;
        }
    }
}
