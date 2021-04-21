using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public enum ItemRarity { Normal = 0, HiQuality, Magic, Rare, Unique, Legendary }

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Skill skill;
    public int index;
    public Image icon;
    public Image gradeBG;
    public Image gradeFrame;
    public Sprite[] gradeBGSprite;
    public Sprite[] gradeFrameSprite;
    public TextMeshProUGUI quality;
    private Image lockIcon;
    public bool isLock;
    public UseType useType;
    public string equipType;
    public Inventory inventory;
    public Popup popup;
    public bool isEquip;
    [SerializeField]
    private Notification notification;
    private bool isNotified = false;

    protected virtual void Awake()
    {
        if (useType == UseType.weapon || useType == UseType.equipment || useType == UseType.consume)
            inventory = GetComponentInParent<InventoryItem>();
        else if (useType == UseType.skill)
            inventory = GetComponentInParent<InventorySkill>();

        if (icon == null) icon = transform.Find("gradeBG").Find("Icon").GetComponent<Image>();
        if (transform.Find("gradeBG") != null) gradeBG = transform.Find("gradeBG").GetComponent<Image>();
        if (transform.Find("GradeFrame") != null) gradeFrame = transform.Find("GradeFrame").GetComponent<Image>();
        if (gradeBG != null) quality = gradeBG.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();

        InventoryManager.instance.onSlotChangedCallback += UpdateSlot;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == gameObject.transform) index = i;
        }
    }

    private void Start()
    {
        inventory.onDisable.AddListener(OnNotify);
    }

    private void OnEnable()
    {
        UpdateSlot();
    }

    private void OnNotify(bool isNotified)
    {
        if (notification == null) return;

        if (isNotified)
        {
            if (this.isNotified == false)
            {
                notification.Notify(true);
                this.isNotified = true;
            }
        }
        else
        {
            notification.Notify(false);
        }
    }

    private void UpdateSlot()
    {
        if (item != null)
        {
            icon.sprite = Resources.Load<Sprite>(item.inventoryImage);
            icon.color = Color.white;
            if (useType == UseType.weapon || useType == UseType.equipment)
            {
                if (gradeBG != null && gradeFrame != null)
                {
                    gradeBG.sprite = gradeBGSprite[(int)Enum.Parse(typeof(ItemRarity), item.rarityType)];
                    gradeFrame.sprite = gradeFrameSprite[(int)Enum.Parse(typeof(ItemRarity), item.rarityType)];
                }
            }
            OnNotify(true);
        }
        else if (skill != null)
        {
            icon.sprite = Resources.Load<Sprite>(skill.image);
            icon.color = Color.white;
            OnNotify(true);
        }
        else
        {
            icon.color = Color.clear;
            if (useType == UseType.weapon || useType == UseType.equipment)
            {
                if (gradeBG != null && gradeFrame != null)
                {
                    gradeBG.sprite = gradeBGSprite[0];
                    gradeFrame.sprite = gradeFrameSprite[0];
                }
            }
            isNotified = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (popup != null)
        {
            popup.gameObject.SetActive(true);
            if (useType == UseType.weapon || useType == UseType.equipment) popup.UpdateItemInfo(this);
            if (useType == UseType.skill) popup.UpdateSkillInfo(this);
            if (useType == UseType.consume) popup.UpdateConsumeInfo(this);

            OnNotify(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (item != null) InventoryManager.instance.OnBeginDrag(this);
        else if (skill != null) InventoryManager.instance.OnBeginDrag(this);
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
