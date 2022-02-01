using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public bool fork;
    public Tile[] nextTile;
    public TileRoute route;
    public bool[] jump;

    private void Start()
    {
        route = GetComponentInChildren<TileRoute>();
    }

    public abstract void Action(Player player);
}