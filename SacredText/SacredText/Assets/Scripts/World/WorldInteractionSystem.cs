using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

///
///     The World Intreraction System (WIS) is what will be used to parse commnads from the mouse
///     and text input system into methods and actions in game.
///

public class WorldInteractionSystem : Singleton<WorldInteractionSystem>
{
    public TMP_InputField PlayerInput;
    public TMP_Text ChatBox;
    public GameObject AddressableDialog;
    public Canvas canvas;

    private ActionManager ActionManager;

    private List<string> ChatMessages = new List<string>();
    private int MaxMessagesToDisplay = 5;

    private CommandParser Parser;

    private GameObject addressableDialog;

    // Start is called before the first frame update
    void Start()
    {
        Parser = new CommandParser();

        ActionManager = ActionManager.Instance;

        if (PlayerInput != null) PlayerInput.onEndEdit.AddListener(OnTextEntered);

        Director.onActiveCharacterUpdated += OnActiveCharacterUpdated;

        foreach(Action action in ActionManager.Instance.ActionList)
        {
            action.onActionPerformed += AddMessageToChatBox;
        }
    }

    public void OnTextEntered(string value)
    {
        List<Word> commands = new List<Word>();

        // Calling the parser to get the list of commands
        string result = Parser.ParseCommand(value, out commands);

        // Checking the result of the command parser. If the result is empty, we know that there was no issue identifying the commands.
        if (string.IsNullOrWhiteSpace(result))
        {
            // Debugging the output of the parser
            foreach (Word word in commands)
            {
                Debug.Log($"{word.Text}: {word.WordType} was detected from the user's input.");
            }

            // We want to get the verb/action from the command list. This is so that we can then go through the list of the player's actions (list of scriptable objects) to see what they can do.
            Word verb = commands.Find(w => w.WordType == WordType.Verb);

            // We want to get the action from a list of the Actions (to be created)
            if (ActionManager.GetAction(verb.Text, out Action action))
            {
                if (commands.Count == 2)
                {
                    result = action.TriggerAction(commands[1]);
                }
                else
                {
                    // Generate a list of parameters and pass it to the action
                    List<Word> param = new List<Word>();
                    param.AddRange(commands.FindAll(c => c.WordType != WordType.Verb));

                    result = action.TriggerAction(param);
                }

            }
        }
        else
        {
            Debug.Log(result);
        }

        AddMessageToChatBox(result);
    }

    public void AddMessageToChatBox(string _Message)
    {
        if (ChatMessages.Count > MaxMessagesToDisplay)
        {
            ChatMessages.RemoveAt(0);
        }

        ChatMessages.Add(_Message);

        ChatBox.text = "";

        foreach (string message in ChatMessages)
        {

            ChatBox.text += $"> {message}\n";
        }
    }

    public void OnActiveCharacterUpdated(GameObject _ActiveCharacter)
    {
        // Checking to see if the player that is active is a player controllable character
        if(_ActiveCharacter.TryGetComponent<PlayerControllable>(out PlayerControllable playerControllable))
        {
            // if it is, we want to enable the text input
            PlayerInput.enabled = true;
            PlayerInput.gameObject.SetActive(true);
        }
        else
        {
            PlayerInput.enabled = false;
            PlayerInput.gameObject.SetActive(false);
        }

    }

    public void OnDisplayMouseOverInformation(Addressable _Addressable)
    {
        // We want to display information for the addressable. This will be split into Characters and Items.
        // Characters will display information such as names, race, class, etc.
        // Items will display name, item type, weight, cost.

        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 directionFromScreenCentre = new Vector3(Screen.width / 2, 0, Screen.height / 2) - mouseScreenPosition;

        Vector3 newPosition = mouseScreenPosition + (directionFromScreenCentre.normalized * 75);

        if (addressableDialog == null)
        {
            InterfaceManager.Instance.CreateInterfaceObject("AddressableDialog", newPosition, out addressableDialog);

            addressableDialog.GetComponent<InterfaceElement>().Text.text = _Addressable.GetAddressableTooltip();

            if(_Addressable is CharacterObject)
                addressableDialog.GetComponent<InterfaceElement>().Text.text = ((CharacterObject)_Addressable).GetAddressableTooltip();
        }
    }

    public void OnHideMouseOverInformation()
    {
        // Hiding the mouse over information dialog box
        InterfaceManager.Instance.DestroyInterfaceObject("AddressableDialog");
    }
}
