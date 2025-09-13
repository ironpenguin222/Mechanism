using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]

// Create characters that store dialogue and required data
public class Character : ScriptableObject
{
    public string characterName;
    public int currentRank;
    public int currentAffection;
    public Sprite characterImage;
    public List<CharacterDialogue> rankedDialogues;
}

// Holds the dialogue in different sections to be called individually based on rank
[System.Serializable]
public class CharacterDialogue
{
    public List<Dialogue> dialogues = new List<Dialogue>();
}
