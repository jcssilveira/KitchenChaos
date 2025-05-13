using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStateChanged+=StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if(e.state==StoveCounter.State.Cooking || e.state == StoveCounter.State.Burning) {
            Debug.Log("StoveSound: Cooking or Burning");
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}
