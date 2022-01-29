using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketHopAIController : AIController
{
    public BasketHopAIController(Player player) : base(player) { }

    private float distance = 1.0f;
    private bool hasValue = false;
    private float throwTimer = 0.0f;

    public override void UpdateAI()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer < 1.15f) return;

        Hop hop = BasketHopManager.Instance.Hop;

        if (!hasValue)
        {
            throwTimer = 0.0f;
            distance = Random.Range(2.5f, 0.2f);
            hasValue = true;
            return;
        }

        switch(hop.direction)
        {
            case Hop.HopSide.Left:
                if (hop.transform.position.x - transform.position.x < distance && hop.transform.position.x - transform.position.x > 0.0f)
                {
                    Player.PressButton(InputButton.A);
                    hasValue = false;
                }
                break;
            case Hop.HopSide.Right:
                if (hop.transform.position.x - transform.position.x > -distance && hop.transform.position.x - transform.position.x < 0.0f)
                {
                    Player.PressButton(InputButton.A);
                    hasValue = false;
                }
                break;
        }
    }
}
