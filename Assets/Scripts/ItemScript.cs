using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private string skill;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Item item)
    {
        this.item = item;
        if (skill != "") this.item.skill = DataManager.skillDB[skill];
        spriteRenderer.sprite = Resources.Load<Sprite>(item.itemImage);
    }

    public Item GetItem()
    {
        return item;
    }
}
