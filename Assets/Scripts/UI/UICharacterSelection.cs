using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterSelection : MonoBehaviour {

    public int uiCharacterIndex;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI playerText;

    private void Start()
    {
        playerText.SetText("P"+(uiCharacterIndex+1));
    }
    public void ChangeCharacter(GameObject character)
    {
        LobbySceneManager.Instance.uiCharacters[uiCharacterIndex].ChangeModel(character);
    }
}
