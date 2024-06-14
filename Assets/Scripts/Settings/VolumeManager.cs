using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        // //Set the slider value to the current volume
        // float volume;
        // audioMixer.GetFloat("volume", out volume);
        // volumeSlider.value = volume;
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        Debug.Log("VolumeSlider Value: " + volumeSlider.value);

        audioMixer.SetFloat("vol", Mathf.Log10(volumeSlider.value) * 20);
    }

    // private void Load()
    // {

    // }

    // private void Save()
    // {

    // }
}
