using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;
using Sirenix.OdinInspector;

public class AudioManager : SerializedMonoBehaviour
{
    [SerializeField] AudioSource effectsSource;
    [SerializeField] AudioSource timerAudioSource;

    [SerializeField] Dictionary<AudioClipsNames, AudioClip> audioClips = new Dictionary<AudioClipsNames, AudioClip>();

    static public AudioManager instance;

    static public bool canPlaySound { get; private set; } = true;
    public void SetCanPlaySoundFalse()
    {
        canPlaySound = false;
        StartCoroutine(ResetCanPlaySound());
    }

    void Awake()
    {
        GenerateInstance();
    }

    void GenerateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are multiple AudioManager in scene!");
            Debug.Break();
        }
    }

    public void PlaySound(AudioClipsNames clipName)
    {
        effectsSource.volume = Options.effectsVolume;
        
        if (!audioClips.ContainsKey(clipName))
        {
            Debug.Log(clipName.ToString());
        }

        if (canPlaySound)
        {
            effectsSource.PlayOneShot(audioClips[clipName]);

            canPlaySound = false;
            StartCoroutine(ResetCanPlaySound());
        }
        else
        {
            StartCoroutine(PlaySoundAfterDelay(clipName));
        }

    }

    public void PlayTimerSound(bool value)
    {
        timerAudioSource.volume = Options.effectsVolume / 5;

        if (value == true && !timerAudioSource.isPlaying)
        {
            timerAudioSource.clip = audioClips[AudioClipsNames.clockTick];
            timerAudioSource.Play();
        }
        else if (value == false)
        {
            timerAudioSource.Stop();
        }

    }

    IEnumerator ResetCanPlaySound()
    {
        yield return new WaitForSeconds(0.05f);
        canPlaySound = true;
    }

    IEnumerator PlaySoundAfterDelay(AudioClipsNames clipName)
    {
        float delay = Random.Range(0.05f, 0.15f);
        yield return new WaitForSecondsRealtime(delay);
        effectsSource.PlayOneShot(audioClips[clipName]);
    }
}
