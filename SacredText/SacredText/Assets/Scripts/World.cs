using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WorldSize;
    public Vector3 Origin;

    public LayerMask BlockingLayers;

    [SerializeField] private CharacterMovement character;
    private Pathfinding Pathfinding;
    private Grid Grid;

    public static event Action<Vector3> onWorldChanged;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new Grid(WorldSize.x, WorldSize.y, 1f, Origin);
        Grid.BlockingLayers = BlockingLayers;

        Pathfinding = GetComponent<Pathfinding>();

        Pathfinding.Initialize();

        PathRequestManager.Instance.Initialize(Pathfinding);

        onWorldChanged += Grid.OnWorldChanged;
    }

    public void SpawnObject()
    {
        if (onWorldChanged != null) onWorldChanged.Invoke(Vector3.zero);
    }

    private void OnDrawGizmos()
    {
        if (Grid == null) return;

        // we want to go over all of the cells in the Grid and draw a cube
        foreach(Node node in Grid.GetCells())
        {
            //Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, n.movementPenalty));
            Gizmos.color = Color.white;
            Gizmos.color = (node.Walkable) ? Gizmos.color : Color.red;
            Gizmos.DrawWireCube(node.WorldPosition, new Vector3(1f, 0f, 1f));
        }
    }
}
