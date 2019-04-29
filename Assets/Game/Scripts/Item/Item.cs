using System;
using UnityEngine;

using CommonsHelper;
using JetBrains.Annotations;

[Serializable]
public class Item
{
    public delegate void ExposeHandler(Slot slot);
    public event ExposeHandler ExposeEvent;
    
    public delegate void PullBackHandler();
    public event PullBackHandler PullBackEvent;
    
    
    /* Parameters */
    
    private ItemData data;
    public ItemData Data => data;


    /* State */
    
    [SerializeField, ReadOnlyField, Tooltip("Current state")]
    private ItemState state;

    
    public int Quantity => state.quantity;
    public bool Exposed => state.exposed;
    public int SlotIndex => state.slotIndex;

    public Item(ItemData data)
    {
        this.data = data;
        state = new ItemState(0);
    }

    public void IncreaseQuantity(int addedQuantity)
    {
        state.quantity += addedQuantity;
    }
    
    public void ExposeInNextFreeSlot()
    {
        ItemSetupManager.Instance.ExposeItemInNextFreeSlot(this);
    }

    public void ExposeInSlot(Slot slot)
    {
        state.exposed = true;
        state.slotIndex = slot.Index;
        
        slot.CurrentItem = this;
        
        ExposeEvent?.Invoke(slot);
    }
    
    public void PullBackFromSlot(Slot slot)
    {
        state.exposed = false;
        state.slotIndex = -1;
        
        slot.CurrentItem = null;

        PullBackEvent?.Invoke();
    }
}
