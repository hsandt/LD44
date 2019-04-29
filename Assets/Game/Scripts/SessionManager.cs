using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField, Tooltip("List of items to order initially. Set it for session start.")]
    private List<DeliveryOrder.SingleItemOrder> initialItemOrders = new List<DeliveryOrder.SingleItemOrder>();
    
    void Start()
    {
        Setup();
    }

    /// One-time setup for a given session, this allows initializing initial
    /// item list, etc. without repeating it every day
    void Setup()
    {
        // Deliver initial set of items
        DeliveryManager.Instance.AddDeliveryOrder(initialItemOrders);
    }
}
