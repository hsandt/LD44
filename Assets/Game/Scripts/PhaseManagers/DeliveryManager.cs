using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public class DeliveryManager : SingletonManager<DeliveryManager>
{
    protected DeliveryManager () {}

    void Awake () {
        SetInstanceOrSelfDestruct(this);
        Init();
    }
    
    [Header("External scene references")]
    
    [Tooltip("Next Delivery Order")]
    public DeliveryOrder deliveryOrder;
    
    [Tooltip("Inventory Model")]
    public Inventory inventory;

    [Tooltip("Delivery UI root")]
    public GameObject uiRoot;

    void Init()
    {
        
    }

    private void OnEnable()
    {
        uiRoot.SetActive(true);
        
        DeliverNextOrder();
    }
    
    private void OnDisable()
    {
        // when stopping game in Editor, OnDisable is called but other objects may have been destroyed
        if (uiRoot != null)
        {
            uiRoot.SetActive(false);
        }
    }

    private void DeliverNextOrder()
    {
        DeliverOrder(deliveryOrder.ItemOrders);
    }
    
    public void DeliverOrder(List<DeliveryOrder.SingleItemOrder> itemOrders)
    {
        foreach (var itemOrder in itemOrders)
        {
            inventory.IncreaseItemQuantity(itemOrder.itemData.itemName, itemOrder.quantity);
        }
    }
}
