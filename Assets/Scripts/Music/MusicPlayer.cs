using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audio;
    public Slider volumeSlider;
    private float musicVolume = 1f;

    private void Start()
    {
        audio.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        audio.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    private void Update()
    {
        musicVolume = volumeSlider.value;
        audio.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void volumeUpdater(float volume)
    {
        musicVolume = volume;
    }

    public void pauseAudio()
    {
        audio.Stop();
    }
}
