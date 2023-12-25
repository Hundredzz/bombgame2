using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 3f;
    public float attackDistance = 5f;
    public float lengthattackDistance = 50f;
    public int damageAmount = 1;
    public bool isAttack = false;
    public bool phaseTwo = false;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public GameObject spawnPoint;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private float lastThrowTime;
    private float cooldown = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        lastThrowTime = -cooldown; // Initialize the last throw time to allow the first throw immediately
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance && distanceToPlayer <= lengthattackDistance)
        {
            // Move towards the player
            transform.LookAt(player);
            animator.SetBool("isAttack", false);
            isAttacking = false;
            if (Time.time - lastThrowTime >= cooldown)
            {
                // Throw projectile at the player
                Movement movementScript = player.GetComponent<Movement>();
                if (movementScript != null)
                {
                    Vector3 playerVelocity = movementScript.GetPlayerVelocity();
                    ThrowProjectile(spawnPoint, playerVelocity);
                }
            }
        }
        else if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            // Check if enough time has passed since the last throw
            transform.LookAt(player);
            animator.SetBool("isAttack", true);
        }
    }

    void ThrowProjectile(GameObject spawnPoint, Vector3 playerVelocity)
    {
        // Get the height of the spawn point
        float spawnPointHeight = spawnPoint.transform.position.y;

        // Adjust this value to control the height to which the projectile is thrown
        float playerHeadOffset = 3.0f; // Increase this value for a higher throw

        // Extrapolation factor to predict future player position
        float extrapolationFactor = 8f; // Adjust this factor as needed

        // Estimate the player's future head position with extrapolation
        Vector3 predictedPlayerPosition = player.position + playerVelocity * Time.deltaTime * extrapolationFactor;
        Vector3 playerHeadPosition = predictedPlayerPosition + Vector3.up * playerHeadOffset;

        // Calculate the instantiation position based on the predicted player's head
        Vector3 instantiationPosition = new Vector3(spawnPoint.transform.position.x, spawnPointHeight + 1f, spawnPoint.transform.position.z);

        // Instantiate the projectile at the predicted player's head position
        GameObject projectile = Instantiate(projectilePrefab, instantiationPosition, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Apply initial force to make the projectile move towards the predicted player's head
        projectileRb.AddForce((playerHeadPosition - instantiationPosition).normalized * projectileSpeed, ForceMode.VelocityChange);

        lastThrowTime = Time.time; // Update the last throw time
    }

}

