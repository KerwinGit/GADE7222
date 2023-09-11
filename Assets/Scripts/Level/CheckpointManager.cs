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

    [SerializeField] private GameObject finishCheckpoint;
    public bool isFinished = false;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject defeatCanvas;

    [SerializeField] private Material activeMaterial;
    private Renderer checkpointRenderer;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;
    private float totalTime = 30f;
    private float timeToAdd = 3f;
    private float remainingTime;    

    private void Start()
    {
        remainingTime = totalTime;
        UpdateTimerUI();

        InitializeCheckpointStack();
        currentCheckpoint = checkpointStack.Pop();
        currentCheckpoint.GetComponent<BoxCollider>().enabled = true;

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
        else if (!isFinished)
        {
            Lose();
        }
    }

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
        currentCheckpoint.SetActive(false);
        AddTime(timeToAdd);

        if (!checkpointStack.IsEmpty())
        {            
            currentCheckpoint = checkpointStack.Pop();
            currentCheckpoint.GetComponent<BoxCollider>().enabled = true;
            checkpointRenderer = currentCheckpoint.GetComponent<Renderer>();
            checkpointRenderer.material = activeMaterial;
        }
        else
        {
            finishCheckpoint.SetActive(true);
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

    public void Lose()
    {
        isFinished = true;
        defeatCanvas.SetActive(true);
    }

    public void Win()
    {
        isFinished = true;
        victoryCanvas.SetActive(true);
    }

    
}
