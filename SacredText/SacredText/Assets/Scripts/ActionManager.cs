using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public List<Action> ActionList = new List<Action>();

    public bool GetAction(string _ActionName, out Action _Action) {
        bool result = false;
        _Action = null;

        _Action = ActionList.FirstOrDefault(a => a.ActionKeyword == _ActionName );

        result = _Action != null;

        return result;
    }
}
