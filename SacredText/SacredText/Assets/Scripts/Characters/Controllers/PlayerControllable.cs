using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllable : MonoBehaviour, IControllable
{
    public bool IsActiveCharacter = false;

    private CharacterObject character;
    public void Initialize()
    {
        character = GetComponent<CharacterObject>();

        Director.onActiveCharacterUpdated += OnActiveCharacterUpdated;
    }

    public void OnActiveCharacterUpdated(GameObject _CharacterObject)
    {
        IsActiveCharacter = (_CharacterObject.gameObject == character.gameObject);
    }
}
