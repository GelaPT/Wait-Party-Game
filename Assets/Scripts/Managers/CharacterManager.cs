using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public GameObject characterUI;
    public GameObject characterPlayable;
    public Sprite characterIcon;
}

public class CharacterManager : Singleton<CharacterManager>
{
    [HideInInspector] public int randomCharacter;
    [HideInInspector] public int bunreeCharacter;
    [HideInInspector] public int florixCharacter;
    [HideInInspector] public int peckoCharacter;
    [HideInInspector] public int shadowCatCharacter;
    [HideInInspector] public int witchonCharacter;

    [HideInInspector] public Character RandomCharacter { get; private set; }
    [HideInInspector] public Character BunreeCharacter { get; private set; }
    [HideInInspector] public Character FlorixCharacter { get; private set; }
    [HideInInspector] public Character PeckoCharacter { get; private set; }
    [HideInInspector] public Character ShadowCatCharacter { get; private set; }
    [HideInInspector] public Character WitchonCharacter { get; private set; }

    [SerializeField] private Character[] characters;
    public Character[] Characters { 
        get => characters; 
        private set { characters = value; } 
    }

    [HideInInspector] public bool[] characterChosen;

    private void Start()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            switch (characters[i].name)
            {
                case "Random":
                    RandomCharacter = characters[i];
                    randomCharacter = i;
                    break;
                case "Bunree":
                    BunreeCharacter = characters[i];
                    bunreeCharacter = i;
                    break;
                case "Florix":
                    FlorixCharacter = characters[i];
                    florixCharacter = i;
                    break;
                case "Pecko":
                    PeckoCharacter = characters[i];
                    peckoCharacter = i;
                    break;
                case "ShadowCat":
                    ShadowCatCharacter = characters[i];
                    shadowCatCharacter = i;
                    break;
                case "Witchon":
                    WitchonCharacter = characters[i];
                    witchonCharacter = i;
                    break;
            }
        }

        characterChosen = new bool[Characters.Length];
        for(int i = 0; i < Characters.Length; i++)
        {
            characterChosen[i] = false;
        }
    }

    public void ChooseCharacter(int chosenCharacter)
    {
        characterChosen[chosenCharacter] = true;
    }

    public Character GetCharacter(string name)
    {
        foreach(Character character in Characters)
        {
            if (character.name == name) return character;
        }

        return default;
    }
}
