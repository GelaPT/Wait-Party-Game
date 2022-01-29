using UnityEngine;

public class CannonDodgeAIController : AIController
{
    public CannonDodgeAIController(Player player) : base(player) { }

    private float moveSpan = 1.0f;
    private float moveTimer = 2.0f;
    private float angle;
    private float speed;

    public override void UpdateAI()
    {
        moveTimer += Time.deltaTime;

        if (Physics.Raycast(new Ray(transform.position + Vector3.up, transform.forward), 3.0f)) angle = Random.Range(0, 90) * 4; ;

        if (moveTimer > moveSpan)
        {
            angle = Random.Range(0, 90) * 4;

            speed = Random.Range(0, 3) switch
            {
                0 => 0f,
                1 => 0.5f,
                2 => 1.0f,
                _ => 1.0f
            };

            moveTimer = 0.0f;
            moveSpan = Random.Range(0.2f, 0.8f);
        }

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

        Player.MoveAxis(InputAxis.Left, direction);
    }
}
