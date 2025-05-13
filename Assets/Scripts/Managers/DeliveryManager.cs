using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    //    serializableObject order possui lista de kitchenObjectSO e nome
    //class deliveryManager possui lista de order e lista de order atual
    //baseado em um timer envia uma order da lista pra atual
    //seleciona uma aleatória da lista de order(ou posso fazer um array determinístico pra fase)
    // escuta evento OnOrderDelivered:
    //   TryGetDeliveredOrder()
    //   se encontrou, chama DeleteFromCurrentOrderList()
    //função TryGetDeliveredOrder()
    // pra cada order da lista de orderAtual
    //  pra cada elemento de e.ingredients
    //   verifica se contem o elemento na order, se sim retorna order
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private List<OrderSO> ordersList;
    private List<OrderSO> activeOrdersList;
    private float currentOrderTimer = 0;
    private int successfulOrderCount;
    private int waitingOrdersMax = 4;

    [SerializeField] private float newOrderRate = 4f;

    public event EventHandler<OnOrderUpdateEventArgs> OnOrderUpdate;
    public event EventHandler<EventArgs> OnSuccessfulOrder;
    public event EventHandler<EventArgs> OnFailedOrder;

    public class OnOrderUpdateEventArgs : EventArgs {
        public List<OrderSO> activeOrdersList;
    }

    private void Awake() {
        if(Instance!=null) {
            Debug.LogError($"An OrderManager is already assigned! Conflicting instance: {gameObject.name}");
        }
        Instance=this;

        successfulOrderCount=0;

        activeOrdersList=new List<OrderSO>();
    }

    private void Start() {

        deliveryCounter.OnOrderDelivered+=DeliveryCounter_OnOrderDelivered;

    }

    private void Update() {
        currentOrderTimer+=Time.deltaTime;
        if(currentOrderTimer>=newOrderRate) {
            currentOrderTimer=0;
            if(ordersList.Count>0 && activeOrdersList.Count<waitingOrdersMax) {
                OrderSO newOrder = ordersList[UnityEngine.Random.Range(0, ordersList.Count)];
                activeOrdersList.Add(newOrder);
                Debug.Log("New order: "+newOrder.orderName);
                OnOrderUpdate?.Invoke(this, new OnOrderUpdateEventArgs { activeOrdersList=activeOrdersList });
            }
        }
    }



    private void DeliveryCounter_OnOrderDelivered(object sender, DeliveryCounter.OnOrderDeliveredEventArgs e) {
        OrderSO deliveredOrder = TryGetDeliveredOrder(e.ingredients);
        if(deliveredOrder==null) {
            OnFailedOrder?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnSuccessfulOrder?.Invoke(this, EventArgs.Empty);
        successfulOrderCount++;

        activeOrdersList.Remove(deliveredOrder);
        OnOrderUpdate?.Invoke(this, new OnOrderUpdateEventArgs { activeOrdersList=activeOrdersList });

    }

    private OrderSO TryGetDeliveredOrder(List<KitchenObjectSO> receivedIngredients) {
        foreach(OrderSO order in activeOrdersList) {
            bool orderFound = true;
            foreach(KitchenObjectSO ingredient in order.ingredients) {
                if(!receivedIngredients.Contains(ingredient)) {
                    Debug.Log("ordernotfound");
                    orderFound=false;
                    break;
                }
            }
            if(orderFound) {
                Debug.Log("Order delivered: "+order.orderName);
                return order;
            }
        }
        return null;
    }
    public int GetSuccessfulOrderCount() {
        return successfulOrderCount;
    }

    //private bool TryGetDeliveredOrder(List deliveredOrder) {
    //    throw new NotImplementedException();
    //}
}
