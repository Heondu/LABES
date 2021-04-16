using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnOffToggle : MonoBehaviour, IPointerClickHandler
{
    public bool isOn = false;
    [SerializeField]
    private UnityEvent<bool> onEvent;
    [SerializeField]
    private UnityEvent<bool> offEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;
        onEvent.Invoke(isOn);
        offEvent.Invoke(!isOn);
    }
}
