using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Characters/Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public StatisticsBlock Stats;

    [Header("Character Health"), Space]
    public int MaximumHitPoints;
    public int CurrentHitPoints;

    [Header("Character Experience"),Space]
    public int Experience;
    public int Level;
}

public enum CharacterStatType
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma
}