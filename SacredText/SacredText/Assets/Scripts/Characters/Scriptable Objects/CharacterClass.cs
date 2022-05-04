using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Characters/Character Class", order = 3), Serializable]
public class CharacterClass : ScriptableObject
{
    public string ClassName;
    [Multiline]
    public string ClassDescription;

    public int HitDice;

    public CharacterStatType PrimaryAbility;

    public List<CharacterStatType> SaveTypes;

    public List<ItemProficiency> ItemProficiencies;
}

[Serializable]
public class ItemProficiency
{
    public ItemType ItemType;
    public string Name;
}

[Serializable]
public enum ItemType
{
    Armour,
    Tool,
    Weapon
}