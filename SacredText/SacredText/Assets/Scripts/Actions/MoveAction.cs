using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public override string TriggerAction<T>(List<T> _Params)
    {
        string returnMessage = "";
        List<Word> words = _Params.Cast<Word>().ToList();

        if(words.Count == 2) // "move to [{coordinate}]" or "move towards {object}"
        {
            // getting the last word as it is the one which actually lets us know where we want to move to.
            Word target = words[1];

            switch(target.WordType)
            {
                case WordType.Special:
                    // if the last word is a special, then we want to try to parse the coordinates
                    // we are expecting the coordinated to be in the format  "[X, Y]"
                    string coord = target.Text;
                    coord = coord.Replace("[", "");
                    coord = coord.Replace("]", "");

                    string[] coords = coord.Split(',');
                    int x, y;

                    x = int.Parse(coords[0]);
                    y = int.Parse(coords[1]);

                    Vector3 position = new Vector3(x, 0, y);

                    _CharacterMovement.SetTargetDestination(position);

                    break;

                case WordType.Noun:
                    // if the last word is a noun, we want to see if there is an object with the name
                    break;
            }
        }

        if (words.Count == 3) // "move {distance} feet {direction}"
        {
            // First we are going to do some pattern recognition, to ensure that the 
            if(words[0].WordType == WordType.Numeric && words[1].Text == "feet" && words[2].WordType == WordType.Noun)
            {
                // First of all we are going to convert the {distance} feet into the number of units that the player wants to move.
                double feetDistance = double.Parse(words[0].Text);
                int unitDistance = (int)feetDistance / WorldConstant.UnitMovementCost;

                // Then we are going to check that the player has entered a cardinal direction to move.
                string direction = words[2].Text;

                if (cardinalDirections.ContainsKey(direction))
                {
                    // Getting the direction that the player has entered and multiplying it by the distance we want to move.
                    Vector3 moveDirection = cardinalDirections[direction];
                    moveDirection = moveDirection * unitDistance;

                    _CharacterMovement.SetTargetDirection(moveDirection);
                }
                else
                {
                    // if the user has entered something other than a noun, we want to return an error
                    returnMessage = "Error: Move direction must be a cardinal direction.";
                    return returnMessage;
                }
            }
        }

        return returnMessage;
    }

    ///
    ///
    ///
    public override void TriggerAction<T>(T[] _Params)
    {
        throw new System.NotImplementedException();
    }
}
