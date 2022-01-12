using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacter : MonoBehaviour
{
    public GameObject[] characters;

    public int PlayerId = 0;

    private void Start()
    {
        for(int i = 0; i< characters.Length; i++)
        {
            characters[i].SetActive(i == 0);
        }
    }

    public void ChangeModel(GameObject character)
    {
        foreach(GameObject _char in characters)
        {
            _char.SetActive(_char.ToString() == character.ToString());
        }
    }
}
