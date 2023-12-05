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
    [SerializeField] Explosion explotion;

    // public int totalThrows;
    // public float throwCooldown;
    public int totalBomb;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    public int bombBaseDamage = 1;
    public bool isPickBomb = false;
    public int bomb = 0;
    // public bool cangetbomb = true;
    // public bool cangetbomb2 = true;
    bool readyToThrow;
    [SerializeField] int maximumBombDamage = 1000;

    private void Awake() {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        readyToThrow = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow )
        {
            Throw();
            // cangetbomb = true;
            // cangetbomb2 = true;
        }
        // maxBomb();

    }

    // private void maxBomb() {
    //     if(bombtier1>maximumBombDamage)
    //     {
    //         explotion.selfExplode(player);
    //     }
    // }

    private void Throw()
    {
        isPickBomb = false;
        readyToThrow = false;
        totalBomb = 0;
        bomb = 0;

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

        // totalThrows--;

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bombitem"){
            int bombDamage = other.gameObject.GetComponent<bombStat>().bombDamage;

            if (bomb == 0){
                print("First pick");
                setBombdamto1();
                UpdateDamage(other, bombDamage);
            }
            else{
                if (bombDamage != bomb)
                {
                    print("Not the same type");
                    return;
                }
                else
                {
                    print("Same type");
                    UpdateDamage(other, bombDamage);
                }
            }
        }
    }

    private void UpdateDamage(Collision other, int bombDamage)
    {
        int firstBombType = other.gameObject.GetComponent<bombStat>().bombDamage;
        bomb = firstBombType;
        Destroy(other.gameObject);
        isPickBomb = true;
        readyToThrow = true;

        bombBaseDamage *= bombDamage;
        totalBomb++;
    }

    // public void setBombdamto1(){
    //     Debug.Log("setBombdamto1 called"); 
    //     bombBaseDamage = 1;
    // }
    public void setBombdamto1()
    {
        bombBaseDamage = 1;
        totalBomb = 0;
    }
}