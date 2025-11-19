using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : Menu
{
    private List<Setting[]> settings = new List<Setting[]>();
    [SerializeField] private UnityEngine.GameObject saveButtons;
    void Awake()
    {
        FindAllSettings();
        ChangeSubMenu(SubMenus[0]);
    }

    void Start()
    {
        foreach (var subMenu in settings)
        {
            foreach (Setting setting in subMenu)
            {
                setting?.Apply();
                setting?.Load();
            }
        }
    }

    void Update()
    {
        saveButtons.SetActive(IsDirty());
    }

    public override void ChangeSubMenu(UnityEngine.GameObject menu)
    {
        base.ChangeSubMenu(menu);
        LoadSettings();
    }

    void FindAllSettings()
    {
        foreach (var menu in SubMenus)
        {
            settings.Add(menu.transform.GetComponentsInChildren<Setting>());
        }
    }
    
    public void ApplySettings()
    {
        foreach (Setting setting in settings[CurrentSubMenu])
        {
            setting?.Apply();
        }
    }

    public void DiscardSettings()
    {
        foreach (Setting setting in settings[CurrentSubMenu])
        {
            setting?.Discard();
        }
    }

    public void LoadSettings()
    {
        foreach (Setting setting in settings[CurrentSubMenu])
        {
            setting?.Load();
        }
    }

    private bool IsDirty()
    {
        foreach (Setting setting in settings[CurrentSubMenu])
        {
            if (setting.IsDirty())
            {
                return true;
            }
        }

        return false;
    }
}
