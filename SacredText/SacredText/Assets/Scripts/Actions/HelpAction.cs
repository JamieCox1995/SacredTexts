using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Help")]
public class HelpAction : Action
{
    public override void Initialize(GameObject _TargetObject)
    {

    }

    public override string TriggerAction<T>(T _Params)
    {
        throw new System.NotImplementedException();
    }

    public override string TriggerAction<T>(List<T> _Params)
    {
        string returnMessage = "";

        List<Word> words = _Params.Cast<Word>().ToList();

        if(words.Count == 0)
        {
            // if there no parameters passes in, we want to output all of the actions help params.
            foreach(Action action in ActionManager.Instance.ActionList)
            {
                returnMessage += $" {action.ActionName} - {action.ActionHelpDescription}\n";
            }
        }

        return returnMessage;
    }

    public override void TriggerAction<T>(T[] _Params)
    {
        throw new System.NotImplementedException();
    }
}
