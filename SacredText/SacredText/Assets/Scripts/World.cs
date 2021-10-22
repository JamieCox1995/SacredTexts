using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Vector2Int WorldSize;
    public Vector3 Origin;

    [SerializeField] private CharacterMovement character;
    private Pathfinding Pathfinding;
    private Grid Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new Grid(WorldSize.x, WorldSize.y, 1f, Origin);

        Pathfinding = GetComponent<Pathfinding>();

        Pathfinding.Initialize();

        PathRequestManager.Instance.Initialize(Pathfinding);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (character)
        //    {
        //        character.SetPath(grid.FindPathInWorldSpace(new Vector2Int(0, 0), new Vector2Int(80, 1)));
        //    }
        //}
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
            Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.8f);
        }
    }
}
