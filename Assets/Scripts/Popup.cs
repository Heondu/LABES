using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image gradeBG;
    [SerializeField] private Image gradeFrame;
    [SerializeField] private Sprite[] gradeBGSprite;
    [SerializeField] private Sprite[] gradeFrameSprite;
    [SerializeField] private TextMeshProUGUI quality;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI isEquipText;
    [SerializeField] private new TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI mainStatText;
    [SerializeField] private TextMeshProUGUI mainStatValue;
    [SerializeField] private TextMeshProUGUI[] addStatText;
    [SerializeField] private TextMeshProUGUI[] addStatValue;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hiQualityColor;
    [SerializeField] private Color magicColor;
    [SerializeField] private Color rareColor;
    [SerializeField] private Color uniqueColor;
    [SerializeField] private Color legendaryColor;
    private Slot slot;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void UpdateInfo(Slot slot)
    {
        if (slot.item == null && slot.skill == null) return;
        
        gameObject.SetActive(true);

        if (slot.item != null)
        {
            if (slot.item.useType == "weapon" || slot.item.useType == "equipment")
            {
                UpdateItemInfo(slot);
            }
            else if (slot.item.useType == "consume")
            {
                UpdateConsumeInfo(slot);
            }
        }
        else if (slot.skill != null)
        {
            UpdateSkillInfo(slot);
        }
    }

    private void UpdateItemInfo(Slot slot)
    {
        this.slot = slot;
        icon.sprite = Resources.Load<Sprite>(slot.item.inventoryImage);
        gradeBG.sprite = gradeBGSprite[(int)Enum.Parse(typeof(ItemRarity), slot.item.rarityType)];
        gradeFrame.sprite = gradeFrameSprite[(int)Enum.Parse(typeof(ItemRarity), slot.item.rarityType)];
        typeText.text = DataManager.Localization(slot.item.type);
        isEquipText.text = slot.isEquip == true ? "ÀåÂøÁß" : "¹ÌÂø¿ë";
        name.text = $"{DataManager.Localization(slot.item.nameAdd[0])} {DataManager.Localization(slot.item.name)}";

        switch (slot.item.rarityType)
        {
            case "Normal": name.color = normalColor; break;
            case "HiQuality": name.color = hiQualityColor; break;
            case "Magic": name.color = magicColor; break;
            case "Rare": name.color = rareColor; break;
            case "Unique": name.color = uniqueColor; break;
            case "Legendary": name.color = legendaryColor; break;
        }

        mainStatText.text = DataManager.Localization(slot.item.status);
        mainStatValue.text = slot.item.stat.ToString();
        for (int i = 0; i < addStatText.Length; i++)
        {
            addStatText[i].text = DataManager.Localization(slot.item.statusAdd[i]);
            addStatValue[i].text = slot.item.statAdd[i].ToString();
        }
    }

    private void UpdateSkillInfo(Slot slot)
    {
        this.slot = slot;
        icon.sprite = Resources.Load<Sprite>(slot.skill.image);
        typeText.text = $"{DataManager.Localization(slot.skill.element)}, {DataManager.Localization(slot.skill.weaponClass)}";
        isEquipText.text = slot.isEquip == true ? "ÀåÂøÁß" : "¹ÌÂø¿ë";
        name.text = DataManager.Localization(slot.skill.name);
        mainStatText.text = DataManager.Localization(slot.skill.relatedStatus[0]);
        if (slot.skill.relatedStatus[0] != "none")
        {
            if (slot.skill.relatedStatus[0] == "damage")
                mainStatValue.text = Mathf.RoundToInt(player.GetStatus(slot.skill.relatedStatus[0]).Value * ((float)slot.skill.amount[0] / 100) + player.GetStatus("fixDamage").Value).ToString();
            else mainStatValue.text = Mathf.RoundToInt(player.GetStatus(slot.skill.relatedStatus[0]).Value * ((float)slot.skill.amount[0] / 100)).ToString();
        }
        addStatText[0].text = "ÄðÅ¸ÀÓ";
        addStatValue[0].text = slot.skill.cooltime.ToString();
        addStatValue[1].text = slot.skill.repeat.ToString();
        addStatValue[2].text = slot.skill.penetration.ToString();
    }

    private void UpdateConsumeInfo(Slot slot)
    {
        this.slot = slot;
        icon.sprite = Resources.Load<Sprite>(slot.item.inventoryImage);
        typeText.text = DataManager.Localization(slot.item.type);
        isEquipText.text = slot.isEquip == true ? "ÀåÂøÁß" : "¹ÌÂø¿ë";
        name.text = DataManager.Localization(slot.item.name);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }

    public void Equip()
    {
        InventoryManager.instance.QuickEquip(slot);
        gameObject.SetActive(false);
    }
}
