using UnityEngine;

public class Gold : MonoBehaviour, IItem
{
    private int gold = 0;

    public void Use()
    {
        InventoryManager.instance.AddGold(gold);
        Destroy(gameObject);
    }

    public void SetGold(int value)
    {
        gold = value;
    }
}
