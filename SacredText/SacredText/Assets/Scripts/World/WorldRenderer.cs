using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{
    /*
    public GameObject WorldTilePrefab;  // This is the prefab that we spawn
    public GameObject WorldTileObject;  // Link to the object in-world.

    private int WorldHeight = 5;
    private int WorldWidth = 5;
    private Vector3 WorldOffset;

    // Variables to store the generated mesh
    private Mesh mesh;
    private Vector3[] verts;
    private Vector2[] uvs;
    private int[] triangles;
    

    public void Initialize()
    {
        // checking to see if the WorldTileObject is null, if it is, we want to spawn a new one
        if (WorldTileObject == null)
        {
            WorldTileObject = Instantiate(WorldTilePrefab);
            WorldTileObject.transform.parent = transform;
        }

        mesh = WorldTileObject.GetComponent<MeshFilter>().mesh = new Mesh();
        GenerateWorldGrid();
    }

    public void Initialize(int _WorldHeight, int _WorldWidth)
    {
        // checking to see if the WorldTileObject is null, if it is, we want to spawn a new one
        if (WorldTileObject == null)
        {
            WorldTileObject = Instantiate(WorldTilePrefab);
            WorldTileObject.transform.parent = transform;
        }

        this.WorldHeight = _WorldHeight;  
        this.WorldWidth = _WorldWidth;

        mesh = WorldTileObject.GetComponent<MeshFilter>().mesh = new Mesh();
        GenerateWorldGrid();
    }

    private void GenerateWorldGrid()
    {
        // setting the vert array to be the size of the world as a 1d array.
        verts = new Vector3[(WorldHeight + 1) * (WorldWidth + 1)];
        uvs = new Vector2[verts.Length];

        // creating a new vertex for the coord.
        for (int vertIndex = 0, width = 0; width < WorldWidth + 1; width++)
        {
            for (int height = 0; height < WorldHeight + 1; height++, vertIndex++)
            {
                verts[vertIndex] = new Vector3(height, 0f, width);
                uvs[vertIndex] = new Vector2((float)width / WorldWidth, (float)height / WorldHeight);
            }
        }

        // setting the meshes vertices
        mesh.vertices = verts;
        mesh.uv = uvs;

        // now we are generating the triangles for the mesh
        triangles = new int[WorldHeight * WorldWidth * 6];

        for(int triIndex = 0, vertIndex = 0, width = 0; width < WorldWidth; width++, vertIndex++)
        {
            for(int height = 0; height < WorldHeight; height++, triIndex += 6, vertIndex++)
            {
                triangles[triIndex] = vertIndex;
                triangles[triIndex + 3] = triangles[triIndex + 2] = vertIndex + 1;
                triangles[triIndex + 4] = triangles[triIndex + 1] = vertIndex + WorldHeight + 1;
                triangles[triIndex + 5] = vertIndex + WorldHeight + 2;
            }
        }


        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    // When the world is updated, we want to update the uvs of the grid so that it displays the correct tiles.
    public void OnWorldUpdated()
    {
        for (int width = 0; width < WorldWidth + 1; width++)
        {
            for(int height = 0; height < WorldHeight + 1; height++)
            {
                // We want to set the uv of the vertices
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (verts == null || verts.Length == 0) return;

        Gizmos.color = Color.black;
        for (int i = 0; i < verts.Length; i++)
        {
            Gizmos.DrawSphere(verts[i], 0.1f);
        }
    }*/

    // This is the prefab that we will spawn for each of the Tiles
    public GameObject TilePrefab;

    // Array to store the spawned prefabs.
    private GameObject[] _SpawnedTiles;

    // Storing the world width and height so that we can turn a coordinate to an index
    private int _WorldHeight;
    private int _WorldWidth;


    // To render the world, we want to spawn an array of tiles (procedurally generated quads?) and set their texture
    // Or we could use a quad prefab so that we can save processing time
    public void Initialize(int _WorldHeight, int _WorldWidth)
    {
        this._WorldHeight = _WorldHeight;
        this._WorldWidth = _WorldWidth;

        _SpawnedTiles = new GameObject[_WorldWidth * _WorldHeight];
    }

    // This will pass in an object of the tile indexes, and we will also need to know what tiles we are using.
    public void CreateWorld()
    {
        int tileIndex = 0;

        for(int width = 0; width < _WorldWidth; width++)
        {
            for(int height = 0; height < _WorldHeight; height++)
            {
                _SpawnedTiles[tileIndex] = GameObject.Instantiate(TilePrefab, new Vector3(width, 0f, height), Quaternion.identity, transform);

                _SpawnedTiles[tileIndex].name = string.Format($"[{width}, {height}]");

                tileIndex++;
            }
        }
    }
}
