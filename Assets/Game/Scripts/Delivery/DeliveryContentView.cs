﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class DeliveryContentView : MonoBehaviour
{
    [Tooltip("Header title text summing the delivery status")]
    public TextMeshProUGUI header;
    
    [Tooltip("Actual delivery content details")]

    public TextMeshProUGUI content;

    void OnEnable()
    {
        // update text now; we don't even need an event that marks this view as dirty
        // when DeliverManager.Instance.nextDeliveryOrder changes, since this can only happen
        // during the SessionManage init and the Order phase, in which this view is inactive
        UpdateText(DeliveryManager.Instance.nextDeliveryOrder.ItemOrders);
    }

    private void UpdateText(List<DeliveryOrder.SingleItemOrder> order)
    {
        if (order.Count > 0)
        {
            header.text = "Delivery has arrived!";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < order.Count; ++i)
            {
                var itemOrder = order[i];
                sb.AppendFormat("{0}\t+{1}", itemOrder.itemData.itemName, itemOrder.quantity);
                if (i + 1 < order.Count)
                {
                    // extra line if there is another item afterward
                    sb.AppendLine();
                }
            }
            content.text = sb.ToString();
        }
        else
        {
            header.text = "No delivery today!";
            content.text = "";
        }
    }
}
