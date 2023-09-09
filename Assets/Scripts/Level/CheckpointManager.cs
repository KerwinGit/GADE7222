using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public List<GameObject> checkpointList;
    private Stack<GameObject> checkpointStack = new Stack<GameObject>();
    public GameObject currentCheckpoint;

    [SerializeField] private Material activeMaterial;
    private Renderer checkpointRenderer;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;
    private float totalTime = 30f;
    private float timeToAdd = 5f;
    private float remainingTime;    

    private void Start()
    {
        remainingTime = totalTime;
        UpdateTimerUI();

        InitializeCheckpointStack();
        currentCheckpoint = checkpointStack.Pop();

        checkpointRenderer = currentCheckpoint.GetComponent<Renderer>();
        checkpointRenderer.material = activeMaterial;
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

    //private void OnEnable()
    //{
    //    foreach (GameObject checkpoint in checkpointList)
    //    {
    //        checkpoint.GetComponent<Checkpoint>().onCheckpointEnter.AddListener(PassCheckpoint);
    //    }
    //}

    //private void OnDisable()
    //{
    //    foreach (GameObject checkpoint in checkpointList)
    //    {
    //        checkpoint.GetComponent<Checkpoint>().onCheckpointEnter.RemoveListener(PassCheckpoint);
    //    }
    //}

    private void InitializeCheckpointStack()
    {
        foreach (GameObject checkpoint in checkpointList)
        {
            checkpointStack.Push(checkpoint);
        }
    }

    // Call this method when a checkpoint is passed.
    public void PassCheckpoint()
    {
        if (!checkpointStack.IsEmpty())
        {
            currentCheckpoint.SetActive(false);
            currentCheckpoint = checkpointStack.Pop();
            AddTime(timeToAdd);

            checkpointRenderer = currentCheckpoint.GetComponent<Renderer>();
            checkpointRenderer.material = activeMaterial;
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
}
