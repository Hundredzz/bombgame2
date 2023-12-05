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
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Set animation conditions
            animator.SetBool("Golem_FrontWalk", true);
        }
        else
        {
            // Attack the player
            // Implement your attack logic here

            // Set animation conditions
            animator.SetBool("Golem_FrontWalk", false);
            animator.SetTrigger("AttackLight");

            // Damage the player
            Heart_Health2 playerHealth = player.GetComponent<Heart_Health2>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
