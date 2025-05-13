using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private GameObject recipeTemplate;

    [SerializeField] private GameObject ordersContainer; // Container com Horizontal Layout Group

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false); // Esconde o template
    }

    void Start()
    {
        deliveryManager.OnOrderUpdate+=DeliveryManager_OnNewOrder;

        //Hide();
    }

    private void DeliveryManager_OnNewOrder(object sender, DeliveryManager.OnOrderUpdateEventArgs e) {
        foreach(Transform child in transform) {
            if(child==recipeTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach(OrderSO orderSO in e.activeOrdersList) {
            GameObject newRecipe = Instantiate(recipeTemplate, ordersContainer.transform);
            newRecipe.GetComponent<RecipeUI>().SetRecipeName(orderSO);
            newRecipe.gameObject.SetActive(true); // Exibe a nova imagem
        }
    }
    //private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
    //    foreach(Transform child in transform) {
    //        if(child == iconTemplate.transform) continue;
    //        Destroy(child.gameObject);
    //    }

    //    foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetIngredientsList()) {
    //        GameObject newIcon = Instantiate(iconTemplate, iconsContainer.transform);
    //        newIcon.GetComponent<IconUI>().SetIcon(kitchenObjectSO);
    //        newIcon.gameObject.SetActive(true); // Exibe a nova imagem
    //    }

    //}

    //private void Show() {
    //    gameObject.SetActive(true);
    //}

    //private void Hide() {
    //    gameObject.SetActive(false);
    //}


}
