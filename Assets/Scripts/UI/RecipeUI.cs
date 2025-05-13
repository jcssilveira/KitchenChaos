using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake() {
        iconTemplate.gameObject.SetActive(false); // Esconde o template
    }

    public void SetRecipeName(OrderSO orderSO) {
        recipeName.text=orderSO.orderName;

        foreach(Transform child in iconContainer) {
            if(child==iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in orderSO.ingredients) {
            Transform newIcon = Instantiate(iconTemplate, iconContainer);
            newIcon.gameObject.SetActive(true); // Exibe a nova imagem
            newIcon.GetComponent<Image>().sprite=kitchenObjectSO.sprite;

        }
    }


}
