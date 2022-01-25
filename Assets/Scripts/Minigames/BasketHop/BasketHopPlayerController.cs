using UnityEngine;

public class BasketHopPlayerController : PlayerController
{
    public BasketHopScriptableObject scriptableObject;

    public override void FixedUpdate() { }

    public override void Update()
    {
        if(InputManager.GetButton(Player.ID, InputButton.A, 1.0f))
        {
            Animator.SetTrigger("ThrowBall");
            Rigidbody acorn = Instantiate(scriptableObject.acorn, transform.position + scriptableObject.offset, Random.rotation).GetComponent<Rigidbody>();
            acorn.AddForce(scriptableObject.force * acorn.mass, ForceMode.Impulse);
        }
    }

    public override void Kill() { }

    public override void Respawn() { }
}