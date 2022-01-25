using UnityEngine;

[CreateAssetMenu(fileName = "BasketHopScriptableObjects", menuName = "ScriptableObjects/BasketHopScriptableObjects", order = 1)]
public class BasketHopScriptableObject : ScriptableObject
{
    public GameObject acorn;
    public Vector3 offset;
    public Vector3 force;
}