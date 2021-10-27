using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommandHighlighter", menuName = "ScriptableObjects/CommandHighlighter", order = 1)]
public class CommandHighlighting : ScriptableObject
{
    public List<WordTypeColour> WordTypeColours;
}

[System.Serializable]
public class WordTypeColour {
    public WordType WordType;
    public Color WordColour;
}