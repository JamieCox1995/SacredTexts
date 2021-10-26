using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser
{

    public static Dictionary<string, WordType> Vocabulary;

    public static void ParseUserInput(string _UserInput) {
        // We are going to split the words down by then space character.
        string[] words = _UserInput.Split(' ');
        
        List<Word> WordTypes = new List<Word>();    // creating a list of words so that we can store the results later.

        // Now that we have an array of words that the player has entered, we want to try to determine what
        // types of words these are. This is so that we can determine what the player wanted to do.
        foreach(string s in words) {
            Word word = new Word {
                Text = s,
                WordType = GetWordType(s)
            };

            WordTypes.Add(word);
        }
    }

    // TODO: Implement a feature to store all of the possible words/commands that the game will accept to be entered.
    private static WordType GetWordType(string _Input) {

        if(Vocabulary.ContainsKey(_Input)) {
            return Vocabulary[_Input];
        }

        return WordType.Unknown;
    }

}

public class Word {
    public string Text;
    public WordType WordType;
}

public enum WordType {
    Unknown,
    Noun,
    Verb,
    Adverb
}
