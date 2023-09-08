using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public List<GameObject> checkpointList;
    private Stack<GameObject> checkpointStack = new Stack<GameObject>();
    private GameObject currentCheckpoint;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float totalTime = 60f;
    [SerializeField] private float timeToAdd = 5f;
    private float remainingTime;    

    private void Start()
    {
        remainingTime = totalTime;
        UpdateTimerUI();

        InitializeCheckpointStack();
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            Debug.Log("Time's up!");
        }
    }

    // Update the timer UI text.
    private void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    // Method to add seconds to the remaining timer.
    public void AddTime(float secondsToAdd)
    {
        remainingTime += secondsToAdd;
        UpdateTimerUI();
    }

    private void InitializeCheckpointStack()
    {        
        foreach (GameObject checkpoint in checkpointList)
        {
            checkpointStack.Push(checkpoint);
        }
    }

    // Pop the stack to find the next checkpoint.
    public GameObject GetNextCheckpoint()
    {
        if (!checkpointStack.IsEmpty())
        {
            return checkpointStack.Peek();
        }
        return null;
    }

    // Call this method when a checkpoint is passed.
    public void PassCheckpoint()
    {
        if (!checkpointStack.IsEmpty())
        {
            checkpointStack.Pop();
            AddTime(timeToAdd);
        }
    }
}
