using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OrderSO : ScriptableObject {

    public string orderName;
    public List<KitchenObjectSO> ingredients;

}
