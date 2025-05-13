using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioEffectsSO audioEffectsSO;

    public static SoundManager Instance { get; private set; }

    private float soundVolumePercent = 1f;

    private static string SOUND_VOLUME_PERCENT = "soundVolumePercent";

    private void Awake() {
        if(Instance==null) {
            Instance=this;
        }
        else {
            Debug.LogError("Instance of SoundManager already exists!");
        }

        soundVolumePercent=PlayerPrefs.GetFloat(SOUND_VOLUME_PERCENT, 1f);
    }

    private void Start() {
        CuttingCounter.OnAnyCut+=CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnSuccessfulOrder+=OrderManager_OnSuccessfulOrder;
        DeliveryManager.Instance.OnFailedOrder+=OrderManager_OnFailedOrder;
        Player.Instance.OnObjectPickup+=BaseCounter_OnObjectPickup;
        Player.Instance.OnObjectDrop+=BaseCounter_OnObjectDrop;
        TrashCounter.OnTrashedObject+=TrashCounter_OnTrashedObject;
    }

    public void PlayFootstepSound(Vector3 position) {
        float volume = 0.5f; 
        Debug.Log("Playing footstep sound");
        PlayAudioClip(audioEffectsSO.footstep, position, volume);
    }

    private void TrashCounter_OnTrashedObject(object sender, System.EventArgs e) {
        float volume = 0.5f;
        TrashCounter trashCounter = sender as TrashCounter;
        PlayAudioClip(audioEffectsSO.trash, trashCounter.transform.position, volume);
    }

    private void BaseCounter_OnObjectDrop(object sender, System.EventArgs e) {
        float volume = 0.5f;
        PlayAudioClip(audioEffectsSO.objectDrop, Camera.main.transform.position, volume);
    }

    private void BaseCounter_OnObjectPickup(object sender, System.EventArgs e) {
        float volume = 0.5f;
        PlayAudioClip(audioEffectsSO.objectPickup, Camera.main.transform.position, volume);
    }

    private void OrderManager_OnSuccessfulOrder(object sender, System.EventArgs e) {
        float volume = 0.5f;
        PlayAudioClip(audioEffectsSO.deliverySuccess, Camera.main.transform.position, volume);

    }
    private void OrderManager_OnFailedOrder(object sender, System.EventArgs e) {
        float volume = 0.5f;
        PlayAudioClip(audioEffectsSO.deliveryFailed, Camera.main.transform.position, volume);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        // Play the first clip in the Chop array
        //play random audio clip in the array chop
        float volume = 1.5f;
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayAudioClip(audioEffectsSO.chop, cuttingCounter.transform.position, volume);
    }

    private void PlayAudioClip(AudioClip[] audioClip, Vector3 position, float volume = 1f) {
        PlayAudioClip(audioClip[Random.Range(0, audioClip.Length)], position, volume*soundVolumePercent);
    }

    private void PlayAudioClip(AudioClip audioClip, Vector3 position, float volume) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume*soundVolumePercent);
    }

    public void SetVolumePercent(float volumePercent) {
        soundVolumePercent=volumePercent;
        PlayerPrefs.SetFloat("volumePercent", volumePercent);
    }
    public float GetVolumePercent() {
        return soundVolumePercent;
    }
}
