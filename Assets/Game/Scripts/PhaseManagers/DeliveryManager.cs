using System.Collections;
using System.Collections.Generic;
using CommonsHelper;
using UnityEngine;
using UnityEngine.Playables;

using CommonsPattern;
using TMPro;
using UnityEngine.Serialization;

public class DeliveryManager : PhaseManager<DeliveryManager>
{
    protected DeliveryManager () {}

    public override void Init()
    {
        SetInstanceOrSelfDestruct(this);
        
        director = this.GetComponentOrFail<PlayableDirector>();
    }
    
    [FormerlySerializedAs("deliveryOrder")]
    [Header("External scene references")]
    
    [Tooltip("Next Delivery Order")]
    public DeliveryOrder nextDeliveryOrder;
    
    [Tooltip("Inventory Model")]
    public Inventory inventory;

    [Tooltip("Inventory View")]
    public InventoryView inventoryView;

    
    /* Sibling components */
    private PlayableDirector director;
    
    
    protected override void OnEnableCallback()
    {
        base.OnEnableCallback();
        
        // don't allow touching the inventory yet
        inventoryView.SetAllowInteractions(false);
        
        // play timeline that will show delivery and apply it
        director.Play();
    }
    
    protected override void OnDisableCallback()
    {
        base.OnDisableCallback();

        if (inventoryView != null)
        {
            // stop interactions until next time
            inventoryView.SetAllowInteractions(false);
        }
    }

    public void AddDeliveryOrder(List<DeliveryOrder.SingleItemOrder> newItemOrders)
    {
        nextDeliveryOrder.AddItemOrders(newItemOrders);
    }
    
    public void DeliverNextOrder()
    {
        DeliverOrder(nextDeliveryOrder.ItemOrders);
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
