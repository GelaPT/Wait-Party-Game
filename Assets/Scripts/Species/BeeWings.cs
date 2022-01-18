using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWings : MonoBehaviour
{
    public GameObject[] wings;

    private float wingRotationAlpha = 0.5f;
    [SerializeField] private float wingSpeed = 10;
    [SerializeField] private Vector2 minMaxRot = new Vector2(-50, 50);
    void Update()
    {
        //atualizar valor do alpha
        wingRotationAlpha = Mathf.Sin(Time.time * wingSpeed) * 0.5f + 0.5f;

        //atualizar nas asinhas da abelha
        foreach (GameObject wing in wings)
        {
            wing.transform.localEulerAngles = new Vector3(wing.transform.localEulerAngles.x, 0, Mathf.Lerp(minMaxRot.x, minMaxRot.y, wingRotationAlpha));
        }
    }
}
