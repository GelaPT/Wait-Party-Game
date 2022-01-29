using UnityEngine;

public class AIController : MonoBehaviour
{
    public AIPlayer Player { get; set; }

    public AIController(Player player)
    {
        Player = (AIPlayer)player;
    }

    public virtual void UpdateAI() { }
}
