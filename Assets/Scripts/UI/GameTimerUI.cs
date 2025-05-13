using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] Image timer;
    [SerializeField] Image background;

    void Start() {
        timer.fillAmount=0f; // n�o � necessario ja que vai ficar escondido mas � bom pra garantir
    }
    private void Update() {
        timer.fillAmount=GameManager.Instance.GetGameRunningTimerNormalized();
        if(timer.fillAmount<0.1) {
            timer.color=Color.red;
        }

    }

}
