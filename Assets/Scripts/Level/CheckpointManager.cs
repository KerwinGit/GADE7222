using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float totalTime = 20f;
    private float timeToAdd = 2f;
    private float remainingTime;    

    private void Start()
    {
        //timer
        remainingTime = totalTime;
        UpdateTimerUI();

        //checkpoint stack handling
        InitializeCheckpointStack();
        currentCheckpoint = checkpointStack.Pop();
        currentCheckpoint.GetComponent<BoxCollider>().enabled = true;

        //updating current checkpoint material
        checkpointRenderer = currentCheckpoint.GetComponent<Renderer>();
        checkpointRenderer.material = activeMaterial;
    }

    private void Update()
    {
        if (remainingTime > 0)//updates timer
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else if (!isFinished)//lose if timer drops to 0 and game isn't finished
        {
            Lose();
        }
    }

    private void InitializeCheckpointStack()//reads checkpoints from list to stack
    {
        foreach (GameObject checkpoint in checkpointList)
        {
            checkpointStack.Push(checkpoint);
        }
    }

    public void PassCheckpoint()//sets checkpoint to inactive, pops new checkpoint and adds time
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

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    public void AddTime(float secondsToAdd)
    {
        remainingTime += secondsToAdd;
        UpdateTimerUI();
    }

    public void Lose()//lose
    {
        isFinished = true;
        defeatCanvas.SetActive(true);
    }

    public void Win()//win
    {
        isFinished = true;
        victoryCanvas.SetActive(true);
    }

    
}
