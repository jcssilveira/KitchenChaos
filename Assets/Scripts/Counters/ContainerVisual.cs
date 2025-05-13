using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    private Animator animator;

    [SerializeField] private ContainerCounter containerCounter;
    private void Awake() {
        animator=GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject+=OnOpenClose;
    }

    private void OnOpenClose(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);

       
    }

}
