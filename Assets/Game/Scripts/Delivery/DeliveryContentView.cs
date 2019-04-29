using System.Collections;
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
    
    public void UpdateText(List<DeliveryOrder.SingleItemOrder> order)
    {
        if (order.Count > 0)
        {
            header.text = "Delivery has arrived!";

            StringBuilder sb = new StringBuilder();
            foreach (var itemOrder in order)
            {
                sb.AppendFormat("{0}\t+{1}", itemOrder.itemData.itemName, itemOrder.quantity);
                sb.AppendLine();
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
