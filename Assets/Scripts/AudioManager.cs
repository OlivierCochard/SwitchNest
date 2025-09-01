using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake(){
        refreshVolume();
    }

    public void refreshVolume(){
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSource.volume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
    }

    public void addSFX(AudioClip audioClip){
        sfxSource.PlayOneShot(audioClip);
    }
}
