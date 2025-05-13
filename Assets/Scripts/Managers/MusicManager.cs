using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private float soundVolumePercent = 1f;
    private AudioSource audioSource;

    private static string SOUND_VOLUME_PERCENT = "soundVolumePercent";
    public static MusicManager Instance { get; private set; }

    private void Awake() {
        if(Instance==null) {
            Instance=this;
        }
        else {
            Debug.LogError("Instance of SoundManager already exists!");
        }
        audioSource=GetComponent<AudioSource>();
        soundVolumePercent=PlayerPrefs.GetFloat(SOUND_VOLUME_PERCENT, 1f);
    }

    public void SetVolumePercent(float volumePercent) {
        audioSource.volume=volumePercent;
        PlayerPrefs.SetFloat("volumePercent", volumePercent);
    }
    public float GetVolumePercent() {
        return soundVolumePercent;
    }

}
