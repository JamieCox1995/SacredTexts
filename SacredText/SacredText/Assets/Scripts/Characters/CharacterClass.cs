using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Characters/Character Class", order = 3)]
public class CharacterClass : ScriptableObject
{
    public string ClassName;
    public string ClassDescription;

    public int HitDice;

    public CharacterStatType PrimaryAbility;

    public List<CharacterStatType> SaveTypes;
}
