﻿public enum ItemType { equipment = 0, consume }

public class Item
{
    private const int additionalMax = 3;

    public string name;
    public int spawnable;
    public string useType;
    public string type;
    public string itemImage;
    public string inventoryImage;
    public string status;
    public int rarity;
    public int statMin;
    public int statMax;
    public int cost;
    public int stat;
    public string rarityType;
    public string[] nameAdd = new string[additionalMax];
    public string[] statusAdd = new string[additionalMax];
    public int[] statAdd = new int[additionalMax];
    public int quality = 0;
    public bool isNew = true;
    public Skill skill;

    public void DeepCopy(Item item)
    {
        name = item.name;
        spawnable = item.spawnable;
        useType = item.useType;
        type = item.type;
        status = item.status;
        rarity = item.rarity;
        statMin = item.statMin;
        statMax = item.statMax;
        cost = item.cost;
        itemImage = item.itemImage;
        inventoryImage = item.inventoryImage;
    }
}
