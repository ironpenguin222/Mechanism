using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Character")]
public class Character : ScriptableObject
{
    public string charName;
    public int currentRank;
    public int currentAffection;
    public List<Dialogue> dialogues;
}
