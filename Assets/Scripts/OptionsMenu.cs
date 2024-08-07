using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioClip sfxClip;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        if (audioMixer.GetFloat("musicVolume", out float musicLevel))
        {
            musicSlider.value = musicLevel;
        }
        if (audioMixer.GetFloat("musicVolume", out float sfxLevel))
        {
            musicSlider.value = sfxLevel;
        }
    }

    public void SetMusicVolume (float musicVolume)
    {
        Debug.Log($"Setting music volume to {musicVolume}");
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetFXVolume(float sfxVolume)
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
        Debug.Log($"Setting sfx volume to {sfxVolume}");
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(sfxClip);
    }
}
