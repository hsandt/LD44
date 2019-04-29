using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JetBrains.Annotations;

public class ItemView : MonoBehaviour
{
    private Item model;
    
    /// Assign model and register view update callback
    /// Since OnEnable happens too early, on instantiation, we need to manually register the callback after assigning
    /// the new model
    public void AssignModel(Item item)
    {
        if (model != null)
        {
            // unregister from old model (useful when pooling item views)
            model.ExposeEvent -= OnItemExposed;
        }
        model = item;
        model.ExposeEvent += OnItemExposed;
    }

    void OnEnable()
    {
        // When instantiated from Item View prefab, OnEnable will be called immediately, but the model isn't set yet
        // at this moment, so check for null. The code is still useful to re-register to model changes later,
        // if this component has been disabled then re-enabled.
        if (model != null)
        {
            model.ExposeEvent += OnItemExposed;
        }
    }

    void OnDisable()
    {
        if (model != null)
        {
            model.ExposeEvent -= OnItemExposed;
        }
    }

    private void OnItemExposed(Slot slot)
    {
        transform.position = slot.transform.position;
    }

    /// Expose this item in the next free slot available.
    /// UB unless there is at least one free slot
    [UsedImplicitly]  // Button callback
    public void ExposeInNextFreeSlot()
    {
        ItemSetupManager.Instance.ExposeItemInNextFreeSlot(model);
    }
}
