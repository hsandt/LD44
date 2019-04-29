using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    private void OnInventoryChanged(Item[] newItems)
    {
        // OPTIMIZATION: for now we don't care about optimize adding just one or removing one item,
        // so we rebuild the view completely (it's fine on Start, but may be nice to change for delta ops)
        // We are not even pooling item views.

        // destroy from temporary copy of children list to avoid invalidation issues (although Destroy is deferred)
        List<Transform> oldItemTransforms = grid.Cast<Transform>().ToList();
        foreach (var oldItemTr in oldItemTransforms)
        {
            Destroy(oldItemTr.gameObject);
        }
        
        // fill grid layout with all items with non-0 quantity
        foreach (Item item in newItems)
        {
            ItemView view = itemViewPrefab.InstantiateUnder(grid).GetComponentOrFail<ItemView>();
            view.AssignModel(item);
        }
    }
}
