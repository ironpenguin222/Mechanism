using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triggers dialogue when player interacts with NPC
public class NPCTrigger : MonoBehaviour
{
    public Character character;
    public bool playerInRange = false;
    public DialogueRunner dialogue;

    // Checks if player is in range and presses E
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogue.StartDialogue(character);
        }
    }

    // Changes variable wheteher player is in range or not
    private void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }
}
