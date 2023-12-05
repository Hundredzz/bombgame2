using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private spawn spawn;

    public Transform GetDestination()
    {
        spawn.stop = 1;
        return destination;
    }
}