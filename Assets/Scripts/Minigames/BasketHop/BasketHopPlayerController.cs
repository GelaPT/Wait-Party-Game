using UnityEngine;

public class BasketHopPlayerController : PlayerController
{
    public BasketHopScriptableObject scriptableObject;

    private void Start()
    {
        if (Player.AI)
        {
            AIController = gameObject.AddComponent<BasketHopAIController>();
            AIController.Player = (AIPlayer)Player;
        }
    }

    protected override void Update()
    {
        if (BasketHopManager.Instance.ended) return;

        base.Update();

        if (InputManager.GetButton(Player.ID, InputButton.A, 1.0f))
        {
            Animator.SetTrigger("ThrowBall");
            Rigidbody acorn = Instantiate(scriptableObject.acorn, transform.position + scriptableObject.offset, Random.rotation).GetComponent<Rigidbody>();
            acorn.AddForce(scriptableObject.force * acorn.mass, ForceMode.Impulse);

            acorn.GetComponent<BasketHopAcorn>().player = Player;
        }
    }
}