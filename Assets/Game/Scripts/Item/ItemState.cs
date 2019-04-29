using System;

[Serializable]
public struct ItemState
{
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
