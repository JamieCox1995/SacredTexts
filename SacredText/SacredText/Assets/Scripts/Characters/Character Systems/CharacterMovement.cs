using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float TimeToTarget = 0.3f;

    public float MaximumMovementPerTurn = 30f;      // Total no. of feet (number of cells * unit movement cost) the character can move in one round
    private float remainingMovement;

    private Vector3 originalWorldPosition;          // Stores the world position of the character before it started moving.
    private List<Vector3> targetPositionBuffer;     // Stores all of the locations that the character wants to move to. This will be utilised by the pathfinding system.
    private Vector3 targetWorldPosition;            // Stores the target position the character wants to move to.

    private bool isMoving = false;

    private CharacterAnimation characterAnimation;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();

        Initialize();
    }

    private void Initialize()
    {
        targetPositionBuffer = new List<Vector3>();
        targetWorldPosition = transform.position;

        remainingMovement = MaximumMovementPerTurn;

        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPositionBuffer.Count > 0 && remainingMovement > 0)
        {
            if(!isMoving)
            {
                Debug.Log("Starting to move...");
                StartCoroutine(MoveCharacter());
            }
        }
    }

    public void SetTargetDirection(Vector3 _Direction) {
        Vector3 targetLocation = transform.position + _Direction;
        PathRequestManager.RequestPath(transform.position, targetLocation, OnPathFound);
    }

    public void SetTargetDestination(Vector3 _Destination)
    {
        PathRequestManager.RequestPath(transform.position, _Destination, OnPathFound);
    }

    private void OnPathFound(Vector3[] _Path, bool _Success)
    {
        if(_Success)
        {
            targetPositionBuffer = _Path.ToList();
        }
    }
    private IEnumerator MoveCharacter()
    {
        isMoving = true;

        characterAnimation.SetIsMoving(true);

        float elapsedTimer = 0f;

        originalWorldPosition = transform.position;

        targetWorldPosition = targetPositionBuffer[0];
        targetPositionBuffer.RemoveAt(0);

        Vector3 direction = (targetWorldPosition - originalWorldPosition).normalized;

        characterAnimation.SetMovementDirection(direction);

        while(elapsedTimer < TimeToTarget)
        {
            transform.position = Vector3.Lerp(originalWorldPosition, targetWorldPosition, (elapsedTimer / TimeToTarget));
            elapsedTimer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetWorldPosition;
        originalWorldPosition = targetWorldPosition;

        remainingMovement -= WorldConstant.UnitMovementCost;

        isMoving = false;

        if(targetPositionBuffer.Count == 0)
        {
            characterAnimation.SetIsMoving(false);
        }
    }

    public void SetPath(List<Vector3> path)
    {
        if (isMoving) return;

        targetPositionBuffer = path;
    }
}
