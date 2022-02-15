using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDodgeCannon : MonoBehaviour
{
    [SerializeField] private GameObject acorn;
    [SerializeField] private Transform acornSpawn;
    [SerializeField] private GameObject particle;

    private void ShootAcorn()
    {
        Instantiate(acorn, acornSpawn.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 90));
        AudioManager.Instance.PlaySound("sfx_cannon");
        particle.SetActive(true);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
