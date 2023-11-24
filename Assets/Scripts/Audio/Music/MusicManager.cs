using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip checkpointMusic;
    public AudioClip beginnerMusic;
    public AudioClip advancedMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "CheckpointRaceDialogue":
                PlayMusic(mainMenuMusic);
                break;
            case "New Checkpoint Race":
                PlayMusic(checkpointMusic);
                break;
            case "BeginnerRaceDialogue":
                PlayMusic(mainMenuMusic);
                break;
            case "New Beginner Race":
                PlayMusic(beginnerMusic);
                break;
            case "AdvancedRaceDialogue":
                PlayMusic(mainMenuMusic);
                break;
            case "New Advanced Race":
                PlayMusic(advancedMusic);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
}
