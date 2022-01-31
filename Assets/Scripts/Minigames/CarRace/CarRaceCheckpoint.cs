using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceCheckpoint : MonoBehaviour
{
    public int checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CarRacePlayerController player = other.GetComponent<CarRacePlayerController>();

        /*if(player.currentCheckpoint == checkpoint - 1)
        {
            player.NextCheckpoint();
        }*/
    }
}