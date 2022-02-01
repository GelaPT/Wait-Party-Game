using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public bool fork;
    public Tile[] nextTile;
    public TileRoute route;

    public abstract void Action(Player player);
}