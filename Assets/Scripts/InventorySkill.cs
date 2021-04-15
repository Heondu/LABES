using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : Inventory
{
    public List<Skill> skills = new List<Skill>();
    public Slot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < transform.childCount; i++)
            skills.Add(null);
    }

    public void AddSkill(Skill newSkill)
    {
        int index = FindSlot(null);
        if (index != -1) skills[index] = newSkill;
        UpdateInventory();
    }

    public override void ChangeSlot(Slot selectedSlot, Slot targetSlot)
    {
        skills[targetSlot.index] = selectedSlot.skill;
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
