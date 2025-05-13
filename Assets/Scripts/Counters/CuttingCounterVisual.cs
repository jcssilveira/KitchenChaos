using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    private const string CUT = "Cut";

    private Animator animator;

    [SerializeField] private CuttingCounter cuttingCounter;
    private void Awake() {
        animator=GetComponent<Animator>();
    }
    private void Start() {
        cuttingCounter.OnCut+=cuttingCounter_OnCut;
    }

    private void cuttingCounter_OnCut(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }

}
