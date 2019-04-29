using System;

[Serializable]
public struct ItemState
{
    // True iff the item has been obtained at least once
    // It allows us to show it in gray even when quantity is reduced to 0
    // Currently unused, as we move item views around completely
//    public bool unlocked;
    public int quantity;
    public bool exposed;

    /// Index of the slot this item is exposed at. Only need be set if exposed is true.
    public int slotIndex;

    public ItemState(int quantity)
    {
        this.quantity = quantity;
        exposed = false;
        slotIndex = -1;
    }
}
