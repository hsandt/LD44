using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [Tooltip("Title Menu to hide on game start")]
    public GameObject titleMenu;
    
    [Tooltip("Inventory View")]
    public InventoryView inventoryView;

    [SerializeField, Tooltip("List of items to order initially. Set it for session start.")]
    private List<DeliveryOrder.SingleItemOrder> initialItemOrders = new List<DeliveryOrder.SingleItemOrder>();

    void Start()
    {
        // Cleanup the scene for all things phase managers
        // didn't do with their own OnDisableCallbacks
        
        // Hide non-phase-specific UI
        inventoryView.gameObject.SetActive(false);
    }
    
    public void StartGameSession()
    {
        // One-time setup for a given session, this allows initializing initial
        // item list, etc. without repeating it every day
        
        // Hide title and title menu
        titleMenu.SetActive(false);
        
        // Deliver initial set of items
        DeliveryManager.Instance.AddDeliveryOrder(initialItemOrders);

        // Start at first phase
        PhaseSwitcher.Instance.OnStartGameSession();
    }

}
