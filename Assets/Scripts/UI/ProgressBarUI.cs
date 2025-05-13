using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private IHasProgress hasProgress;
    [SerializeField] private Image progressBar;
    void Start()
    {
        hasProgress=hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null) {
            Debug.LogError(hasProgressGameObject +" does not have a IHasProgress component");
        }
        //se inscreve pro evento de cutting
        hasProgress.OnProgressChanged+=ProgressBar_OnProgressChanged;
        progressBar.fillAmount=0f; // não é necessario ja que vai ficar escondido mas é bom pra garantir
        Hide();
    }
    private void ProgressBar_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        progressBar.fillAmount=e.progressNormalized;
        if(e.progressNormalized==1 || e.progressNormalized==0) {
            Hide();
        }
        else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
