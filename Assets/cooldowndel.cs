using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDel : MonoBehaviour
{
    [SerializeField] private float time = 3f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float moveSpeed = 1f;

    private MeshRenderer[] childMeshRenderers;
    private Collider[] childColliders;

    void Start()
    {
        childMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        childColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0) { 
            time -= Time.deltaTime;
            StartCoroutine(Delcolider());
        }
        else 
        {   
            
            StartCoroutine(FadeAndMoveDown());
        }
    }

    IEnumerator Delcolider()
    {
        yield return new WaitForSeconds(time);
        foreach (var collider in childColliders)
            {
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
    }

    IEnumerator FadeAndMoveDown()
    {
        float elapsedTime = 0f;

        // Assuming all child MeshRenderers share the same material
        Color startColor = childMeshRenderers.Length > 0 ? childMeshRenderers[0].material.color : Color.white;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            foreach (var meshRenderer in childMeshRenderers)
            {
                if (meshRenderer != null)
                {
                    meshRenderer.material.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), t);
                }
            }

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Disable colliders of all child objects before destroying
        

        // Destroy the GameObject after fading and moving
        Destroy(gameObject);
    }
}