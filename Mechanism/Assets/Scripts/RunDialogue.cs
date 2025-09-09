using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunDialogue : MonoBehaviour
{
    public Dialogue[] dialogues;
    public TextMeshProUGUI dialogueTxt;
    public TextMeshProUGUI nameTxt;
    public Button choiceBTN;
    public RectTransform btnContainer;
    public Dialogue currDialogue;
    public int curAffection;
    public int dialogueIndex = 0;
    public GameObject rankup;
    public TextMeshProUGUI rankText;
    public int rank;
    // Start is called before the first frame update
    void Start()
    {
        currDialogue = dialogues[0];
        nameTxt.text = currDialogue.charName;
        dialogueTxt.text = currDialogue.dialogueText;
        Choices(currDialogue);

    }

    // Update is called once per frame
    void Update()
    {
        if (curAffection >= 5)
        {
            rank++;
            StartCoroutine(Rankup());
            curAffection = 0;
        }

        if (rank == 10)
        {
            rank = 0;
        }
    }

    IEnumerator Rankup()
    {
        rankup.SetActive(true);
        rankText.text = rank.ToString();
        yield return new WaitForSeconds(2f);
        rankup.SetActive(false);

        yield return null;
    }

    public void Choices(Dialogue dialogue)
    {
        foreach (Transform choices in btnContainer) {
        Destroy(choices.gameObject);
        
        }

        for (int i = 0; i < dialogue.choices.Count; i++) {
            int choiceIndex = i;
            Choices choice = dialogue.choices[i];
            Button b = Instantiate(choiceBTN, btnContainer);
            TMP_Text txt = b.GetComponentInChildren<TMP_Text>();
            txt.text = choice.optionText;
            b.onClick.AddListener(() => BtnChoice(choiceIndex));
        }
    }
    public void BtnChoice(int index)
    {
        Choices chosen = currDialogue.choices[index];
        if (chosen.givesAffection)
        {
            curAffection += chosen.affectionGiven;
        }
        dialogueIndex += 1;
        newDialogue();
    }

    public void newDialogue()
    {
        if (dialogueIndex >= dialogues.Length)
        {
            dialogueIndex = 0;
        }

        currDialogue = dialogues[dialogueIndex];
        nameTxt.text = currDialogue.charName;
        dialogueTxt.text = currDialogue.dialogueText;
        Choices(currDialogue);
    }
}
