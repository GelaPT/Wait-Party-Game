using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWings : MonoBehaviour
{
    public GameObject[] wings;

    private int multiplier = 1;
    [SerializeField] private float wingRotationAlpha = 0.5f;
    [SerializeField] private float wingSpeed = 0.1f;
    void FixedUpdate()
    {
        //atualizar valor do alpha
        wingRotationAlpha += multiplier * wingSpeed;

        //atualizar nas asinhas da abelha
        foreach (GameObject wing in wings)
        {
            wing.transform.localEulerAngles = new Vector3(wing.transform.localEulerAngles.x, 0, Mathf.Lerp(-50, 50, wingRotationAlpha));
        }

        //trocar de direçao o alpha para efeito de ping bong
        if (wingRotationAlpha >= 1 || wingRotationAlpha <= 0)
        {
            multiplier *= -1;
        }
    }
}
