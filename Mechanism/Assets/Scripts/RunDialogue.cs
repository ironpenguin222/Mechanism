using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    public Dialogue[] dialogues;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Button choiceButton;
    public RectTransform buttonContainer;
    public Dialogue currentDialogue;
    public int currentAffection;
    public int dialogueIndex = 0;
    public GameObject rankupUI;
    public TextMeshProUGUI rankText;
    public int currentRank;
    // Start is called before the first frame update
    void Start()
    {
        LoadDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentAffection >= 5)
        {
            currentRank++;
            StartCoroutine(RankUp());
            currentAffection = 0;
        }

        if (currentRank == 10)
        {
            currentRank = 0;
        }
    }

    IEnumerator RankUp()
    {
        rankupUI.SetActive(true);
        rankText.text = currentRank.ToString();
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
            currentAffection += chosen.affectionGiven;
        }
        dialogueIndex += 1;
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        if (dialogueIndex >= dialogues.Length)
        {
            dialogueIndex = 0;
        }

        currentDialogue = dialogues[dialogueIndex];
        nameText.text = currentDialogue.charName;
        nameText.text = currentDialogue.dialogueText;
        ShowChoices(currentDialogue);
    }
}
