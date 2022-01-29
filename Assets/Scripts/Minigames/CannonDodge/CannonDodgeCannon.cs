using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDodgeCannon : MonoBehaviour
{
    [SerializeField] private GameObject acorn;
    [SerializeField] private Transform acornSpawn;

    private void ShootAcorn()
    {
        Instantiate(acorn, acornSpawn.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 90));
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
