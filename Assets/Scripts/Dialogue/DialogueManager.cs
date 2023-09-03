using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("DialogueManager");
                    instance = obj.AddComponent<DialogueManager>();
                }
            }
            return instance;
        }
    }

    private Queue<string> nameQueue = new Queue<string>();
    private Queue<string> dialogueQueue = new Queue<string>();

    public DialogueSO dialogueSO;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    private string charName;
    private string dialogue;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        // Populate the queue from the ScriptableObject
        if (dialogueSO != null)
        {
            dialogueQueue.Clear();
            dialogueQueue = new Queue<string>(dialogueSO.dialogueLines);
            nameQueue = new Queue<string>(dialogueSO.names);
        }

        StartDialogue();
    }

    private void StartDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            string dialogue = dialogueQueue.Dequeue();
            charName = nameQueue.Dequeue();
            StartCoroutine(TypeDialogue(dialogue));
        }
        else
        {
            // Dialogue is finished
            Debug.Log("Dialogue is finished.");
        }
    }

    private IEnumerator TypeDialogue(string text)
    {
        nameText.text = charName;
        dialogueText.text = "";
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Add a slight delay for typing effect
        }
    }

    public void ContinueDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            StartDialogue();
        }
        else
        {
            // Dialogue is finished
            Debug.Log("Dialogue is finished.");
        }
    }

}

