using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        // Add listener to the slider value change event
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
        volumeSlider.value = volumeSlider.maxValue;
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
    
    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
