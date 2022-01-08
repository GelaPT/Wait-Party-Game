using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelection : MonoBehaviour {

    private List<UICharacter> characters = new();

    public void FindCharacters()
    {
        for (int i = 0; i < 4; i++)
        {
            UICharacter character = GameObject.Find("P" + (i+1) + "Char").GetComponent<UICharacter>();
            characters.Add(character);
        }
    }

    public void ChangeModel(string Id_CharIndex)
    {
        int id = int.Parse(Id_CharIndex.Split('_')[0]);
        int charIndex = int.Parse(Id_CharIndex.Split('_')[1]);

        characters[id-1].ChangeModel(charIndex);
    } 
}
