using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
     [SerializeField] private AudioMixer _audioMixer;
     [SerializeField] private Slider _volumeSlider;
     [SerializeField] private Slider _sfxSlider;
     private void Start()
     {
         if(PlayerPrefs.HasKey("Volume"))
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
         var volume = _volumeSlider.value;
         _audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
         PlayerPrefs.SetFloat("Volume", volume);
     }
     public void SetSFXVolume()
     {
         var volume = _sfxSlider.value;
         _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
         PlayerPrefs.SetFloat("SFX", volume);
     }
     
     private void LoadVolume()
     {
         _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            _sfxSlider.value = PlayerPrefs.GetFloat("SFX");
         SetVolume();
         SetSFXVolume();
     }
     
}
