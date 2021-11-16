using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Move")]
public class MoveAction : Action
{
    private CharacterMovement _CharacterMovement;

    public override void Initialize(GameObject _TargetObject)
    {
        // We want to get the CharacterMovement script attached to the target object
        _CharacterMovement = _TargetObject.GetComponent<CharacterMovement>();
    }

    public override void TriggerAction<T>(T _Params)
    {
        // A single parameter move action is used to move the character a single
        // unit one of the cardinal directions (north, east, south, west, northeast, southeast, southwest, northwest)

        // First of all we want to check to see if the parameters have been passed in as a string, or as a Word class
        // If the param is not a word class, we want to generate it into one.
        //if()

        throw new System.NotImplementedException();
    }

    ///
    ///
    ///
    public override void TriggerAction<T>(List<T> _Params)
    {
        throw new System.NotImplementedException();
    }

    ///
    ///
    ///
    public override void TriggerAction<T>(T[] _Params)
    {
        throw new System.NotImplementedException();
    }
}
