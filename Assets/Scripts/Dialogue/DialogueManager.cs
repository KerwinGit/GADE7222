using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public Queue<string> dialogueQueue = new Queue<string>();
    public Queue<string> nameQueue = new Queue<string>();
    public Queue<Sprite> portraitQueue = new Queue<Sprite>();

    [Header("References")]
    public DialogueDataSO dialogueDataSO;    
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private UnityEngine.UI.Image portraitImage;
    [SerializeField] private Button continueBtn;

    private string charName;
    private string dialogue;
    private Sprite charPortrait;

    private Scene currentScene;

    private void Awake()
    {
        if (dialogueDataSO != null)
        {
            foreach (var entry in dialogueDataSO.dialogueEntries)//creates separate queues for the different variables stored in the dataSO
            {
                dialogueQueue.Enqueue(entry.dialogueLine);
                nameQueue.Enqueue(entry.characterName);
                portraitQueue.Enqueue(entry.charPortrait);
            }
        }

        LoadDialogueData();
    }

    private string GetNextDialogue()
    {
        if (!dialogueQueue.IsEmpty())
        {
            return dialogueQueue.Dequeue();
        }
        return null;
    }

    private string GetNextCharacterName()
    {
        if (!nameQueue.IsEmpty())
        {
            return nameQueue.Dequeue();
        }
        return null;
    }

    private Sprite GetNextCharacterSprite()
    {
        if (!portraitQueue.IsEmpty())
        {
            return portraitQueue.Dequeue();
        }
        return null;
    }

    private void LoadDialogueData() //loads next node from each queue
    {
        charName = GetNextCharacterName();
        dialogue = GetNextDialogue();
        charPortrait = GetNextCharacterSprite();

        StartCoroutine(TypeDialogue(dialogue));
    }

    private IEnumerator TypeDialogue(string text) //helper hethod which types the dialogue to the ui
    {
        continueBtn.interactable = false;       

        nameText.text = charName;
        dialogueText.text = "";
        portraitImage.sprite = charPortrait;
        foreach (char letter in text)
        {
            SFXManager.Instance.PlayAudio("dialog");
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Add a slight delay for typing effect
        }

        continueBtn.interactable = true;
    }

    public void ContinueClicked()//gets next dialogue or moves to new scene if dialogue queue is empty
    {
        if (!dialogueQueue.IsEmpty())
        {
            LoadDialogueData();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SkipDialogue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

