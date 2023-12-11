using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private bool IsAttackP = false;

    // Assuming Heart_Health2 is a MonoBehaviour attached to the same GameObject
    private Heart_Health2 heart_Health2;

    void Start()
    {
        // Get the reference to the Heart_Health2 script
        heart_Health2 = GetComponent<Heart_Health2>();
    }

    void OnTriggerEnter(Collider col)
    {
        StartCoroutine(SetAttack());

        if (col.gameObject.tag == "Player" && IsAttackP)
        {
            // Assuming health is a public variable in Heart_Health2
            heart_Health2.health -= 1;
            IsAttackP = false;
        }
        else
        {
            IsAttackP = false;
        }
    }

    IEnumerator SetAttack()
    {
        yield return new WaitForSeconds(1);
        IsAttackP = true;
    }
}
