using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject info;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform[] inventorys;
    [SerializeField]
    private Toggle[] menuToggle;

    private void Awake()
    {
        inventoryPanel.SetActive(true);
        for (int i = 0; i < inventorys.Length; i++)
        {
            scrollRect.content = inventorys[i];
            inventorys[i].gameObject.SetActive(false);
        }
        scrollRect.content = inventorys[0];
        inventorys[0].gameObject.SetActive(true);
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.status])) menuToggle[0].isOn = !menuToggle[0].isOn;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.inventory])) menuToggle[1].isOn = !menuToggle[1].isOn;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.awaken])) menuToggle[2].isOn = !menuToggle[2].isOn;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.quest])) menuToggle[3].isOn = !menuToggle[3].isOn;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.setting])) menuToggle[4].isOn = !menuToggle[4].isOn;
    }
}
