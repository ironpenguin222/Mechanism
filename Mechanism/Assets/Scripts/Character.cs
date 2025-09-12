using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string charName;
    public int currentRank;
    public int currentAffection;
    public Sprite characterImage;
    public List<CharacterDialogue> rankedDialogues;
}

[System.Serializable]
public class CharacterDialogue
{
    public List<Dialogue> dialogues = new List<Dialogue>();
}
