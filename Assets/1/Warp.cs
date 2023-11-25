using UnityEngine;

public class Warp : MonoBehaviour
{
    private GameObject currentTeleporter;

    void Update()
    {
        if (currentTeleporter != null)
        {
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}