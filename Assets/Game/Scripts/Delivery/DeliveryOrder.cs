using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsHelper;

public class DeliveryOrder : MonoBehaviour
{
    [Serializable]
    public struct SingleItemOrder
    {
        public ItemData itemData;
        public int quantity;
    }

    [SerializeField, ReadOnlyField, Tooltip("List of items to order next")]
    private List<SingleItemOrder> itemOrders = new List<SingleItemOrder>();
    public List<SingleItemOrder> ItemOrders => itemOrders;

    public void AddItemOrder(ItemData itemData, int quantity)
    {
        itemOrders.Add(new SingleItemOrder{itemData = itemData, quantity = quantity});
    }

    public void AddItemOrders(List<SingleItemOrder> newItemOrders)
    {
        itemOrders.AddRange(newItemOrders);
    }

    /// Call when order has been taken into account
    public void Clear()
    {
        itemOrders.Clear();
    }
}
