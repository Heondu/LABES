using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : Inventory
{
    public List<Skill> skills = new List<Skill>();
    public Slot[] slots;
    public Slot[] prevSlot;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        prevSlot = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
            skills.Add(null);
    }

    public void AddSkill(Skill newSkill)
    {
        int index = FindSlot(null);
        if (index != -1) skills[index] = newSkill;
        OnNotify();
        UpdateInventory();
    }

    public override void ChangeSlot(Slot selectedSlot, Slot targetSlot)
    {
        if (selectedSlot == null)
        {
            if (targetSlot.useType == UseType.equipSlot)
            {
                if (prevSlot[targetSlot.index] != null) prevSlot[targetSlot.index].isEquip = false;
            }
            skills[targetSlot.index] = null;
        }
        else
        {
            if (targetSlot.useType == UseType.equipSlot)
            {
                if (prevSlot[targetSlot.index] != null) prevSlot[targetSlot.index].isEquip = false;
                selectedSlot.isEquip = true;
                prevSlot[targetSlot.index] = selectedSlot;
            }
            skills[targetSlot.index] = selectedSlot.skill;
        }
    }

    public int FindSlot(Skill targetSkill)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].skill == targetSkill) return i;
        }
        return -1;
    }

    public override void UpdateInventory()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            slots[i].skill = skills[i];
        }

        InventoryManager.instance.onSlotChangedCallback.Invoke();
    }
}
