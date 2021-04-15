using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Skill skill;
    public int index;
    public Image icon;
    public Image gradeBG;
    public TextMeshProUGUI qualty;
    private Image lockIcon;
    public bool isLock;
    public UseType useType;
    public string equipType;
    public Inventory inventory;

    protected virtual void Awake()
    {
        if (useType == UseType.weapon || useType == UseType.equipment || useType == UseType.consume)
            inventory = GetComponentInParent<InventoryItem>();
        else if (useType == UseType.skill)
            inventory = GetComponentInParent<InventorySkill>();

        if (icon == null) icon = transform.Find("gradeBG").Find("Icon").GetComponent<Image>();
        if (transform.Find("gradeBG") != null) gradeBG = transform.Find("gradeBG").GetComponent<Image>();
        if (gradeBG != null) qualty = gradeBG.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();

        InventoryManager.instance.onSlotChangedCallback += UpdateSlot;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == gameObject.transform) index = i;
        }
    }

    private void OnEnable()
    {
        UpdateSlot();
    }

    private void UpdateSlot()
    {
        if (item != null)
        {
            icon.sprite = Resources.Load<Sprite>(item.inventoryImage);
            icon.color = Color.white;
        }
        else if (skill != null)
        {
            icon.sprite = Resources.Load<Sprite>(skill.image);
            icon.color = Color.white;
        }
        else
        {
            icon.color = Color.clear;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (item != null)
        {
            InventoryManager.instance.OnBeginDrag(this);
            if (item.useType == "equipment" || item.useType == "weapon") icon.color = Color.clear;
        }
        else if (skill != null)
        {
            InventoryManager.instance.OnBeginDrag(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.instance.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (eventData == null) InventoryManager.instance.OnEndDrag(this, null);
        else InventoryManager.instance.OnEndDrag(this, eventData.pointerEnter.GetComponent<Slot>());
    }
}
