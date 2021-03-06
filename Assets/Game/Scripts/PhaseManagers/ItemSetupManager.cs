﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using CommonsHelper;

public class ItemSetupManager : PhaseManager<ItemSetupManager>
{
    protected ItemSetupManager () {}

    public override void Init()
    {
        SetInstanceOrSelfDestruct(this);
        
        director = this.GetComponentOrFail<PlayableDirector>();
    }

    [Tooltip("Slot pool")]
    public SlotPool slotPool;
    
    [Tooltip("Inventory View")]
    public InventoryView inventoryView;
    
    
    /* Sibling components */
    private PlayableDirector director;
    

    // we use static here just to make it easier to register callbacks in OnEnable
    // without fearing that the Singleton Instance has not been registered yet
    public delegate void ExposeItemHandler();
    public static event ExposeItemHandler ExposeItemEvent;
    
    public delegate void PullBackItemHandler();
    public static event PullBackItemHandler PullBackItemEvent;
    
    protected override void OnEnableCallback()
    {
        base.OnEnableCallback();

        if (inventoryView != null)
        {
            // you can place items now
            inventoryView.SetAllowInteractions(true);
        }
        
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
        
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.HideTutorial();
        }
    }

    public void ExposeItemInNextFreeSlot(Item item)
    {
        Slot freeSlot = slotPool.GetNextFreeSlot();
        if (freeSlot != null)
        {
            item.ExposeInSlot(freeSlot);
            
            // exceptionally, do not call Inventory.OnChanged because the Item View will take care
            // of moving the sprite to the target slot, so the inventory grid doesn't need to be
            // updated (if we were using clone of the Item View in the slot and graying out the
            // item view in the inventory grid, then we would to update either the whole inventory grid
            // or at least the item view in the inventory separately)

            OnExposedItem();
        }
        else
        {
            Debug.LogError("No free slot left, cannot Expose Item (you should gray out the Expose button first)");
        }
    }

    public void PullBackToInventory(Item item)
    {
        Slot originalSlot = slotPool.GetSlotAt(item.SlotIndex);
        item.PullBackFromSlot(originalSlot);
        
        // exceptionally, we do not call Inventory.OnChanged because the Item View will take care
        // of moving the sprite back to the grid

        OnPulledBackItem();
    }

    protected virtual void OnExposedItem()
    {
        ExposeItemEvent?.Invoke();
    }

    protected virtual void OnPulledBackItem()
    {
        PullBackItemEvent?.Invoke();
    }
}
