using UnityEngine;

public class WarpTile : Tile
{
    public Tile warpTile;

    public override void Action(Player player)
    {
        Debug.Log("Warp");
    }
}
