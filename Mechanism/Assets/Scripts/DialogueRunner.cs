using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    public List <Character> characters;
    public Character currentCharacter;
    public Dialogue[] dialogues;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject npcPortrait;
    public Button choiceButton;
    public RectTransform buttonContainer;
    public Dialogue currentDialogue;
    public int dialogueIndex = 0;
    public GameObject rankupUI;
    public TextMeshProUGUI rankText;
    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = characters[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCharacter.currentAffection >= 5)
        {
            currentCharacter.currentRank++;
            StartCoroutine(RankUp());
            currentCharacter.currentAffection = 0;
        }

        if (currentCharacter.currentRank == 10)
        {
            currentCharacter.currentRank = 0;
        }
    }

    IEnumerator RankUp()
    {
        rankupUI.SetActive(true);
        rankText.text = currentCharacter.currentRank.ToString();
        yield return new WaitForSeconds(2f);
        rankupUI.SetActive(false);

        yield return null;
    }

    public void ShowChoices(Dialogue dialogue)
    {
        foreach (Transform choices in buttonContainer) {
        Destroy(choices.gameObject);
        
        }

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
        if (currentCharacter.currentRank >= currentCharacter.rankedDialogues.Count)
        {
            return;
        }

        var dialogueForRank = currentCharacter.rankedDialogues[currentCharacter.currentRank].dialogues;

        if (dialogueIndex >= dialogueForRank.Count)
        {
            dialogueIndex = 0;
        }
        currentDialogue = dialogueForRank[dialogueIndex];
        nameText.text = currentCharacter.charName;
        dialogueText.text = currentDialogue.dialogueText;
        ShowChoices(currentDialogue);
    }
}
