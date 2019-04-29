using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public class ItemSetupManager : SingletonManager<ItemSetupManager>
{
    protected ItemSetupManager () {}

    void Awake () {
        SetInstanceOrSelfDestruct(this);
        Init();
    }

    [Tooltip("Slot pool")]
    public SlotPool slotPool;
    
    [Tooltip("Item Setup UI root")]
    public GameObject uiRoot;

    [Tooltip("Inventory View")]
    public InventoryView inventoryView;

    void Init()
    {
        
    }

    private void OnEnable()
    {
        uiRoot.SetActive(true);
        
        // you can place items now
        inventoryView.SetAllowInteractions(true);
    }
    
    private void OnDisable()
    {
        // when stopping game in Editor, OnDisable is called but other objects may have been destroyed
        if (uiRoot != null)
        {
            uiRoot.SetActive(false);
        }
        
        // stop interactions until next time
        inventoryView.SetAllowInteractions(false);
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
    }
}
