using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string ActionName = "New Action";
    public string ActionHelpDescription = "help description";
    public string ActionKeyword = "";

    public abstract void Initialize(GameObject _TargetObject);
    public abstract string TriggerAction<T>(T _Params);

    public abstract string TriggerAction<T>(List<T> _Params);

    public abstract void TriggerAction<T>(T[] _Params);
}
