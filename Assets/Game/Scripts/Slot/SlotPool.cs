using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class SlotPool : MonoBehaviour
{
    [SerializeField, Tooltip("Array of item slots in the store")]
    private Slot[] slots;

    public Slot GetNextFreeSlot()
    {
        return slots.FirstOrDefault(slot => !slot.IsFilled);
    }
}
