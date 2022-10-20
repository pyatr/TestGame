using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> musicList;

    private Coroutine musicPlay = null;

    private AudioClip currentTrack = null;

    public void Play()
    {
        Stop();
        musicPlay = StartCoroutine(Playback());
    }

    private IEnumerator Playback()
    {
        while (isActiveAndEnabled)
        {
            AudioClip finishedTrack = currentTrack;
            musicList.Remove(currentTrack);
            currentTrack = musicList.GetRandomElement();
            if (finishedTrack)
            {
                musicList.Add(finishedTrack);
            }
            audioSource.clip = currentTrack;
            audioSource.Play();
            yield return new WaitForSecondsRealtime(currentTrack.length);
        }
        Stop();
    }

    public void Stop()
    {
        if (musicPlay != null)
        {
            StopCoroutine(musicPlay);
            audioSource.Stop();
        }
    }

}
