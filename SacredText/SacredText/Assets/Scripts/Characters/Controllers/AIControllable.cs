using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllable : MonoBehaviour, IControllable
{
    public bool IsActiveCharacter = false;

    private ActionManager actionManager;
    private CharacterObject character;

    public void Initialize()
    {
        character = GetComponent<CharacterObject>();
        actionManager = ActionManager.Instance;

        Director.onActiveCharacterUpdated += OnActiveCharacterUpdated;
    }

    private void Update()
    {
        if(IsActiveCharacter)
        {
            if(actionManager.GetAction("move", out Action action))
            {
                // getting the location of the character and telling it to move to a random location within 5 blocks
                Vector3 location = transform.position;
                Vector3 dir = Random.insideUnitSphere * 5;
                Vector3 targetLoction = transform.position + new Vector3(dir.x, 0, dir.z);

                action.TriggerAction(new List<Word> { new Word { Text = "to", WordType = WordType.Preposition }, new Word { Text = $"[{(int)dir.x},{(int)dir.z}]", WordType = WordType.Special } });

                IsActiveCharacter = false;
            }
        }
    }

    public void OnActiveCharacterUpdated(GameObject _CharacterObject)
    {
        IsActiveCharacter = (_CharacterObject.gameObject == character.gameObject);
    }
}
