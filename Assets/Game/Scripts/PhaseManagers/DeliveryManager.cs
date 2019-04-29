using System.Collections;
using System.Collections.Generic;
using CommonsHelper;
using UnityEngine;
using UnityEngine.Playables;

using CommonsPattern;
using TMPro;
using UnityEngine.Serialization;

public class DeliveryManager : SingletonManager<DeliveryManager>
{
    protected DeliveryManager () {}

    void Awake () {
        SetInstanceOrSelfDestruct(this);
        Init();
    }
    
    [FormerlySerializedAs("deliveryOrder")]
    [Header("External scene references")]
    
    [Tooltip("Next Delivery Order")]
    public DeliveryOrder nextDeliveryOrder;
    
    [Tooltip("Inventory Model")]
    public Inventory inventory;

    [Tooltip("Delivery UI root")]
    public GameObject uiRoot;

    [Tooltip("Delivery Content View")]
    public DeliveryContentView deliveryContentView;

    
    /* Sibling components */
    private PlayableDirector director;
    
    void Init()
    {
        director = this.GetComponentOrFail<PlayableDirector>();
    }

    private void OnEnable()
    {
        uiRoot.SetActive(true);
        
        // play timeline that will show delivery and apply it
        director.Play();
    }
    
    private void OnDisable()
    {
        // when stopping game in Editor, OnDisable is called but other objects may have been destroyed
        if (uiRoot != null)
        {
            uiRoot.SetActive(false);
        }
    }

    public void AddDeliveryOrder(List<DeliveryOrder.SingleItemOrder> newItemOrders)
    {
        nextDeliveryOrder.AddItemOrders(newItemOrders);
    }
    
    public void DeliverNextOrder()
    {
        // model
        DeliverOrder(nextDeliveryOrder.ItemOrders);
        
        // view
        deliveryContentView.UpdateText(nextDeliveryOrder.ItemOrders);
        
        // clear
        nextDeliveryOrder.Clear();
    }
    
    private void DeliverOrder(List<DeliveryOrder.SingleItemOrder> itemOrders)
    {
        foreach (var itemOrder in itemOrders)
        {
            inventory.IncreaseItemQuantity(itemOrder.itemData.itemName, itemOrder.quantity);
        }
    }
}
