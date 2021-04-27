using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public static ItemGenerator instance;
    [SerializeField]
    private Item item;
    private Dictionary<string, object> rarity;
    private Dictionary<string, object> rarityAdd;
    private List<Item> itemList = new List<Item>();
    private List<string> additionalList = new List<string>();
    private string rarityType = "Normal";
    [SerializeField]
    private GameObject itemPrefab;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void DropItem(int rarityMin, int rarityMax, string type, Vector3 pos)
    {
        GameObject clone;
        int num = Random.Range(0, System.Enum.GetNames(typeof(ItemType)).Length);
        if (num == (int)ItemType.equipment)
        {
            Filtering(rarityMin, rarityMax, DataManager.itemEquipmentDB);
            RandomRarity(type);
            ItemInit();
            Additional();
            clone = Instantiate(itemPrefab, pos, Quaternion.identity);
            clone.GetComponent<ItemScript>().Init(item);
        }
        else if (num == (int)ItemType.consume)
        {
            Filtering(rarityMin, rarityMax, DataManager.itemConsumeDB);
            if (itemList.Count == 0) return;
            item = itemList[Random.Range(0, itemList.Count)];
            clone = Resources.Load<GameObject>("Prefabs/Items/" + item.name);
            clone = Instantiate(clone, pos, Quaternion.identity);
            clone.GetComponent<ItemScript>().Init(item);
        }
    }

    private void Filtering(int rarityMin, int rarityMax, Dictionary<string, Item> itemDB)
    {
        itemList.Clear();
        foreach(string key in itemDB.Keys)
        {
            if (rarityMin <= itemDB[key].rarity && rarityMax >= itemDB[key].rarity)
            {
                Item item = new Item();
                item.DeepCopy(itemDB[key]);
                itemList.Add(item);
            }
        }
    }

    private void RandomRarity(string type)
    {
        rarity = DataManager.rarity.FindDic("Function", type);
        int[] sort = { (int)rarity["Legendary"], (int)rarity["Unique"], (int)rarity["Rare"], (int)rarity["Magic"], (int)rarity["HiQuality"], (int)rarity["Normal"] };
        string[] types = { "Legendary", "Unique", "Rare", "Magic", "HiQuality", "Normal" };
        for (int i = 0; i < sort.Length - 1; i++) {
            for (int j = i + 1; j < sort.Length; j++) {
                if (sort[i] > sort[j]) {
                    int tempInt = sort[i];
                    sort[i] = sort[j];
                    sort[j] = tempInt;
                    string tempString = types[i];
                    types[i] = types[j];
                    types[j] = tempString;
                }
            }
        }
        int rand = Random.Range(0, sort[sort.Length - 1]);
        if (rand <= sort[0]) rarityType = types[0];
        else if (rand <= sort[1]) rarityType = types[1];
        else if (rand <= sort[2]) rarityType = types[2];
        else if (rand <= sort[3]) rarityType = types[3];
        else if (rand <= sort[4]) rarityType = types[4];
        else if (rand <= sort[5]) rarityType = types[5];
    }

    private void ItemInit()
    {
        item = itemList[Random.Range(0, itemList.Count)];
        item.rarityType = rarityType;
        item.stat = Random.Range(item.statMin, item.statMax);
    }

    private void Additional()
    {
        additionalList.Clear();
        rarityAdd = DataManager.rarity.FindDic("Function", "rarityAdd");
        int rand = Random.Range(0, 100);
        int rarityAll = item.rarity + (int)rarityAdd[rarityType];
        if (rand > 25) Mathf.Max(1, rarityAll - 1);
        for (int i = 0; i < DataManager.additional.Count; i++)
        {
            if (rarityAll == (int)DataManager.additional[i]["rarity"])
                additionalList.Add(DataManager.additional[i]["code"].ToString());
        }
        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, additionalList.Count);
            Dictionary<string, object> additional = DataManager.additional.FindDic("code", additionalList[rand]);
            item.nameAdd[i] = additional["code"].ToString();
            item.statusAdd[i] = additional["status"].ToString();
            item.statAdd[i] = Random.Range((int)additional["statMin"], (int)additional["statMax"]);
            item.cost += (int)additional["cost"];
        }
    }
}
