using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile Set/WorldTileData")]
public class WorldTileData : ScriptableObject
{
    public string TileSetName;
    public TilePackingType TilePackingType;
    public int TileSize = 16;
    public Texture2DArray TileMap;
    public List<TileData> TileData = new List<TileData>();

    /// <summary>
    /// Used to get the data for a given tile index.
    /// </summary>
    /// <param name="index"> Index of the tile data we want to get.</param>
    public void GetTileData(int index)
    {

    }

    public void OnValidate()
    {
        if (TileMap == null) return;
        if (TileData.Count == 0) return;

        if(TileMap.depth < TileData.Count)
        {
            TileData.RemoveAt(TileData.Count - 1);
            return;
        }

        for(int index = 0; index < TileData.Count; index++)
        {
            Color[] pixels = TileMap.GetPixels(index);

            TileData data = TileData[index];

            Texture2D text = new Texture2D(TileSize, TileSize);
            text.SetPixels(pixels);
            text.Apply();

            data.TileGraphic = text;

            TileData[index] = data;
        }
    }
}

public enum TilePackingType
{
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}
