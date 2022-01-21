using UnityEngine;

public class IdleRotation : MonoBehaviour
{
    private float rotationAlpha = 0.5f;
    [SerializeField] private float speed = 10;
    [SerializeField] private Vector2 minMaxRot = new Vector2(-5, 5);
    private float randomStartRotation;
    private float randomStartTimer;
    private void Start()
    {
        randomStartRotation = Random.Range(0, 359.99f);
        randomStartTimer = Random.Range(0, 1);
    }
    void Update()
    {
        rotationAlpha = Mathf.Sin((Time.time + randomStartTimer) * speed) * 0.5f + 0.5f;

        transform.localEulerAngles = new Vector3(0, randomStartRotation + Mathf.Lerp(minMaxRot.x, minMaxRot.y, rotationAlpha), 0);
    }
}
