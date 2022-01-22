using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public bool fork;
    public Tile[] nextTile;

    public abstract void Action(Player player);
}