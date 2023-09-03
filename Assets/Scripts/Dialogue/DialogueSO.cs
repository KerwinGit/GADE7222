using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueSO", menuName = "Custom/Dialogue Scriptable Object")]
public class DialogueSO : ScriptableObject
{
    public string[] names;
    [TextArea(3, 10)]
    public string[] dialogueLines;
}
