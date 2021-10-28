using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseVocabulary", menuName = "SacredText/VocabularyObject", order = 0)]
public class VocabularyScriptableObject : ScriptableObject {
    public List<Word> Vocabulary;
}