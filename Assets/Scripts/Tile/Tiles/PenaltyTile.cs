using UnityEngine;

public class PenaltyTile : Tile
{
    public override void Action(Player player)
    {
        Debug.Log("Penalty");
    }
}
