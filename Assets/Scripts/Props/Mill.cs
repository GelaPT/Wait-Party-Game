using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{
    public GameObject blades;
    private float xRotation;
    [Range(0,10)] public float speed = 0.2f;
    
    void Start()
    {
        xRotation = Random.Range(0, 359);
        blades.transform.localRotation = Quaternion.Euler(xRotation,0,0);
    }
    void FixedUpdate()
    {
        xRotation += speed;
        blades.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        if (xRotation >= 360) xRotation = 0;
    }
}
