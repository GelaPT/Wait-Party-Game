using UnityEngine;

public class CarRaceCheckpoint : MonoBehaviour
{
    public int checkpoint;
    public float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        CarRaceManager.Instance.NextCheckpoint(other.gameObject, checkpoint);
    }
}