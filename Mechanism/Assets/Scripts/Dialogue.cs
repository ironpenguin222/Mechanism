using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueEvent")]

// Creates a ext area for enterable text and choices
public class Dialogue : ScriptableObject
{
    [TextArea] public string dialogueText;
    public List<Choices> choices;
}

// Allows creation of choices with parameters to give affection

[System.Serializable]
public class Choices
{
    [TextArea] public string optionText;
    public bool givesAffection;
    public int affectionGiven;
}
