using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] GameObject optionsScreen;

    private void Awake() {
        //resumeButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.GameScene); });
        //mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });

        resumeButton.onClick.AddListener(() => { GameInput.Instance.Pause_performed(); });
        optionsButton.onClick.AddListener(() => { optionsScreen.SetActive(true); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });

    }

    private void Start() {
        gameObject.SetActive(false);
        GameInput.Instance.OnPause+=GameInput_OnPause;
    }

    private void GameInput_OnPause(object sender, EventArgs e) {
        SwitchPauseScreen();
    }

    private void SwitchPauseScreen() {
        gameObject.SetActive(!gameObject.activeSelf);
        optionsScreen.SetActive(false);
    }
}
