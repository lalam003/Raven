using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;
    public Image BlackScreen;
    public delegate void CoroutineAction();
    public SaveLoadButton saveButton;

    private void Awake()
    {
        Blackboard.GM = this;
        audioSource = GetComponent<AudioSource>();
        TimeSystem.timeEvents = new Dictionary<int, System.Action>();
        loadAudio();
        saveButton = new SaveLoadButton();
    }

    public void Continue()
    {
        print("continuing");
        if (!saveButton.Load())
        {
            NewGame();
        }
        PlayAudio("GameStart");
        Blackboard.Title.closeMenu();
    }

    public void NewGame()
    {
        PlayAudio("GameStart");
        saveButton.Delete();
        Blackboard.Title.closeMenu();
    }

    public void Save()
    {
        saveButton.Save();
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

    public void StartFade(float time, CoroutineAction midFade, CoroutineAction afterFade)
    {
        Blackboard.Player.PlayerMovement.canMove = false;
        StartCoroutine(Fade(time, midFade, afterFade));
    }

    private IEnumerator Fade(float time, CoroutineAction midFade, CoroutineAction afterFade)
    {
        float startTime = Time.time;
        while ((Time.time - startTime) < time)
        {
            float frac = ((Time.time - startTime) / time);
            Blackboard.GM.BlackScreen.color = new Color(Blackboard.GM.BlackScreen.color.r, Blackboard.GM.BlackScreen.color.g, Blackboard.GM.BlackScreen.color.b, frac);
            yield return null;
        }
        midFade();
        // Fade in
        startTime = Time.time;
        while ((Time.time - startTime) < time)
        {
            float frac = ((Time.time - startTime) / time);
            Blackboard.GM.BlackScreen.color = new Color(Blackboard.GM.BlackScreen.color.r, Blackboard.GM.BlackScreen.color.g, Blackboard.GM.BlackScreen.color.b, (1 - frac));
            yield return null;
        }
        afterFade();
        Blackboard.Player.PlayerMovement.canMove = true;
    }
}
