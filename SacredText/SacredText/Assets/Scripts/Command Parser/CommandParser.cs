using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CommandParser
{
    public Dictionary<string, WordType> Vocabulary;

    public CommandParser() {
        // We want to initialise the Vocabulary Dictionary
        InitializePredefinedVocabulary();
    }

    private void InitializePredefinedVocabulary() {
        Vocabulary = new Dictionary<string, WordType>();

        Vocabulary.Add("move", WordType.Verb);

        Vocabulary.Add("north", WordType.Noun);
        Vocabulary.Add("east", WordType.Noun);
        Vocabulary.Add("south", WordType.Noun);
        Vocabulary.Add("west", WordType.Noun);
        Vocabulary.Add("northeast", WordType.Noun);
        Vocabulary.Add("southeast", WordType.Noun);
        Vocabulary.Add("southwest", WordType.Noun);
        Vocabulary.Add("northwest", WordType.Noun);

        Vocabulary.Add("feet", WordType.Noun);
    }

    public string ParseCommand(string _UserInput, out List<Word> _Commands) {
        _Commands = new List<Word>();
        List<string> stringList = new List<string>();;
        string returnMessage = "";

        // We are going to convert the input string to lower case so that we don't have to worry about having multiple words with differing cases
        _UserInput = _UserInput.Trim().ToLower();

        if(_UserInput == "") {
            returnMessage = "Enter a command.";
            return returnMessage;
        }

        stringList = new List<string>(_UserInput.Split(new char[] {' ', '.'}));

        returnMessage = IdentifyCommands(stringList, out _Commands);

        return returnMessage;
    }

    public string IdentifyCommands(List<string> _StringList, out List<Word> _Commands) {
        string returnMessage = "";

        _Commands = new List<Word>();
        WordType wordType;
        Regex regex = new Regex(@"(\[\d+,\s*\d+\])");

        foreach(string s in _StringList) {
            if(Vocabulary.ContainsKey(s)) {
                wordType = Vocabulary[s];

                if(wordType != WordType.Article) {
                    _Commands.Add(new Word {
                        Text = s,
                        WordType = wordType
                    });
                }
            } 
            else {
                // Checking to see if fa number has been entered by the player.
                if (double.TryParse(s, out _))
                {
                    _Commands.Add(new Word
                    {
                        Text = s,
                        WordType = WordType.Numeric
                    });
                }
                else if (regex.IsMatch(s))
                {
                    _Commands.Add(new Word
                    {
                        Text = s,
                        WordType = WordType.Special
                    });
                }    
                else
                {
                    _Commands.Add(new Word
                    {
                        Text = s,
                        WordType = WordType.Error
                    });

                    returnMessage = $"Cannot understand the command: {s}";
                }
            }
        }

        return returnMessage;
    }

}

[System.Serializable]
public class Word {
    public string Text;
    public WordType WordType;
}

public enum WordType {
    Noun,
    Verb,
    Adjective,
    Conjunction,
    Article,
    Preposition,
    Numeric,
    Special,
    Unknown,
    Error
}
