using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISESlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI volumeText;

    private void Awake()
    {
        slider.onValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float value)
    {
        volumeText.text = ((int)(value * 10)).ToString();
        SettingsManager.setSE(value);
    }
}
