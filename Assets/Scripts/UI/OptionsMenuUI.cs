using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour {
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Button returnButton;


    private void Awake() {
        musicSlider.value=MusicManager.Instance.GetVolumePercent();

        soundSlider.value=SoundManager.Instance.GetVolumePercent();

        musicSlider.onValueChanged.AddListener((float value) => {
            MusicManager.Instance.SetVolumePercent(value);
        });


        soundSlider.onValueChanged.AddListener((float value) => {
            SoundManager.Instance.SetVolumePercent(value);
        });

        returnButton.onClick.AddListener(() => { gameObject.SetActive(false); });

    }

    private void Start() {
        gameObject.SetActive(false);
    }

}
