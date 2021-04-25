using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : Inventory
{
    public List<Item> items = new List<Item>();
    public Slot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
            items.Add(null);
    }

    public void AddItem(Item newItem)
    {
        int index = FindSlot(null);
        if (index != -1) items[index] = newItem;
        OnNotify();
        UpdateInventory();
    }

    public override void ChangeSlot(Slot selectedSlot, Slot targetSlot)
    {
        items[targetSlot.index] = selectedSlot.item;
    }

    public void RemoveItem(Item targetItem)
    {
        int index = FindSlot(targetItem);
        if (index != -1) items[index] = null;
        UpdateInventory();
    }

    public int FindSlot(Item targetItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == targetItem) return i;
        }
        return -1;
    }

    public override void UpdateInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].item = items[i];
        }

        InventoryManager.instance.onSlotChangedCallback.Invoke();
    }
}
