using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileData
{
    public int TileIndex;
    public string TileName;
    public float TileMovementCost;
    public bool IsTileWalkable;
    public Texture2D TileGraphic;
}
