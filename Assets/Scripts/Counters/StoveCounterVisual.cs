using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject sizzlingParticles;
    [SerializeField] GameObject stoveOnVisual;
    [SerializeField] StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStateChanged+=StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = e.state==StoveCounter.State.Cooking||e.state==StoveCounter.State.Burning;

        stoveOnVisual.SetActive(showVisual);
        sizzlingParticles.SetActive(showVisual);
    }


}
