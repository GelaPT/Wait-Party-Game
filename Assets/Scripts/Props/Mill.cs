using UnityEngine;

public class Mill : MonoBehaviour
{
    public GameObject blades;
    private float xRotation;
    [Range(-5,0)] public float speed = 0.2f;
    
    void Start()
    {
        xRotation = Random.Range(0, 359);
        blades.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        speed = Random.Range(-1.0f, -0.2f);
    }
    void FixedUpdate()
    {
        xRotation += speed * Mathf.PerlinNoise(0, Time.time / 2.0f) * 2.0f;
        blades.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        if (xRotation >= 360) xRotation = 0;
    }
}
