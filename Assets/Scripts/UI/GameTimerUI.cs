using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] Image timer;
    [SerializeField] Image background;

    void Start() {
        timer.fillAmount=0f; // não é necessario ja que vai ficar escondido mas é bom pra garantir
    }
    private void Update() {
        timer.fillAmount=GameManager.Instance.GetGameRunningTimerNormalized();
        if(timer.fillAmount<0.1) {
            timer.color=Color.red;
        }

    }

}
