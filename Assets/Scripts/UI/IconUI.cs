using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    public void SetIcon(KitchenObjectSO kitchenObjectSO) {
        icon.sprite=kitchenObjectSO.sprite;
    }

}
