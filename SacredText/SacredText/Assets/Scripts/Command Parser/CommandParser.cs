using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser
{

    public static Dictionary<string, WordType> Vocabulary;
    public CommandHighlighting CommandHighlighting;

    public void InitializeVocabulary() {
        InitializeCoreVocabulary();
    }

    private void InitializeCoreVocabulary() {
        // Here we want to add in the words that every game/level will want to know.
    }

    public static List<Word> ParseUserInput(string _UserInput, out string _ReturnMessage) {
        // We are going to split the words down by then space character.
        string[] words = _UserInput.Split(' ', '.');
        
        _ReturnMessage = "";
        List<Word> WordTypes = new List<Word>();    // creating a list of words so that we can store the results later.

        // Now that we have an array of words that the player has entered, we want to try to determine what
        // types of words these are. This is so that we can determine what the player wanted to do.
        foreach(string s in words) {
            Word word = new Word {
                Text = s,
                WordType = GetWordType(s)
            };

            if(word.WordType != WordType.Article)
                WordTypes.Add(word);
        }

        Word? errorWord = WordTypes.First(w => w.WordType == WordType.Unknown);

        if(errorWord != null) {
            _ReturnMessage = $"{errorWord.Text}: Word is not recognised.";
        }

        return WordTypes;
    }

    public string HighlightCommandText(string _UserInput) {
        string[] words = _UserInput.Split(' ', ',');
        string compliedInput = "";
        // We want to go through the text that has been entered.
        foreach(string word in words) {
            WordType wordType = GetWordType(word);
            
            WordTypeColour colour = CommandHighlighting.WordTypeColours.First(w => w.WordType == wordType);
            //colour.WordColour.


            //compliedInput += "<color=''"
        }

        return compliedInput;
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
    Noun,
    Verb,
    Adjective,
    Conjunction,
    Article,
    Preposition,
    Unknown,
    Error
}
