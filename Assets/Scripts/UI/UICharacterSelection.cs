using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelection : MonoBehaviour {

    public int uiCharacterIndex;
    

    public void ChangeCharacter(GameObject character)
    {
        CharacterManager.Instance.uiCharacters[uiCharacterIndex].ChangeModel(character);
    }
}
