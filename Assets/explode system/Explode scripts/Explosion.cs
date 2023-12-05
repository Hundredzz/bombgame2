using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject bombParticlePrefab;
    public Camera mainCamera;
    public Heart_Health2 heart_health2;
    public ThrowingTutorial throwingtutorial;

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public AudioSource sfx;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        originalPosition = mainCamera.transform.localPosition;
    }

    private void Start()
    {
        if (bombParticlePrefab == null)
        {
            bombParticlePrefab = Resources.Load<GameObject>("bombparticle");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        StartCoroutine(ProcessShake());
        Explode();
    }

    private IEnumerator ProcessShake()
    {
        Debug.Log("Shake coroutine started");

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
            float z = originalPosition.z + Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.transform.localPosition = new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }

    private void Explode()
    {
        sfx.Play(0);
        Instantiate(bombParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void selfExplode(Transform player)
    {
        sfx.Play(0);
        heart_health2.animator.SetTrigger("isDamage");
        Instantiate(bombParticlePrefab, player.position, player.rotation);
        heart_health2.health -= 1;
        throwingtutorial.setBombdamto1();
    }
}
