using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandParser
{
    public static Dictionary<string, WordType> Vocabulary;

    public static CommandParser Instance;

    public void Initialize() {
        if(Instance != null) {
            return;
        }

        Instance = this;

        InitializeVocabulary();
    }

    public void InitializeVocabulary() {
        InitializeCoreVocabulary();
    }

    private void InitializeCoreVocabulary() {
        // Here we want to add in the words that every game/level will want to know.
        Vocabulary = new Dictionary<string, WordType>();

        // We are going to load in a Scriptable object from the Resources Folder.
        VocabularyScriptableObject vocabularyScriptableObject = Resources.Load<VocabularyScriptableObject>("Core/BaseVocabulary");

        if (vocabularyScriptableObject != null) {
            foreach(Word word in vocabularyScriptableObject.Vocabulary) {
                Vocabulary.Add(word.Text, word.WordType);
            }
        }
        else {
            Debug.LogError("Unable to load Base Vocabulary!");
        }
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

    // TODO: Implement a feature to store all of the possible words/commands that the game will accept to be entered.
    private static WordType GetWordType(string _Input) {

        if(Vocabulary.ContainsKey(_Input)) {
            return Vocabulary[_Input];
        }

        return WordType.Unknown;
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
    Unknown,
    Error
}
