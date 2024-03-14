using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
     [SerializeField] private AudioMixer _audioMixer;
     [SerializeField] public Slider _volumeSlider;
     [SerializeField] public Slider _sfxSlider;
     private void Start()
     {
         if(PlayerPrefs.HasKey("Music"))
         {
             LoadVolume();
         }else
         {
             SetVolume();
             SetSFXVolume();
         }
     }
     
     
     public void SetVolume()
     {
         float volume = _volumeSlider.value;
         _audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
         PlayerPrefs.SetFloat("Music", volume);
     }
     public void SetSFXVolume()
     {
         float volume = _sfxSlider.value;
         _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
         PlayerPrefs.SetFloat("SFX", volume);
     }
     
     private void LoadVolume()
     {
         _volumeSlider.value = PlayerPrefs.GetFloat("Music");
         _sfxSlider.value = PlayerPrefs.GetFloat("SFX");
         SetVolume();
         SetSFXVolume();
     }
     
}
