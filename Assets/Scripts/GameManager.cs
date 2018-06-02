using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        loadAudio();
    }

    public void Continue()
    {
        PlayAudio("GameStart");
        Debug.Log("Continue feature not implemented yet.");
        Blackboard.Title.closeMenu();
    }

    public void NewGame()
    {
        PlayAudio("GameStart");
        Debug.Log("NewGame feature not implemented yet.");
        Blackboard.Title.closeMenu();
    }

    public void Save()
    {
        Debug.Log("Save feature not implemented yet.");
    }

    public void AdjustVolume()
    {
        Debug.Log("Adjust volume feature not implemented yet.");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayAudio(string clip)
    {
        if (audioDict.ContainsKey(clip))
        {
            audioSource.clip = audioDict[clip];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip: " + clip + " not found.");
        }
    }

    private void loadAudio()
    {
        object[] loaded_items = Resources.LoadAll("Audio");
        foreach (AudioClip clip in loaded_items)
        {
            if (!audioDict.ContainsKey(clip.name))
            {
                audioDict.Add(clip.name, clip);
            }
        }
    }
}
