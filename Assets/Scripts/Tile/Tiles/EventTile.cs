using UnityEngine;

public class EventTile : Tile
{
    public override void Action(Player player)
    {
        Debug.Log("Event");
    }
}
