using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public GameObject character;

    public List<Action> ActionList = new List<Action>();

    public void Start() {
        foreach(Action action in ActionList) {
            action.Initialize(character);
        }
    }

    public bool GetAction(string _ActionName, out Action _Action) {
        bool result = false;
        _Action = null;

        _Action = ActionList.FirstOrDefault(a => a.ActionKeyword == _ActionName );

        result = _Action != null;

        return result;
    }
}
