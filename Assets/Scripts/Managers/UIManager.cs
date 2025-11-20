using Febucci.UI;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public Menu SettingsMenu { get; private set; }
    [field: SerializeField] public Menu PauseMenu { get; private set; }
    [field: SerializeField] public Menu DialogueMenu { get; private set; }
    [field: SerializeField] public FaderMenu FaderMenu { get; private set; }
    
    [field: SerializeField] public TypewriterByCharacter shootTypewriter { get; private set; }
    [field: SerializeField] public UnityEvent OnApplySettings { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        HideAllMenus();
    }

    public void Start()
    {
        InvokeApplySettings();
    }

    public void OpenMenu(Menu menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void HideAllMenus()
    {
        SettingsMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        DialogueMenu.gameObject.SetActive(false);
        FaderMenu.gameObject.SetActive(false);
    }

    public void FadeIn(float duration)
    {
        FaderMenu.gameObject.SetActive(true);
        FaderMenu.FadeInMenu(duration);
    }
    public void FadeOut(float duration)
    {
        FaderMenu.FadeOutMenu(duration);
    }

    public void DisplayShootText(string text)
    {
        shootTypewriter.ShowText(text);
    }

    public void InvokeApplySettings()
    {
        OnApplySettings?.Invoke();
    }
}
