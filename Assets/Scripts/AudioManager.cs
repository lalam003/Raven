using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioSource AudioSource;
    private IEnumerator routine;
    private const float scrollDelay = .5f;
    private const float volumeDecayRate = .05f;

    [SerializeField]
    private Slider audioSlider;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        loadAudio();
    }

    public void AdjustVolume()
    {
        Blackboard.Player.PlayerMovement.Up =    () => { return Vector2.zero; };
        Blackboard.Player.PlayerMovement.Down =  () => { return Vector2.zero; };
        Blackboard.Player.PlayerMovement.Right = () => 
        {
            if (routine == null || !routine.MoveNext())
            {
                raiseVolume();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.East, raiseVolume, scrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Left = () =>
        {
            if (routine == null || !routine.MoveNext())
            {
                lowerVolume();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.West, lowerVolume, scrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Menu = () =>
        {
            audioSlider.gameObject.SetActive(false);
            Blackboard.Menu.SetMenuInput();
        };

        audioSlider.value = AudioSource.volume;
        audioSlider.gameObject.SetActive(true);
    }

    private void lowerVolume()
    {
        AudioSource.volume -= volumeDecayRate;
        AudioSource.volume = Mathf.Clamp01(AudioSource.volume);
        audioSlider.value = AudioSource.volume;
        PlayAudio(audioDict["MenuSelect"]);
    }

    private void raiseVolume()
    {
        AudioSource.volume += volumeDecayRate;
        AudioSource.volume = Mathf.Clamp01(AudioSource.volume);
        audioSlider.value = AudioSource.volume;
        PlayAudio(audioDict["MenuSelect"]);
    }

    public void PlayAudio(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
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
