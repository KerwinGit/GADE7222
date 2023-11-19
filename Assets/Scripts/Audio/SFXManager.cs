using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource aSource;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private Dictionary<string, AudioClip> audioMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        aSource = GetComponent<AudioSource>();

        audioMap = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in clips)
        {
            audioMap.Add(clip.name, clip);
        }

    }

    void Start()
    {

    }

    public void PlayAudio(string name)
    {
        if (audioMap.ContainsKey(name))
        {
            aSource.clip = audioMap[name];
            aSource.Play();
            Debug.Log("Play");
            //AudioSource.PlayClipAtPoint(audioMap[name], transform.position);
        }
        else
        {
            Debug.Log("Sound " + name + " not found");
        }
    }
}
