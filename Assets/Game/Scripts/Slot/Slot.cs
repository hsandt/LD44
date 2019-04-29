using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField, Tooltip("Index of the slot. 0 for front slot.")]
    private int index = 0;
    public int Index => index;


    /* State vars */

    /// Current item placed there, null if slot is not used
    private Item currentItem = null;
    public Item CurrentItem
    {
        get => currentItem;
        set => currentItem = value;
    }
    public bool IsFilled => CurrentItem != null;
}
