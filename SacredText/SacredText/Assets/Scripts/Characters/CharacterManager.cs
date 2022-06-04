using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public Dictionary<string, Character> NonPlayableCharacters = new Dictionary<string, Character>();
    //public Dictionary<int, Monster> Creatures;

    // Temporary Object for spawning the player.
    public Character character;

    // Temporary List of NPCs for testing
    public List<Character> TemporaryNPCs = new List<Character>();

    public GameObject characterPrefab;
    //public Monster Monster;

    private Dictionary<string, CharacterRace> CharacterRaces;
    private Dictionary<string, CharacterClass> CharacterClasses;
    private Dictionary<string, CharacterData> CharacterData;

    // Start is called before the first frame update
    public void Initialize()
    {
        foreach(Character ch in TemporaryNPCs)
        {
            NonPlayableCharacters.Add(ch.Name, ch);
        }

        LoadClasses();
        LoadRaces();
        LoadCharacterStatisticsBlocks();

        NonPlayableCharacters.Add(character.Name, character);
    }

    private void LoadClasses()
    {
        CharacterClasses = new Dictionary<string, CharacterClass>();

        foreach (CharacterClass c in Resources.LoadAll<CharacterClass>(""))
        {
            CharacterClasses.Add(c.ClassName, c);
        }
    }

    private void LoadRaces()
    {
        CharacterRaces = new Dictionary<string, CharacterRace>();

        foreach(CharacterRace race in Resources.LoadAll<CharacterRace>(""))
        {
            CharacterRaces.Add(race.RaceName, race);
        }
    }

    private void LoadCharacterStatisticsBlocks()
    {
        CharacterData = new Dictionary<string, CharacterData>();

        foreach(CharacterData data in Resources.LoadAll<CharacterData>(""))
        {
            CharacterData.Add(data.name, data);
        }
    }

    private void LoadNPCs(string _Package)
    {
        
    }

    public GameObject SpawnCharacter(string _CharacterName)
    {
        // spawning the blank character prefab so that we can set up the data.
        GameObject spawnedCharacter = Instantiate(characterPrefab);

        Character characterData = NonPlayableCharacters[_CharacterName];

        // getting the CharacterObject from the prefab we spawned.
        CharacterObject obj = spawnedCharacter.GetComponent<CharacterObject>();

        if(obj == null)
        {
            Debug.LogError("Prefab did not contain a CharacterObject component.");
            return null;
        }

        obj.Name = characterData.Name;
        obj.CharacterClass = CharacterClasses[characterData.ClassName];
        obj.CharacterRace = CharacterRaces[characterData.RaceName];

        // now we want to check to see if the character has been spawned before. If they have, they will have a scriptable object created for their stats. If not, we will create one now.
        if(CharacterData.ContainsKey($"{_CharacterName} Data"))
        {
            obj.CharacterData = CharacterData[$"{_CharacterName} Data"];
        }
        else
        {
            // this is where we want to save the statistics blocks to.
            string characterDataFolder = "Assets/Resources/Core/Characters/StatisticsBlocks/";

            // creating a new scriptable object instance for the character and setting the name of the file.
            CharacterData chara = (CharacterData)ScriptableObject.CreateInstance("CharacterData");
            chara.name = characterData.Name + " Data";

            // setting the actual statistics.
            chara.Stats = characterData.Stats;

            // setting the spawned character's stats to the ones we have just created.
            obj.CharacterData = chara;

            // saving a physical file to the folder.
            AssetDatabase.CreateAsset(chara, $"{characterDataFolder}{chara.name}.asset");
            AssetDatabase.SaveAssets();
        }

        obj.InitializeCharacter();

        return spawnedCharacter;
    }

}
