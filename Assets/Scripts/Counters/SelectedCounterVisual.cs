 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] BaseCounter thisCounter;
    [SerializeField] GameObject[] counterSelectedVisualArray;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged+=Player_OnSelectedCounter;
    }

    private void Player_OnSelectedCounter(object sender, Player.InteractEventArgs e) {
        if(e.selectedCounter == thisCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        foreach(GameObject counterSelectedVisual in counterSelectedVisualArray) {
            counterSelectedVisual.SetActive(true);
        }
    }

    private void Hide() {
        foreach(GameObject counterSelectedVisual in counterSelectedVisualArray) {
            counterSelectedVisual.SetActive(false);
        }
    }

}
