using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    [Header("Characters")]
    public List <Character> characters;
    public Character currentCharacter;

    [Header("Choices")]
    public Button choiceButton;
    public RectTransform buttonContainer;

    [Header("Dialogue Info")]
    public Dialogue currentDialogue;
    public int dialogueIndex = 0;

    [Header("Dialogue Visual")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image npcPortrait;

    [Header ("Rankup Screen")]
    public GameObject rankupUI;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI rankNameText;

    void Start()
    {
        //Starts the dialogue data as default

        currentCharacter = characters[0];
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(Character character)
    {
        // Makes sure that there is dialogue for that rank

        if (character.currentRank >= character.rankedDialogues.Count)
        {
            return;
        }

        // Sets up the dialogue data based on the character attached to the trigger script

        dialogueUI.SetActive(true);
        currentCharacter = character;
        npcPortrait.sprite = character.characterImage;
        dialogueIndex = 0;
        LoadDialogue();
    }

    void Update()
    {
        // Checks if should rank up

        if (currentCharacter.currentAffection >= 5)
        {
            currentCharacter.currentRank++;
            StartCoroutine(RankUp());
            currentCharacter.currentAffection = 0;
        }
    }

    IEnumerator RankUp()
    {
        // Shows the rankup UI for 2 seconds then disables UI

        rankupUI.SetActive(true);
        rankText.text = currentCharacter.currentRank.ToString();
        rankNameText.text = currentCharacter.characterName;
        yield return new WaitForSeconds(2f);
        rankupUI.SetActive(false);
        dialogueUI.SetActive(false);

        yield return null;
    }

    public void ShowChoices(Dialogue dialogue)
    {
        // Clears previous choices

        foreach (Transform choices in buttonContainer) {
        Destroy(choices.gameObject);
        
        }

        // Creates the choices. Adds based on amount of choices in the dialogue and adds a listener which progresses the dialogue based on the choice index

        for (int i = 0; i < dialogue.choices.Count; i++) {
            int choiceIndex = i;
            Choices choice = dialogue.choices[i];
            Button b = Instantiate(choiceButton, buttonContainer);
            TMP_Text txt = b.GetComponentInChildren<TMP_Text>();
            txt.text = choice.optionText;
            b.onClick.AddListener(() => HandleChoice(choiceIndex));
        }
    }
    public void HandleChoice(int index)
    {
        // Distributes the effects of the choice (affection/progression to next choice)

        Choices chosen = currentDialogue.choices[index];
        if (chosen.givesAffection)
        {
            currentCharacter.currentAffection += chosen.affectionGiven;
        }
        dialogueIndex += 1;
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        // Chooses which dialogue to give based on current rank

        var dialogueForRank = currentCharacter.rankedDialogues[currentCharacter.currentRank].dialogues;

        // Resets dialogue index upon reaching end of dialogue for that rank

        if (dialogueIndex >= dialogueForRank.Count)
        {
            dialogueIndex = 0;
        }

        // Sets information for current parameters of dialogue

        currentDialogue = dialogueForRank[dialogueIndex];
        nameText.text = currentCharacter.characterName;
        dialogueText.text = currentDialogue.dialogueText;
        ShowChoices(currentDialogue);
    }
}
