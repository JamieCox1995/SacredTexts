using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Characters/Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    [Tooltip("Measures: Natural Athleticism and bodily power")]
    public int Strength;
    [Tooltip("Measures: Physical agility, relfexes, balance, poise")]
    public int Dexterity;
    [Tooltip("Measures: Health, stamina, vital force")]
    public int Constitution;
    [Tooltip("Measures: Mental acurity, information recall, analytical skill")]
    public int Intelligence;
    [Tooltip("Measures: Awareness, intuition, insight")]
    public int Wisdom;
    [Tooltip("Measures: Confidence, eloquence, leadership")]
    public int Charisma;

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