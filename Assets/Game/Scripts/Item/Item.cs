﻿using UnityEngine;

using CommonsHelper;
using JetBrains.Annotations;

public class Item : MonoBehaviour
{
    public ItemData data;
    
    [SerializeField, Tooltip("Do not modify in the inspector!"), ReadOnlyField]
    private ItemState state;

    void Start()
    {
        
    }

    /// Expose this item in the next free slot available.
    /// UB unless there is at least one free slot
    [UsedImplicitly]  // Button callback
    public void ExposeInNextFreeSlot()
    {
        ItemSetupManager.Instance.ExposeItemInNextFreeSlot(this);
    }

    public void OnExposed(int slotIndex)
    {
        state.exposed = true;
        state.slotIndex = slotIndex;
    }
}
