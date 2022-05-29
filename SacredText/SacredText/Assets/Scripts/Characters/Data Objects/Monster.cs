using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Monster
{
    public string Name;
    public CharacterSize CharacterSize;
    public MonsterType MonsterType;
    public string[] Tags;
    public Alignment Alignment;
    public int NaturalArmourClass;
    public int HitPoints;
    public int Speed;
    public StatisticsBlock Stats;
    public List<CharacterStatType> SaveTypes;
    public List<CharacterRacialTrait> RacialTraits;
    public float ChallengeRating;
}

[Serializable]
public enum MonsterType
{
    Aberration, 
    Beast, 
    Celestial, 
    Construct, 
    Dragon, 
    Elemental, 
    Fey, 
    Fiend, 
    Giant, 
    Humanoid, 
    Monstrosity, 
    Ooze, 
    Plant, 
    Undead
}
