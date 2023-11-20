using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueDataSO", menuName = "Custom/Dialogue Data Scriptable Object")]
public class DialogueDataSO : ScriptableObject
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string characterName;
        [TextArea(3, 10)]
        public string dialogueLine;
        public Sprite charPortrait;
    }

    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
}
