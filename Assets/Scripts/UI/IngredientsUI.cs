using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject iconTemplate;

    [SerializeField] private GameObject iconsContainer; // Container com Horizontal Layout Group

    void Start()
    {
        plateKitchenObject.OnIngredientAdded+=PlateKitchenObject_OnIngredientAdded;

        //Hide();
    }
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach(Transform child in transform) {
            if(child == iconTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetIngredientsList()) {
            GameObject newIcon = Instantiate(iconTemplate, iconsContainer.transform);
            newIcon.GetComponent<IconUI>().SetIcon(kitchenObjectSO);
            newIcon.gameObject.SetActive(true); // Exibe a nova imagem
        }
        
    }

    //private void Show() {
    //    gameObject.SetActive(true);
    //}

    //private void Hide() {
    //    gameObject.SetActive(false);
    //}


}
