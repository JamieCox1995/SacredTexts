using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : Addressable, IDamagable
{
    [Space]
    public CharacterRace CharacterRace;
    public CharacterClass CharacterClass;
    public CharacterData CharacterData;

    [Space]
    public CharacterAnimation CharacterAnimation;
    public CharacterMovement CharacterMovement;

    #region Unity Messages
    private void Start()
    {
        //InitializeCharacter();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightControl))
        {
            TakeDamage(2);
        }
    }
    #endregion

    #region Methods
    public void InitializeCharacter()
    {
        if(CharacterAnimation == null) { CharacterAnimation = GetComponent<CharacterAnimation>(); }

        if(CharacterMovement == null) { CharacterMovement = GetComponent<CharacterMovement>(); }

        if (CharacterMovement == null || CharacterAnimation == null)
        {
            Debug.LogError("Character is missing either the Character Animation or Character Movement Components.");
            return;
        }

        // Setting the max movement in the CharacterMovement component to be equal to the Character's race's speed stat.
        CharacterMovement.MaximumMovementPerTurn = CharacterRace.Speed;

        // We are going to rename the gameobject to the name of the character. This is only really for debugging in the Unity Editor
        gameObject.name = Name;

        // Now that the character is initialized, we are going to temporarily set the characters initiative
        System.Random rng = new System.Random();

        int initiativeRoll = rng.Next(1, 20);

        int initative = initiativeRoll + GetStatModifier(CharacterStatType.Dexterity);

        Director.Instance.RegisterCharacterInitiative(this, initative);
    }

    public int GetProficiencyBonus()
    {
        int bonus = 2;
        int level = CharacterData.Level;

        if (level >= 1 && level < 5) bonus = 2;
        else if (level >= 5 && level < 9) bonus = 3;
        else if (level >= 9 && level < 13) bonus = 4;
        else if (level >= 13 && level < 17) bonus = 5;
        else if (level >= 17) bonus = 6;

        return bonus;
    }

    public int GetUnmodifiedStat(CharacterStatType _StatType)
    {
        switch(_StatType)
        {
            default: 
                return 0;

            case CharacterStatType.Strength:
                return CharacterData.Stats.Strength;

            case CharacterStatType.Dexterity:
                return CharacterData.Stats.Dexterity;

            case CharacterStatType.Constitution:
                return CharacterData.Stats.Constitution;

            case CharacterStatType.Intelligence:
                return CharacterData.Stats.Intelligence;

            case CharacterStatType.Wisdom:
                return CharacterData.Stats.Wisdom;

            case CharacterStatType.Charisma:
                return CharacterData.Stats.Charisma;
        }
    }

    public int GetStat(CharacterStatType _StatType)
    {
        switch (_StatType)
        {
            default:
                return 0;

            case CharacterStatType.Strength:
                return CharacterData.Stats.Strength;

            case CharacterStatType.Dexterity:
                return CharacterData.Stats.Dexterity;

            case CharacterStatType.Constitution:
                return CharacterData.Stats.Constitution;

            case CharacterStatType.Intelligence:
                return CharacterData.Stats.Intelligence;

            case CharacterStatType.Wisdom:
                return CharacterData.Stats.Wisdom;

            case CharacterStatType.Charisma:
                return CharacterData.Stats.Charisma;
        }
    }

    public int GetStatModifier(CharacterStatType _StatType)
    {
        int statScore;

        switch (_StatType)
        {
            default:
                return 10;

            case CharacterStatType.Strength:
                statScore = CharacterData.Stats.Strength;
                break;

            case CharacterStatType.Dexterity:
                statScore = CharacterData.Stats.Dexterity;
                break;

            case CharacterStatType.Constitution:
                statScore = CharacterData.Stats.Constitution;
                break;

            case CharacterStatType.Intelligence:
                statScore = CharacterData.Stats.Intelligence;
                break;

            case CharacterStatType.Wisdom:
                statScore = CharacterData.Stats.Wisdom;
                break;

            case CharacterStatType.Charisma:
                statScore = CharacterData.Stats.Charisma;
                break;

        }

        return (statScore - 10) / 2;
    }

    public override string GetAddressableTooltip()
    {
        return $"{Name}, \nClass: {CharacterClass.ClassName} \tRace: {CharacterRace.RaceName}";
    }

    #endregion

    #region Interfaces
    public void TakeDamage(int hitPoints)
    {
        Debug.Log($"Took damage, {hitPoints.ToString()}");

        CharacterData.CurrentHitPoints -= hitPoints;

        if(CharacterData.CurrentHitPoints <= 0)
        {
            Debug.Log("Character has died.");
        }
    }

    #endregion
}

