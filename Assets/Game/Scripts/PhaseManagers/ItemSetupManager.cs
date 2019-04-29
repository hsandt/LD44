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

    void Init()
    {
        
    }

    private void OnEnable()
    {
        uiRoot.SetActive(true);
    }
    
    private void OnDisable()
    {
        // when stopping game in Editor, OnDisable is called but other objects may have been destroyed
        if (uiRoot != null)
        {
            uiRoot.SetActive(false);
        }
    }

    public void ExposeItemInNextFreeSlot(Item item)
    {
        Slot freeSlot = slotPool.GetNextFreeSlot();
        if (freeSlot != null)
        {
            MoveItemToSlot(item, freeSlot);
        }
        else
        {
            Debug.LogError("No free slot left, cannot Expose Item (you should gray out the Expose button first)");
        }
    }

    private void MoveItemToSlot(Item item, Slot slot)
    {
        item.OnExposed(slot);
        slot.CurrentItem = item;
    }
}
