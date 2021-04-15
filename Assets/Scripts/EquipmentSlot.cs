using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    private Item item = null;
    private Slot slot;

    private void Awake()
    {
        InventoryManager.instance.onSlotChangedCallback += EquipCheck;
        slot = GetComponent<Slot>();
    }

    private void EquipCheck()
    {
        if (item != slot.item)
        {
            if (slot.item != null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequipCallback.Invoke(item);
                }
                InventoryManager.instance.onItemEquipCallback.Invoke(slot.item);
            }
            else if (slot.item == null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequipCallback.Invoke(item);
                }
            }

            item = slot.item;
        }
    }
}
