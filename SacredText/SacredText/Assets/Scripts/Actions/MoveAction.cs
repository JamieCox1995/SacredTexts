using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Move")]
public class MoveAction : Action
{
    private CharacterMovement _CharacterMovement;
    private Dictionary<string, Vector3> cardinalDirections = new Dictionary<string, Vector3> {
        {"north", new Vector3(0, 0, 1)},
        {"east", new Vector3(1, 0, 0)},
        {"south", new Vector3(0, 0, -1)},
        {"west", new Vector3(-1, 0, 0)},
        {"northeast", new Vector3(1, 0, 1)},
        {"southeast", new Vector3(1, 0, -1)},
        {"southwest", new Vector3(-1, 0, -1)},
        {"northwest", new Vector3(-1, 0, 1)}    
    };

    public override void Initialize(GameObject _TargetObject)
    {
        // We want to get the CharacterMovement script attached to the target object
        _CharacterMovement = _TargetObject.GetComponent<CharacterMovement>();
    }

    public override string TriggerAction<T>(T _Params)
    {
        string returnMessage = "";

        // A single parameter move action is used to move the character a single
        // unit one of the cardinal directions (north, east, south, west, northeast, southeast, southwest, northwest)

        // First of all we want to check to see if the parameters have been passed in as a Word class
        // If the param is not a word class, we want to generate an exception so that we can feed it back to the user.
        try 
        {
            Word word = (Word)Convert.ChangeType(_Params, typeof(Word));

            // checking that the word is a noun
            if(word.WordType == WordType.Noun) {
                // Checking to see if the word is a cardinal direction.
                if(cardinalDirections.ContainsKey(word.Text)) {
                    // if the user has entered a correct cardinal direction, we want to move the character.
                    Vector3 direction = cardinalDirections[word.Text];

                    _CharacterMovement.SetTargetDirection(direction);
                }
                else {
                    // if the user has entered something other than a noun, we want to return an error
                    returnMessage = "Error: Move direction must be a cardinal direction.";
                    return returnMessage;
                }
            }
            else {
                // if it is not a noun, we want to return a error message
                returnMessage = "Error: Move direction must be a noun.";
                return returnMessage;
            }

        }
        catch (Exception ex)
        {
            // If this crashed, then the parameter is not a word class
            // We want to output this to the user.
            returnMessage = string.Format($"{0}, is not a recognised Word.", _Params.ToString());
        }

        return returnMessage;
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
