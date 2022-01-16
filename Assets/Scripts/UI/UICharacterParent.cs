using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterParent : MonoBehaviour
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

    public void PlayReadyAnimation()
    {
        foreach (GameObject _char in characters)
        {
            if (!_char.activeSelf) continue;
            _char.GetComponentInChildren<Animator>().SetBool("isSelected", true);
        }
    }

    public void StopReadyAnimation()
    {
        foreach (GameObject _char in characters)
        {
            if (!_char.activeSelf) continue;
            _char.GetComponentInChildren<Animator>().SetBool("isSelected", false);
        }
    }
}