
using DG.Tweening;
using Febucci.UI;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class FaderMenu : Menu
{
    [SerializeField] private MMFader fader;
    [SerializeField] public TypewriterByCharacter typewriter;
    [SerializeField] public Image[] portraits;
    [SerializeField] public Image[] panels;
    [SerializeField] public Image[] papers;
    public void FadeInMenu(float duration)
    {
        typewriter.ShowText("");
        fader.FadeIn(duration, fader.DefaultTween, false);
    }

    public void FadeInObject(Image image, float duration)
    {
        image.gameObject.SetActive(true);
        image.DOFade(1f, duration);
    }
    
    public void FadeOutObject(Image image, float duration)
    {
        
        image.DOFade(0f, duration).OnComplete(delegate { image.gameObject.SetActive(false); });
    }
    
    public void FadeOutMenu(float duration)
    {   
        typewriter.StartDisappearingText();
        fader.FadeOut(duration, fader.DefaultTween, false);
    }

    public void TypeText(string text, float speed)
    {
        UIManager.Instance.OpenMenu(UIManager.Instance.FaderMenu);
        typewriter.SetTypewriterSpeed(speed);
        typewriter.ShowText(text);
    }
        
}