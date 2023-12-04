using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ThrowingTutorial : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    [SerializeField] Transform player;
    [SerializeField] Explotion explotion;

    public int totalThrows;
    public float throwCooldown;
    public int totalBomb;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    public int bombtier1 = 1;
    public bool cangetbomb = true;
    public bool cangetbomb2 = true;
    bool readyToThrow;
    [SerializeField] int maximumBombDamage = 1000;

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
        maxBomb();

    }

    

    private void maxBomb() {
        if(bombtier1>maximumBombDamage)
        {
            explotion.selfExplode(player);
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
                cangetbomb2 = false;
                bombtier1 = 1;
                totalBomb = 0;
            }
            readyToThrow = true;
            bombtier1 *= 2;
            totalBomb += 1;
        }


        if (other.gameObject.tag == "bombitem2" && cangetbomb2 == true)
        {
            if (readyToThrow == false)
            {;
                cangetbomb = false;
                bombtier1 = 1;
                totalBomb = 0;
            }
            readyToThrow = true;
            bombtier1 *= 3;
            totalBomb += 1;
        }
    }

    public void setBombdamto1()
    {
        bombtier1 = 1;
        totalBomb = 0;
    }
}