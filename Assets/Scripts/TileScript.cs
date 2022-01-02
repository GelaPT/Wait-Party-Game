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
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (GameObject tile in nextTile)
        {
            Handles.DrawLine(transform.position, tile.transform.position);
        }
    }
    #endif
}
