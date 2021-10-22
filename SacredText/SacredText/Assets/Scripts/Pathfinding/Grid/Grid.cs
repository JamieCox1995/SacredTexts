using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public static Grid Instance;

    private int Width;
    private int Height;
    private Vector3 Origin;
    private float CellSize;
    private LayerMask BlockingLayers;

    private Node[,] Cells;

    public bool DebugEnabled = true;

    public Grid(int _Width, int _Height, float _CellSize, Vector3 _Origin)
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;

        this.Width = _Width;
        this.Height = _Height;
        this.CellSize = _CellSize;
        this.Origin = _Origin;

        //Initializing the array of Nodes.
        Cells = new Node[Width, Height];

        // Now we are going to iterate over all of the cells, and instantiate variables for the cell
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                Vector3 worldPosition = new Vector3(x, 0, y) * CellSize + Origin + new Vector3(CellSize / 2f, 0f, CellSize / 2f);
                bool isWalkable = true;

                int movementPenalty = 0;

                Cells[x, y] = new Node(x, y, worldPosition, isWalkable, movementPenalty);
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return Width * Height;
        }
    }

    public Node[,] GetCells()
    {
        return Cells;
    }
    public List<Node> GetNeighbours(Node _Node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX, checkY;

                checkX = _Node.X + x;
                checkY = _Node.Y + y;

                if(checkX >= 0 && checkX < Width && checkY >= 0 && checkY < Height)
                {
                    neighbours.Add(Cells[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    #region Getting Cells
    public Node GetCell(int _X, int _Y)
    {
        if (!IsCoordinatesVaild(_X, _Y)) return null;

        return Cells[_X, _Y];
    }

    public Node GetCell(Vector3 _WorldPosition)
    {
        ConvertWorldPositionToCoordinates(_WorldPosition, out int x, out int y);

        return GetCell(x, y);
    }
    #endregion

    #region Update Cells
    public void UpdateCell(Node _Node, int _X, int _Y)
    {
        Cells[_X, _Y] = _Node;
    }
    #endregion

    // We will want a method that we can call on the grid when an object is spawned or removed in the world.
    // This method will be used to update the gird with all of the walkable areas
    public void OnWorldChanged(Vector3 _ObjectLocation)
    {
        ConvertWorldPositionToCoordinates(_ObjectLocation, out int x, out int y);

        if(FindBlockingTerrain(_ObjectLocation))
        {
            Cells[x, y].Walkable = false;
        }
    }

    public bool FindBlockingTerrain(Vector3 _WorldPosition)
    {
        return Physics.CheckSphere(_WorldPosition, CellSize / 2f, BlockingLayers);
    }

    public void ConvertWorldPositionToCoordinates(Vector3 _WorldPosition, out int _X, out int _Y)
    {
        _X = Mathf.FloorToInt((_WorldPosition - Origin).x / CellSize);
        _Y = Mathf.FloorToInt((_WorldPosition - Origin).z / CellSize);
    }

    public void ConvertCoordinatesToWorldPosition(int _X, int _Y, out Vector3 _WorldPosition)
    {
        _WorldPosition = new Vector3(_X, 0, _Y) * CellSize + Origin + new Vector3(CellSize / 2f, 0f, CellSize / 2f);
    }

    public bool IsCoordinatesVaild(int _X, int _Y)
    {
        return (_X >= 0 && _X < Width && _Y >= 0 && _Y < Height);
    }
}
