using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CommonsHelper;

using JetBrains.Annotations;

public class ItemView : MonoBehaviour
{
    [Tooltip("Item sprite")]
    public Image itemPicture;
    
    /* External scene references (injected) */
    
    private InventoryView inventoryView;

    
    /* Parameters */
    
    private Item model;


    void Awake()
    {
    }

    public void SetInventoryView(InventoryView inventoryView)
    {
        this.inventoryView = inventoryView;
    }

    /// Assign model and register view update callback
    /// Since OnEnable happens too early, on instantiation, we need to manually register the callback after assigning
    /// the new model
    public void AssignModel(Item item)
    {
        // unregister from old model (useful when pooling item views)
        TryUnregisterFromModelEvents();

        model = item;
        SetupViewFromModel();        
        
        TryRegisterToModelEvents();
    }

    private void SetupViewFromModel()
    {
        itemPicture.sprite = model.Data.sprite;
    }

    void OnEnable()
    {
        TryRegisterToModelEvents();
    }

    void OnDisable()
    {
        TryUnregisterFromModelEvents();
    }

    private void TryRegisterToModelEvents()
    {
        // When instantiated from Item View prefab, OnEnable will be called immediately, but the model isn't set yet
        // at this moment, so check for null. The code is still useful to re-register to model changes later,
        // if this component has been disabled then re-enabled.
        if (model != null)
        {
            model.ExposeEvent += OnItemExposed;
            model.PullBackEvent += OnItemPulledBack;
        }
    }
    private void TryUnregisterFromModelEvents()
    {
        if (model != null)
        {
            model.ExposeEvent -= OnItemExposed;
            model.PullBackEvent -= OnItemPulledBack;
        }
    }

    private void OnItemExposed(Slot slot)
    {
        // detach from grid parent, reparent to slot at relative position 0 to match center
        transform.SetParent(slot.transform, false);
        transform.localPosition = Vector3.zero;
    }

    private void OnItemPulledBack()
    {
        // reparent to grid, let the layout reposition correctly
        // (item will be replaced at the end of the grid!)
        transform.SetParent(inventoryView.grid, false);
    }

    [UsedImplicitly]  // Button callback
    public void OnButtonAction()
    {
        // if inside the inventory, expose
        // if exposed, pull back to the inventory

        if (model.Exposed)
        {
            PullBackToInventory();
        }
        else
        {
            ExposeInNextFreeSlot();
        }
    }

    /// Remove item from exposition
    private void PullBackToInventory()
    {
        ItemSetupManager.Instance.PullBackToInventory(model);
    }
    
    /// Expose this item in the next free slot available.
    /// UB unless there is at least one free slot
    private void ExposeInNextFreeSlot()
    {
        ItemSetupManager.Instance.ExposeItemInNextFreeSlot(model);
    }
}
