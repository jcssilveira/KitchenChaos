using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    //acessa countdown de GameManager para exibir o número equivalente

    private void Awake() {
        countdownText.gameObject.SetActive(false);
    }

    private void Start() {
        GameManager.Instance.OnGameStateChanged+=Instance_OnGameStateChanged;
    }

    private void Update() {

        //if game is notStarted, show countdown and update text
        if(GameManager.Instance.IsGameOnCountdown()) {
            countdownText.gameObject.SetActive(true);
            countdownText.text=Mathf.Ceil(GameManager.Instance.GetRunningTimer()).ToString();
        }
        else {
            countdownText.gameObject.SetActive(false);
        }
    }
    private void Instance_OnGameStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsGameOnCountdown()) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }


}
