using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public GameObject UIModelPrefab;
    public GameObject playablePrefab;
}


public class CharacterManager : Singleton<CharacterManager>
{
    public Character[] Characters;

    public UICharacter[] uiCharacters;
}