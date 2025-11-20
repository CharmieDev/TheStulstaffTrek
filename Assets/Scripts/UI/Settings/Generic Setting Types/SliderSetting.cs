using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderSetting : Setting
{
    [SerializeField] private string settingName;
    private protected override string saveKey => settingName;
    private static readonly int defaultValue = 50;
    public static float CurrentValue { get; private set; }

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;
    
    private void OnEnable()
    {
        slider.value = CurrentValue;
    }

    public override void Load()
    {
        CurrentValue = SaveManager.SettingsStore.GetInt(saveKey, defaultValue);
        slider.value = CurrentValue;
    }

    public override void Apply()
    {
        int sliderValue = (int)slider.value;
        SaveManager.SettingsStore.SetInt(saveKey, sliderValue);
        CurrentValue = sliderValue;
    }

    public override void Discard()
    {
        slider.value = CurrentValue;
    }

    public void OnValueChanged()
    {
        if(valueText != null)
        {
            valueText.text = slider.value.ToString("0");
        }
         
    }
    
    public override bool IsDirty() => slider.value != CurrentValue;
}
