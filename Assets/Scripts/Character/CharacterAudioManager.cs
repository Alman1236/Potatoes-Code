using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;
using Sirenix.OdinInspector;

public class CharacterAudioManager : SerializedMonoBehaviour
{
    [SerializeField] AudioSource effectsSource;
    [SerializeField] AudioSource stepsAudioSource;

    [SerializeField] Dictionary<AudioClipsNames, AudioClip> audioClips = new Dictionary<AudioClipsNames, AudioClip>();
    
    public void PlaySound(AudioClipsNames clipName)
    {
        effectsSource.volume = Options.effectsVolume;

        if (AudioManager.canPlaySound)
        {
            if (audioClips.ContainsKey(clipName))
            {
                effectsSource.PlayOneShot(audioClips[clipName]);
            }
            else
            {
                Debug.LogError(clipName.ToString() + " not present in dictionary");
            }

            AudioManager.instance.SetCanPlaySoundFalse();
        }
        else
        {
            StartCoroutine(PlaySoundAfterDelay(clipName));
        }

    }

    public void PlayStepsSound(bool value)
    {
        stepsAudioSource.volume = Options.effectsVolume;

        if (value == true && !stepsAudioSource.isPlaying)
        {
            stepsAudioSource.clip = audioClips[AudioClipsNames.steps];
            stepsAudioSource.Play();
        }
        else if (value == false)
        {
            stepsAudioSource.Stop();
        }
    }

    IEnumerator PlaySoundAfterDelay(AudioClipsNames clipName)
    {
        float delay = Random.Range(0.05f, 0.15f);
        yield return new WaitForSecondsRealtime(delay);
        effectsSource.PlayOneShot(audioClips[clipName]);
    }
}
