using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector3 movementDirection;
    private bool isMoving;

    private void Start()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    public void SetMovementDirection(Vector3 _Direction)
    {
        if (_Direction.y > 0) _Direction.y = 0;

        movementDirection = _Direction;
        OnValueChanged();
    }

    public void SetIsMoving(bool _IsMoving)
    {
        isMoving = _IsMoving;
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("XDirection", movementDirection.z);
    }

}
