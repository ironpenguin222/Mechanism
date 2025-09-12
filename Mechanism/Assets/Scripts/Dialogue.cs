using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueEvent")]
public class Dialogue : ScriptableObject
{
    [TextArea] public string dialogueText;
    public List<Choices> choices;
}

[System.Serializable]
public class Choices
{
    [TextArea] public string optionText;
    public bool givesAffection;
    public int affectionGiven;
}
