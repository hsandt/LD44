using System;

[Serializable]
public class ItemState
{
    public int quantity = 0;
    public bool exposed = false;

    /// Index of the slot this item is exposed at. Only need be set if exposed is true.
    public int slotIndex = -1;
}
