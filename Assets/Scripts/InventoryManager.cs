using UnityEngine;


public enum UseType { weapon = 0, equipment, skill, consume, rune, equipSlot }

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryItem inventoryWeapon;
    public InventoryItem inventoryEquipment;
    public InventorySkill inventorySkill;
    public InventoryItem inventoryConsume;
    public InventoryItem equipSlotL;
    public InventoryItem equipSlotR;
    public InventorySkill skillSlot;
    public InventoryItem consumeSlot;
    public delegate void OnSlotChanged();
    public OnSlotChanged onSlotChangedCallback;
    public delegate void OnItemEquipChanged(Item item);
    public OnItemEquipChanged onItemEquipCallback;
    public delegate void OnItemUnequipChanged(Item item);
    public OnItemUnequipChanged onItemUnequipCallback;
    private SlotDrag draggingSlot;
    [SerializeField]
    private GameObject draggingObject;
    public Notification notification;

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
        notification.Notify(true);
        notification.IncreaseNum();
    }
    
    public void AddSkill(Skill newSkill)
    {
        inventorySkill.AddSkill(newSkill);
        notification.Notify(true);
        notification.IncreaseNum();
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
        if (targetSlot == null) return;

        if (targetSlot.useType == UseType.equipSlot)
        {
            if (selectedSlot.useType == UseType.weapon && targetSlot.equipType == "weapon") Equip(selectedSlot, targetSlot, true);
            else if (selectedSlot.useType == UseType.equipment && selectedSlot.item.type == targetSlot.equipType) Equip(selectedSlot, targetSlot, true);
            else if (selectedSlot.useType == UseType.skill && targetSlot.equipType == "skill") Equip(selectedSlot, targetSlot, true);
            else if (selectedSlot.useType == UseType.consume && targetSlot.equipType == "consume") Equip(selectedSlot, targetSlot, true);
        }
        else if (selectedSlot.useType == UseType.equipSlot)
        {
            if (targetSlot.equipType == "") Equip(selectedSlot, targetSlot, false);
            else if (selectedSlot.equipType == targetSlot.equipType)
            {
                targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
                selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
                selectedSlot.inventory.UpdateInventory();
                targetSlot.inventory.UpdateInventory();
            }
            else if (selectedSlot.equipType != targetSlot.equipType) return;
        }
        else if (selectedSlot.useType == targetSlot.useType)
        {
            targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
            selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
            selectedSlot.inventory.UpdateInventory();
            targetSlot.inventory.UpdateInventory();
        }
    }

    private void Equip(Slot selectedSlot, Slot targetSlot, bool isEquip)
    {
        if (isEquip) targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
        else selectedSlot.inventory.ChangeSlot(null, selectedSlot);
        selectedSlot.inventory.UpdateInventory();
        targetSlot.inventory.UpdateInventory();
    }

    public void QuickEquip(Slot selectedSlot)
    {
        if (selectedSlot.useType == UseType.weapon)
        {
            Equip(selectedSlot, equipSlotL.slots[0], true);
        }
        else if (selectedSlot.useType == UseType.equipment)
        {
            for (int i = 0; i < equipSlotL.slots.Length; i++)
            {
                if (selectedSlot.item.type == equipSlotL.slots[i].equipType)
                {
                    Equip(selectedSlot, equipSlotL.slots[i], true);
                    return;
                }
            }
            for (int i = 0; i < equipSlotR.slots.Length; i++)
            {
                if (selectedSlot.item.type == equipSlotR.slots[i].equipType)
                {
                    Equip(selectedSlot, equipSlotR.slots[i], true);
                    return;
                }
            }
        }
        else if (selectedSlot.useType == UseType.skill)
        {
            for (int i = 0; i < skillSlot.slots.Length; i++)
            {
                if (skillSlot.slots[i].skill == null)
                {
                    Equip(selectedSlot, skillSlot.slots[i], true);
                    return;
                }
            }
        }
        else if (selectedSlot.useType == UseType.consume)
        {
            for (int i = 0; i < consumeSlot.slots.Length; i++)
            {
                if (consumeSlot.slots[i].item == null)
                {
                    Equip(selectedSlot, consumeSlot.slots[i], true);
                    return;
                }
            }
        }
    }
}
