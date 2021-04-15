using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public static PopupUI instance;
    [SerializeField]
    private GameObject popup;
    private RectTransform popupRect;
    private Image icon;
    private new Text name;
    private Text qualityName;
    private Text baseStatusTitle;
    private Text baseStatus;
    private Text addStatusTitle1;
    private Text addStatusTitle2;
    private Text addStatusTitle3;
    private Text addStatus1;
    private Text addStatus2;
    private Text addStatus3;
    private Slot selectedSlot;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        popupRect = popup.GetComponent<RectTransform>();
        icon = popup.transform.Find("ItemIcon").GetComponent<Image>();
        name = popup.transform.Find("Name").GetComponent<Text>();
        qualityName = popup.transform.Find("QualityName").GetComponent<Text>();
        baseStatusTitle = popup.transform.Find("BaseStatusTitle").GetComponent<Text>();
        baseStatus = popup.transform.Find("BaseStatusTitle").Find("Status").GetComponent<Text>();
        addStatusTitle1 = popup.transform.Find("StatusTitle (1)").GetComponent<Text>();
        addStatusTitle2 = popup.transform.Find("StatusTitle (2)").GetComponent<Text>();
        addStatusTitle3 = popup.transform.Find("StatusTitle (3)").GetComponent<Text>();
        addStatus1 = popup.transform.Find("StatusTitle (1)").Find("Status").GetComponent<Text>();
        addStatus2 = popup.transform.Find("StatusTitle (2)").Find("Status").GetComponent<Text>();
        addStatus3 = popup.transform.Find("StatusTitle (3)").Find("Status").GetComponent<Text>();
    }

    //private void Update()
    //{
    //    if (popup.activeSelf) PopupFollowMouse();
    //    if (popup.activeSelf != false && selectedSlot != null && selectedSlot.item != null) UpdatePopup(selectedSlot);
    //    else SetActive(false);
    //}

    //public void UpdatePopup(Slot selectedSlot)
    //{
    //    this.selectedSlot = selectedSlot;
    //    icon.sprite = selectedSlot.icon.sprite;
    //    name.text = $"{DataManager.Localization(selectedSlot.item.nameAdd[0])} {DataManager.Localization(selectedSlot.item.name)}";
    //    qualityName.text = $"{DataManager.Localization(selectedSlot.item.rarityType)} {DataManager.Localization(selectedSlot.item.type)}";
    //    baseStatusTitle.text = DataManager.Localization(selectedSlot.item.status);
    //    baseStatus.text = selectedSlot.item.stat.ToString();
    //    addStatusTitle1.text = DataManager.Localization(selectedSlot.item.statusAdd[0]);
    //    addStatus1.text = selectedSlot.item.statAdd[0].ToString();
    //    addStatusTitle2.text = DataManager.Localization(selectedSlot.item.statusAdd[1]);
    //    addStatus2.text = selectedSlot.item.statAdd[1].ToString();
    //    addStatusTitle3.text = DataManager.Localization(selectedSlot.item.statusAdd[2]);
    //    addStatus3.text = selectedSlot.item.statAdd[2].ToString();
    //}

    public void SetActive(bool value)
    {
        popup.SetActive(value);
    }

    private void PopupFollowMouse()
    {
        popup.transform.position = Input.mousePosition;
        if (popup.transform.position.y < Screen.height / 2) popupRect.pivot = Vector2.zero;
        else popupRect.pivot = Vector2.up;
        if (popup.transform.position.x + popupRect.sizeDelta.x > Screen.width) popupRect.pivot = new Vector2(1, popupRect.pivot.y);
    }
}
