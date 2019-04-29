using System.Collections;
using System.Collections.Generic;
using CommonsHelper;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [Header("Asset references")]
    [Tooltip("Item View Prefab")]
    public GameObject itemViewPrefab;
    
    [Header("External scene references")]
    [Tooltip("Inventory Model")]
    public Inventory model;
    
    [Header("Child references")]
    [Tooltip("Inventory Grid")]
    public Transform grid;
    
    void OnEnable()
    {
        model.ChangeEvent += OnInventoryChanged;
    }

    void OnDisable()
    {
        if (model != null)
        {
            model.ChangeEvent -= OnInventoryChanged;
        }
    }
    
    private void OnInventoryChanged(Item[] items)
    {
        // OPTIMIZATION: for now we don't care about optimize adding just one or removing one item,
        // so we rebuild the view completely (it's fine on Start, but may be nice to change for delta ops)
        
        // fill grid layout with all items with non-0 quantity
        foreach (Item item in items)
        {
            ItemView view = itemViewPrefab.InstantiateUnder(grid).GetComponentOrFail<ItemView>();
            view.AssignModel(item);
        }
    }
}
