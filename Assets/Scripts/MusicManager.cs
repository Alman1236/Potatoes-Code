using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [SerializeField] AudioClip mainMenuTrack;
    [SerializeField] AudioClip[] levelsMusics = new AudioClip[2];

    byte currentSong;
    bool isStartingSongChose = false;

    Coroutine routine;
   
    private void Start()
    {
        OnVolumeChanged(Options.musicVolume);
    }

    public void OnVolumeChanged(float value)
    {
        source.volume = value;
    }

    public void OnPlayingVictrorySound()
    {
        StartCoroutine(DecreaseVolume());
    }

    IEnumerator DecreaseVolume()
    {
        float volume = source.volume;

        if (volume > 0) 
        {
            source.volume = 0.1f;
            yield return new WaitForSeconds(2);
            source.volume = volume;
        }
        
       
    }

    public void OnSceneLoaded()
    {
        ushort whichLevelIsThis = (ushort)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (whichLevelIsThis == 0)
        {
            source.clip = mainMenuTrack;
            PlayMusic();
            StopAllCoroutines();
            isStartingSongChose = false;
        }
        else
        {
            if (!isStartingSongChose)
            {
                PlayNextSong();
            }
        }

    }

    void PlayMusic()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }

        source.Play();
    }

    IEnumerator WaitThatSongFinishes(float trackDurationInSeconds)
    {
        yield return new WaitForSeconds(trackDurationInSeconds + 5);

        PlayNextSong();
    }

    void PlayNextSong()
    {
        if (!isStartingSongChose)
        {
            currentSong = (byte)Random.Range(0, levelsMusics.Length);
            source.clip = levelsMusics[currentSong];
            isStartingSongChose = true;
        }
        else
        {
            currentSong++;

            if (currentSong >= levelsMusics.Length)
            {
                currentSong = 0;
            }

            source.clip = levelsMusics[currentSong];
        }

        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(WaitThatSongFinishes(source.clip.length));
        PlayMusic();
    }
}
