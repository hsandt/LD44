using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SlotPool : MonoBehaviour
{
    [SerializeField, Tooltip("Array of item slots in the store")]
    private Slot[] slots;

    void Start()
    {
        
    }

    public Slot GetNextFreeSlot()
    {
        return null;
    }
}
