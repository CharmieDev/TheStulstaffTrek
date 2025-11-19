using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSetting : Setting
{
    [SerializeField] private string settingName;
    private protected override string saveKey => settingName;
    private static readonly int defaultValue = 0;
    public static int CurrentValue { get; private set; }

    [SerializeField] private List<string> options = new List<string>();
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI valueText;
    
    private void OnEnable()
    {
        dropdown.value = CurrentValue;
        dropdown.options.Clear();
        foreach (var option in options)
        {
            dropdown.options.Add(new Dropdown.OptionData(option));
        }
    }

    public override void Load()
    {
        CurrentValue = SaveManager.SettingsStore.GetInt(saveKey, defaultValue);
        dropdown.value = CurrentValue;
    }

    public override void Apply()
    {
        int sensitivityValue = (int)dropdown.value;
        SaveManager.SettingsStore.SetInt(saveKey, sensitivityValue);
        CurrentValue = sensitivityValue;
    }

    public override void Discard()
    {
        dropdown.value = CurrentValue;
    }

    public void OnValueChanged()
    {
        if(valueText != null)
        {
            valueText.text = dropdown.value.ToString("0");
        }
         
    }
    
    public override bool IsDirty() => dropdown.value != CurrentValue;
}
