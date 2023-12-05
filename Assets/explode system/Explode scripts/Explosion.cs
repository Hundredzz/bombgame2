using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject bombParticlePrefab;
    public Camera mainCamera;

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

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
        Instantiate(bombParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
