using UnityEngine;

public class Hop : MonoBehaviour
{
    public enum HopSide
    {
        Right,
        Left
    }

    public float speed = 1.0f;
    public float acceleration = 0.025f;
    public HopSide direction = HopSide.Right;

    private void FixedUpdate()
    {
        speed += acceleration * Time.deltaTime;

        direction = transform.position.x < -3.5 ? HopSide.Right : transform.position.x > 3.5 ? HopSide.Left : direction;

        switch(direction)
        {
            case HopSide.Left:
                transform.position += speed * Time.deltaTime * Vector3.left;
                break;
            case HopSide.Right:
                transform.position += speed * Time.deltaTime * Vector3.right;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Acorn")) return;

        other.GetComponent<BasketHopAcorn>().Score();
        AudioManager.Instance.PlaySound("sfx_collectitem");
    }
}
