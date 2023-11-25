using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ThrowingTutorial : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    public int totalThrows;
    public float throwCooldown;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    public int bombtier1 = 1;
    public bool cangetbomb = true;
    public bool cangetbomb2 = true;
    bool readyToThrow;

    private void Start()
    {
        readyToThrow = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
            cangetbomb = true;
            cangetbomb2 = true;
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bombitem" && cangetbomb == true)
        {
            if (readyToThrow == false)
            {
                bombtier1 = 1;
                cangetbomb2 = false;
            }
                readyToThrow = true;
                bombtier1 *= 2;
        }


        if (other.gameObject.tag == "bombitem2" && cangetbomb2 == true)
        {
                if (readyToThrow == false)
                {
                    bombtier1 = 1;
                    cangetbomb = false;
                }
                readyToThrow = true;
                bombtier1 *= 3;
        }
    }
}