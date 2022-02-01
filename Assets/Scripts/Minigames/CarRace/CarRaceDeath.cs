using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        CarRaceManager.Instance.ResetCar(other.gameObject);
    }
}
