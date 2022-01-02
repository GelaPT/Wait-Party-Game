using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public enum TileType
    {
        REWARD,
        MINIGAME,
        EVENT,
        PENALTY,
        TELEPORT,
        SHOP
    }
    
    public TileType Type;

    public GameObject[] previousTile;
    public GameObject[] nextTile;
    public GameObject teleportToTile;

    public bool[] hasToJump = new bool[] {false};
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i< nextTile.Length; i++)
        {
            Handles.color = hasToJump[i] ? Color.red : Color.white;
            Handles.DrawLine(transform.position, nextTile[i].transform.position);
        }
    }
    #endif
}
