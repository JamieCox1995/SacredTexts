using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Director : Singleton<Director>
{
    /*==========================================
     * ------------- THE DIRECTOR ------------- 
     * The director will be used to control the flow
     * of all Characters in the world. If the game
     * is in sequential play, all characters will move
     * when the player character ends a move, in intitive
     * play all characters will take turns in order of
     * their initiative for the encounter.
     * 
     * The Director will also be responsible for spawning
     * all of the characters and items to be in the world.
     ===========================================*/
    #region Variables
    [SerializeField]
    private List<CharacterObject> characters = new List<CharacterObject>();

    [SerializeField]
    private List<CharacterObject> playableCharacters = new List<CharacterObject>();

    private List<CharacterInitiative> InitiativeOrder = new List<CharacterInitiative>();

    public GameFlowType GameFlowType { get; private set; }
    #endregion

    #region Unity Messages
    private void Start()
    {
        CharacterManager.Instance.Initialize();

        SpawnStartingCharacter();
    }
    #endregion

    #region Events
    /// <summary>
    /// Even fired when a player controlled character has performed an action.
    /// If the game is running sequentially, all other characters
    /// will move or perform an action.
    /// </summary>
    public static event Action<CharacterObject> onCharacterActionPerformed;

    public static event Action<GameObject> onActiveCharacterUpdated;
    #endregion

    #region Methods
    public List<CharacterObject> GetPlayableCharacters() { return characters; }

    public void RegisterCharacterInitiative(CharacterObject _Character, int _Initiative)
    {
        if(_Character.CharacterName == "Regner")
        {
            _Initiative = 1;
        }


        InitiativeOrder.Add(new CharacterInitiative { Character = _Character, Initiative = _Initiative });

        Debug.Log($"Registered an initiative of {_Initiative} for {_Character.CharacterName}");
    }

    #region Spawning Characters

    private void SpawnStartingCharacter()
    {
        if (SpawnPlayerCharacter("John Smith", Vector3.zero, out GameObject go))

        SpawnCharacter("Regner", new Vector3(5, 0, 5), out go);

        InitiativeOrder.Sort((c1, c2) => c1.Initiative.CompareTo(c2.Initiative));

        if (onActiveCharacterUpdated != null) onActiveCharacterUpdated.Invoke(InitiativeOrder[InitiativeOrder.Count - 1].Character.gameObject);
    }

    public bool SpawnCharacter(string _CharacterName, Vector3 _Location, out GameObject _SpawnedCharacter)
    {
        _SpawnedCharacter = CharacterManager.Instance.SpawnCharacter(_CharacterName);

        if (_SpawnedCharacter == null) return false;

        _SpawnedCharacter.AddComponent<AIControllable>().Initialize();

        characters.Add(_SpawnedCharacter.GetComponent<CharacterObject>());

        _SpawnedCharacter.transform.position = _Location;

        Grid.Instance.UpdateCellWalkable(_Location, false);

        return true;
    }

    public bool SpawnPlayerCharacter(string _CharacterName, Vector3 _Location, out GameObject _SpawnedCharacter)
    {
        _SpawnedCharacter = CharacterManager.Instance.SpawnCharacter(_CharacterName);

        if (_SpawnedCharacter == null) return false;

        _SpawnedCharacter.AddComponent<PlayerControllable>().Initialize();

        CharacterObject co = _SpawnedCharacter.GetComponent<CharacterObject>();

        characters.Add(co);
        playableCharacters.Add(co);

        _SpawnedCharacter.transform.position = _Location;

        Grid.Instance.UpdateCellWalkable(_Location, false);

        return true;
    }
    #endregion
    #endregion
}

public enum GameFlowType
{
    Sequential,
    Initiative
}

public class CharacterInitiative
{
    public CharacterObject Character;
    public int Initiative;
}
