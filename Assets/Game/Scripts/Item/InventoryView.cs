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
    
    
    /* Sibling components */

    private CanvasGroup canvasGroup;
    
    
    /* State */
    
    /// Should we refresh the view?
    /// Start true, so even if inventory view is enabled late, or if the inventory
    /// starts with some workshop items that shouldn't be in the final game, everything
    /// will be cleaned up in time.
    private bool dirty = true;

    /// The interactable field of CanvasGroup only applies to children
    /// if this game object has not been activated yet. So to allow disabling interactions
    /// early (when the first Delivery phase starts), we delay it to the next OnEnable
    /// by storing the state in a boolean.
    private bool allowInteractions = true;

    void Awake()
    {
        canvasGroup = this.GetComponentOrFail<CanvasGroup>();
    }
    
    void OnEnable()
    {
        model.ChangeEvent += OnInventoryChanged;

        // apply interaction flag now
        RefreshInteractableState();
    }

    void OnDisable()
    {
        if (model != null)
        {
            model.ChangeEvent -= OnInventoryChanged;
        }
    }

    private void OnInventoryChanged()
    {
        // do not update view immediately, as it would be expensive if items are added
        // inside a loop
        dirty = true;
    }

    private void Update()
    {
        if (dirty)
        {
            dirty = false;
            UpdateView();
        }
    }

    private void UpdateView()
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
        
        // fill grid layout with all non-exposed items with non-0 quantity
        foreach (Item item in model.Items)
        {
            if (item.Quantity > 0 && !item.Exposed)
            {
                ItemView view = itemViewPrefab.InstantiateUnder(grid).GetComponentOrFail<ItemView>();
                view.SetInventoryView(this);
                view.AssignModel(item);
            }
        }
        
        RefreshInteractableState();
    }

    public void SetAllowInteractions(bool allowInteractions)
    {
        this.allowInteractions = allowInteractions;

        if (isActiveAndEnabled)
        {
            // we are already enabled on an active object, so we cannot count
            // on the next OnEnable... refresh now. No need to use the double toggle trick either.
            canvasGroup.interactable = allowInteractions;
        }
    }
    
    private void RefreshInteractableState()
    {
        // Ugly hack to refresh interaction state on new buttons that have just been added
        // unfortunately they may flash to white then gray due to the state change
        // to hide this we'll need to change the state under the hood (e.g. while Canvas Group alpha is still 0)
        // Right now, they don't seem to flash.
        canvasGroup.interactable = !allowInteractions;
        canvasGroup.interactable = allowInteractions;
    }
}
