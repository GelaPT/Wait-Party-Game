using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBallAIController : AIController
{
    public DodgeBallAIController(Player player) : base(player) { }

    float moveTimer = 0.0f;
    float moveSpan = 0.2f;

    float pressingTimer = 0.0f;
    float pressingSpan = 1.0f;

    float angle;
    float speed;

    public override void UpdateAI()
    {
        if((Player.PlayerController as DodgeBallPlayerController).balls > 0)
        {
            Player.PressButton(InputButton.A);

            pressingTimer += Time.deltaTime;

            if(pressingTimer > pressingSpan)
            {
                Player.PressButton(InputButton.X);

                int player;
                if (Player.ID == 1 || Player.ID == 3)
                {
                    player = Random.Range(0, 2) == 0 ? 0 : 2;
                }
                else
                {
                    player = Random.Range(0, 2) == 0 ? 1 : 3;
                }

                Vector3 playerPos = Vector3.zero;

                if (PlayerManager.Instance.Players[player].PlayerController != null)
                    playerPos = PlayerManager.Instance.Players[player].PlayerController.transform.position;

                Vector3 dir = playerPos - transform.position;

                dir.Normalize();

                Player.MoveAxis(InputAxis.Left, new Vector2(dir.x, dir.z));
            }
        } 
        else
        {
            pressingTimer = 0.0f;

            moveTimer += Time.deltaTime;

            if (Physics.Raycast(new Ray(transform.position + Vector3.up, transform.forward), 3.0f)) angle = Random.Range(0, 90) * 4; ;

            if (moveTimer > moveSpan)
            {
                angle = Random.Range(0, 90) * 4;

                speed = Random.Range(0, 2) switch
                {
                    0 => 0f,
                    1 => 1.0f,
                    _ => 1.0f
                };

                moveTimer = 0.0f;
                moveSpan = Random.Range(0.2f, 0.8f);
            }

            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

            Player.MoveAxis(InputAxis.Left, direction);
        }
    }
}
