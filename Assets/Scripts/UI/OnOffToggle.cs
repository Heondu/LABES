using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnOffToggle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject onObject;
    [SerializeField]
    private GameObject offObject;
    public bool isOn = false;
    [SerializeField]
    private UnityEvent<bool> onValueChanged;

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;

        onObject.SetActive(isOn);
        offObject.SetActive(!isOn);

        onValueChanged.Invoke(isOn);
    }
}
