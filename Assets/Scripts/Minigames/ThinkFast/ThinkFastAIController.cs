using UnityEngine;

public class ThinkFastAIController : AIController
{
    public ThinkFastAIController(Player player) : base(player) { }

    private float answerTimer = 0.0f;
    private float answerSpan = 0.0f;
    private bool answered = false;

    public override void UpdateAI()
    {
        if (!ThinkFastManager.Instance.isRound)
        {
            answered = false;
            return;
        }

        if (answered) return;

        if (answerSpan == 0.0f)
        {
            answerSpan = Random.Range(0.5f, 2.2f);
            answerTimer = 0.0f;
            return;
        }

        answerTimer += Time.deltaTime;

        if (answerTimer > answerSpan)
        {
            Player.PressButton(ThinkFastManager.Instance.currentButton switch
            {
                ThinkFastManager.ThinkFastButton.Square => InputButton.Left,
                ThinkFastManager.ThinkFastButton.Triangle => InputButton.Down,
                ThinkFastManager.ThinkFastButton.Heart => InputButton.Right,
                ThinkFastManager.ThinkFastButton.NotSquare => InputButton.Right,
                ThinkFastManager.ThinkFastButton.NotTriangle => InputButton.Left,
                ThinkFastManager.ThinkFastButton.NotHeart => InputButton.Down,
                ThinkFastManager.ThinkFastButton.None => InputButton.Up,
                _ => InputButton.Up
            });

            answerSpan = 0.0f;
            answered = true;
        }
    }
}
