using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDodgePlayerController : PlayerController
{
    private Rigidbody rb;
    private float speed = 75.0f;
    private bool isStunned = false;

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
        collider.height = 1.5f;
        collider.center = Vector3.up;

        if (Player.AI)
        {
            AIController = gameObject.AddComponent<CannonDodgeAIController>();
            AIController.Player = (AIPlayer)Player;
        }
    }

    public override void Update()
    {
        base.Update();

        if (isStunned) return;

        Vector2 direction = InputManager.GetAxis(Player, InputAxis.Left);

        direction.x *= -1;

        if(direction.magnitude > 0.2f) transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(Vector2.down, direction), 0);

        direction.y *= -1;

        Animator.SetFloat("MoveBlend", direction.magnitude);

        BaseVelocity = speed * new Vector3(direction.x, 0, direction.y);

        Velocity = Vector3.Lerp(Velocity, BaseVelocity, 0.5f);

        rb.velocity = Velocity;
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    public override void Kill() {
        isStunned = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Animator.SetBool("isStunned", true);
        Invoke("DestroySelf", 2.5f);
    }

    public override void Respawn() { }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}