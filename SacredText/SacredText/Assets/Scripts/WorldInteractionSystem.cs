using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

///
///     The World Intreraction System (WIS) is what will be used to parse commnads from the mouse
///     and text input system into methods and actions in game.
///

public class WorldInteractionSystem : MonoBehaviour
{
    public TMP_InputField PlayerInput;
    public TMP_Text ChatBox;

    public ActionManager ActionManager;

    private Queue<string> ChatMessages = new Queue<string>();
    private int MaxMessagesToDisplay = 5;

    private CommandParser Parser;

    // Start is called before the first frame update
    void Start()
    {
        Parser = new CommandParser();

        if(PlayerInput != null) PlayerInput.onEndEdit.AddListener(OnTextEntered);
    }

    public void OnTextEntered(string value) {
        List<Word> commands = new List<Word>();

        // Calling the parser to get the list of commands
        string result = Parser.ParseCommand(value, out commands);

        // Checking the result of the command parser. If the result is empty, we know that there was no issue identifying the commands.
        if(string.IsNullOrWhiteSpace(result)) {
            // Debugging the output of the parser
            foreach(Word word in commands) {
                Debug.Log($"{word.Text}: {word.WordType} was detected from the user's input.");
            }            

            // We want to get the verb/action from the command list. This is so that we can then go through the list of the player's actions (list of scriptable objects) to see what they can do.
            Word action = commands.Find(w => w.WordType == WordType.Verb);

            // We want to get the action from a list of the Actions (to be created)
            if(ActionManager.GetAction(action.Text, out Action action1))
            {
                if(commands.Count == 2)
                {
                    action1.TriggerAction(commands[1]);
                }
                else
                {
                    // Generate a list of parameters and pass it to the action
                    List<Word> param = new List<Word>();
                    param.AddRange(commands.FindAll(c => c.WordType != WordType.Verb));

                    action1.TriggerAction(param);
                }

            }
        }
        else {
            Debug.Log(result);
        }

        AddMessageToChatBox(result);
    }

    public void AddMessageToChatBox(string _Message) {
        if(ChatMessages.Count > MaxMessagesToDisplay) {
            ChatMessages.Dequeue();
        }

        ChatMessages.Enqueue(_Message);

        string[] chatArray = ChatMessages.ToArray();
        ChatBox.text = "";

        foreach(string message in chatArray) {
            ChatBox.text += message;
            ChatBox.text += "\n";
        }
    }
}
