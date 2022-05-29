using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    public GameObject activeCharacter;

    public List<Action> ActionList = new List<Action>();

    protected override void Awake() {
        base.Awake();

        Director.onActiveCharacterUpdated += OnActiveCharacterUpdated;
    }

    public bool GetAction(string _ActionName, out Action _Action) {
        bool result = false;
        _Action = null;

        _Action = ActionList.FirstOrDefault(a => a.ActionKeyword == _ActionName );

        result = _Action != null;

        return result;
    }

    public void OnActiveCharacterUpdated(GameObject _ActiveCharacter)
    {
        activeCharacter = _ActiveCharacter;

        foreach (Action action in ActionList)
        {
            action.Initialize(activeCharacter);
        }
    }
}
