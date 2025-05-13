using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI recipeNumber;
    //acessa countdown de GameManager para exibir o número equivalente

    private void Awake() {
        
        background.gameObject.SetActive(false);
    }

    private void Start() {
        GameManager.Instance.OnGameStateChanged+=Instance_OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void Update() {

        //if game is notStarted, show countdown and update text
        if(GameManager.Instance.GetGameState()==GameManager.GameState.GameOver) {
            background.gameObject.SetActive(true);
        }
        else {
            background.gameObject.SetActive(false);
        }
    }
    private void Instance_OnGameStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.GetGameState()==GameManager.GameState.GameOver) {
            gameObject.SetActive(true);
            recipeNumber.text=DeliveryManager.Instance.GetSuccessfulOrderCount().ToString();
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
