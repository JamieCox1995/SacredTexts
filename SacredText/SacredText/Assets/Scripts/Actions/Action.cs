using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string ActionName = "New Action";
    public string ActionHelpDescription = "help description";
    public string ActionKeyword = "";

    public abstract void Initialize();
    public abstract void TriggerAction<T>(T param);

    public abstract void TriggerAction<T>(List<T> param);

    public abstract void TriggerAction<T>(T[] param);
}
