using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    public static List<AudioController> Audios { get; private set; }

    private AudioSource audio;

    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        Play();

        if (Audios == null)
            Audios = new List<AudioController>();
        Audios.Add(this);
    }

    public static void AudiosClear()
    {
        Audios.Clear();
        Audios = null;
    }

    public void Play()
    {
        audio.Play();
    }

    public void Pause()
    {
        audio.Pause();
    }

    public void Resume()
    {
        audio.UnPause();
    }

    public static void PauseAll()
    {
        foreach (AudioController audio in Audios)
        {
            audio.Pause();
        }
    }

    public static void ResumeAll()
    {
        foreach (AudioController audio in Audios)
        {
            audio.Resume();
        }
    }

    private void OnDestroy()
    {
        Audios.Remove(this);
    }
}
