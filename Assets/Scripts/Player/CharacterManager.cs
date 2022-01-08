using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public GameObject UIModelPrefab;
    public GameObject playablePrefab;
}


public class CharacterManager : MonoBehaviour
{
    public Character[] Characters;
}