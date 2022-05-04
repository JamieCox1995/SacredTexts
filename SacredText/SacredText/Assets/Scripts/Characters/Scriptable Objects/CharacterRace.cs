using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Characters/Character Race", order = 2), Serializable]
public class CharacterRace : ScriptableObject
{
    public string RaceName;
    [Multiline]
    public string RaceDescription;

    public CharacterSize CharacterSize;

    public float Speed;

    public List<CharacterRacialTrait> RacialTraits = new List<CharacterRacialTrait>();
}

[Serializable]
public struct CharacterRacialTrait
{
    public CharacterStatType AbilityStatType;
    public int StatModifier;
}

public enum CharacterSize
{
    Tiny,
    Small,
    Medium,
    Large,
    Huge,
    Gargantuan
}
