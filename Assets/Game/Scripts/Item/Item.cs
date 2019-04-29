using System;
using UnityEngine;

using CommonsHelper;
using JetBrains.Annotations;

[Serializable]
public class Item
{
    public delegate void ExposeHandler(Slot slot);
    public event ExposeHandler ExposeEvent;
    
    
    /* Parameters */
    
    private ItemData data;
    public ItemData Data => data;


    /* State */
    
    [SerializeField, ReadOnlyField, Tooltip("Current state")]
    private ItemState state;

    
    public int Quantity => state.quantity;

    public Item(ItemData data)
    {
        this.data = data;
        state = new ItemState(0);
    }

    public void IncreaseQuantity(int addedQuantity)
    {
        state.quantity += addedQuantity;
    }
    
    /// Expose this item in the next free slot available.
    /// UB unless there is at least one free slot
    [UsedImplicitly]  // Button callback
    public void ExposeInNextFreeSlot()
    {
        ItemSetupManager.Instance.ExposeItemInNextFreeSlot(this);
    }

    public void OnExposed(Slot slot)
    {
        state.exposed = true;
        state.slotIndex = slot.Index;
        ExposeEvent?.Invoke(slot);
    }
}
