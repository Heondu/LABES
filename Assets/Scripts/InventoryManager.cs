using UnityEngine;

public enum UseType { weapon = 0, equipment, skill, consume, rune }

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryItem inventoryWeapon;
    public InventoryItem inventoryEquipment;
    public InventorySkill inventorySkill;
    public InventoryItem inventoryConsume;
    public delegate void OnSlotChanged();
    public OnSlotChanged onSlotChangedCallback;
    public delegate void OnItemEquipChanged(Item item);
    public OnItemEquipChanged onItemEquipCallback;
    public delegate void OnItemUnequipChanged(Item item);
    public OnItemUnequipChanged onItemUnequipCallback;
    private SlotDrag draggingSlot;
    [SerializeField]
    private GameObject draggingObject;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        draggingSlot = draggingObject.GetComponent<SlotDrag>();
    }

    public void AddItem(Item newItem)
    {
        if (newItem.useType == "weapon") inventoryWeapon.AddItem(newItem);
        else if (newItem.useType == "equipment") inventoryEquipment.AddItem(newItem);
        else if (newItem.useType == "consume") inventoryConsume.AddItem(newItem);
    }
    
    public void AddSkill(Skill newSkill)
    {
        inventorySkill.AddSkill(newSkill);
    }

    public void RemoveItem(Item selectedItem)
    {
        if (selectedItem.useType == "weapon") inventoryWeapon.RemoveItem(selectedItem);
        else if (selectedItem.useType == "equipment") inventoryEquipment.RemoveItem(selectedItem);
        else if (selectedItem.useType == "consume") inventoryConsume.RemoveItem(selectedItem);
    }

    public void OnBeginDrag(Slot selectedSlot)
    {
        draggingObject.SetActive(true);
        draggingSlot.icon.sprite = selectedSlot.icon.sprite;
    }

    public void OnDrag()
    {
        draggingObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;
    }

    public void OnEndDrag(Slot selectedSlot, Slot targetSlot)
    {
        draggingObject.SetActive(false);
        if (targetSlot == null)
        {
            selectedSlot.icon.color = Color.white;
            return;
        }
        if (selectedSlot.useType == targetSlot.useType)
        {
            if (selectedSlot.useType == UseType.equipment)
            {
                if (targetSlot.equipType == "" || selectedSlot.item.type == targetSlot.equipType)
                {
                    targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
                    selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
                    selectedSlot.inventory.UpdateInventory();
                    targetSlot.inventory.UpdateInventory();
                }
                else
                {
                    selectedSlot.icon.color = Color.white;
                }
            }
            else
            {
                targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
                selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
                selectedSlot.inventory.UpdateInventory();
                targetSlot.inventory.UpdateInventory();
            }
        }
        else
        {
            selectedSlot.icon.color = Color.white;
        }
    }
}
