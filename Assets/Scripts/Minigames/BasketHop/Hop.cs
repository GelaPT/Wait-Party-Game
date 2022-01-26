using UnityEngine;

public class Hop : MonoBehaviour
{
    private enum HopSide
    {
        Right,
        Left
    }

    public float speed = 1.0f;
    public float acceleration = 0.025f;
    private HopSide rightSide = HopSide.Right;

    private void FixedUpdate()
    {
        speed += acceleration * Time.deltaTime;

        rightSide = transform.position.x < -3.5 ? HopSide.Right : transform.position.x > 3.5 ? HopSide.Left : rightSide;

        switch(rightSide)
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

        other.GetComponent<Acorn>().Score();
    }
}
