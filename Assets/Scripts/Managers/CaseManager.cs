using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public enum CasePhase
{
    Prologue,
    Witness,
    HoldingCell,
    Interrogation,
    Judgement,
    FinalJudgement,
    Escort,
    WinEnding,
    LoseEnding,
    Ending3
}
public class CaseManager : Singleton<CaseManager>
{
    [field: SerializeField] public CasePhase CurrentPhase { get; private set; } = CasePhase.Witness;
    [field: SerializeField] public Transform holdingCellParent { get; private set; }
    [field: SerializeField] public UnityEngine.GameObject BellDialogue { get; private set; }
    private float insanityLevel = 0f;
    
    [field: SerializeField] public UnityEngine.GameObject AudioParentObject { get; private set; }
    [field: SerializeField] public UnityEngine.GameObject ChaseKael;
    public void Start()
    {
        StartCoroutine(ChangePhaseCoroutine());
        BellDialogue.SetActive(false);
    }

    
    public void ChangePhase(CasePhase newPhase)
    {
        CurrentPhase = newPhase;
        StartCoroutine(ChangePhaseCoroutine());
    }

    [Button]
    public void RedoCurrentPhase()
    {
        StartCoroutine(ChangePhaseCoroutine());
    }
    
    private IEnumerator ChangePhaseCoroutine()
    {
        switch (CurrentPhase)
        {
            case CasePhase.Prologue:
                
                AudioManager.Instance.StopSong();
                
                GameManager.Instance.ChangeGameState(GameState.Cutscene);
                                
                UIManager.Instance.FadeIn(0.1f);
                
                yield return new WaitForSeconds(0.5f);
                //START PROLOGUE
                
                UIManager.Instance.FaderMenu.TypeText("This city doesn't sleep. It tosses. It turns.", 0.1f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.TypeText("It hides its monsters under streetlights and alley grates.", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.TypeText("Laurence Finch, my partner in crime and life, was one of the few good ones left.", 0.25f);
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.panels[2], 1f);
                
                yield return new WaitForSeconds(5f);
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.panels[2], 0.5f);
                
                UIManager.Instance.FaderMenu.TypeText("He spent weeks digging up dirt on corruption in the Governor's office", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.panels[0], 1f);
                
                UIManager.Instance.FaderMenu.TypeText("Last night, someone decided he dug too deep.", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.panels[0], 0.5f);
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.panels[1], 1f);
                
                UIManager.Instance.FaderMenu.TypeText("A passerby reported his bleeding body in the alley behind Phyllis' Bakery", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.TypeText("The crime scene was neat... a little too neat. Only a single bullet wound and casing were left behind.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.TypeText("Whoever did this wanted to muddy the waters.", 0.25f);
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.panels[1], 0.5f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.TypeText("Make me doubt myself...", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                UIManager.Instance.FaderMenu.TypeText("We have 5 suspects in custody:", 0.25f);
                
                yield return new WaitForSeconds(4f);
                
                // Introduce the characters
                UIManager.Instance.FaderMenu.typewriter.transform.localPosition = new Vector3(0f, -200f, 0f);
                // Benedict
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.portraits[0], 1f);
                UIManager.Instance.FaderMenu.TypeText("Benedict Bartholomew\nGovernor.\nThe victim was investigating him for corruption. Claims he was in a \"highly confidential meeting\"\nSomehow, there's never proof of that.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.portraits[0], 0.5f);
                
                
                // Jacques 
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.portraits[1], 1f);
                UIManager.Instance.FaderMenu.TypeText("Jacques Jackson\nExterminator.\nHis van was parked right near the murder site. Says he was working in some adjacent apartment.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.portraits[1], 0.5f);
                
                // Phyllis
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.portraits[2], 1f);
                UIManager.Instance.FaderMenu.TypeText("Phyllis Phalange:\nBaker.\nOwner of the bakery located near the crime scene. She closed early the night of the murder.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.portraits[2], 0.5f);
                
                // Charlotte
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.portraits[3], 1f);
                UIManager.Instance.FaderMenu.TypeText("Charlotte Chamois\nGunsmith.\nFinch's shooter used a gun recently purchased from her shop.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.portraits[3], 0.5f);
                
                
                // Nathaniel
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.portraits[4], 1f);
                UIManager.Instance.FaderMenu.TypeText("Nathaniel Nicholson\nProfessional Street Bum.\nHe reported the body like he was ordering pizza. Didn't even try to give an alibi.", 0.25f);
                
                yield return new WaitForSeconds(10f);
                
                UIManager.Instance.FaderMenu.FadeOutObject(UIManager.Instance.FaderMenu.portraits[4], 0.5f);
                
                UIManager.Instance.FaderMenu.TypeText("I'll find the true murderer.\nEven if this case consumes me...", 0.25f);
                
                yield return new WaitForSeconds(5f);
                
                //END
                UIManager.Instance.FadeOut(5f);
                
                yield return new WaitForSeconds(3f);
                
                ChangePhase(CasePhase.Witness);
                
                break;
            case CasePhase.Witness:
                
                insanityLevel = (5 - holdingCellParent.childCount);
                
                Shader.SetGlobalFloat("_Grunge", insanityLevel * 0.1875f);
                
                AudioManager.Instance.StopSong();

                yield return new WaitForSecondsRealtime(0.1f);
                
                if (holdingCellParent.childCount != 5)
                {
                    GameManager.Instance.ChangeGameState(GameState.Cutscene);
                
                    UIManager.Instance.FadeIn(1f);
                
                    yield return new WaitForSeconds(0.5f);
                
                    UIManager.Instance.FaderMenu.TypeText($"{holdingCellParent.childCount} Suspects Remain...", 0.25f);
                
                    yield return new WaitForSeconds(5f);
                
                    UIManager.Instance.FadeOut(5f);
                
                    yield return new WaitForSeconds(3f);
                }

                
                GameManager.Instance.ChangeGameState(GameState.Gameplay);
                
                break;
            case CasePhase.HoldingCell:
                AudioManager.Instance.PlaySong(AudioManager.Songs.WanderSong);
                break;
            case CasePhase.Interrogation:
                break;
            case CasePhase.Judgement:
                
                if (holdingCellParent.childCount == 2)
                {
                    ChangePhase(CasePhase.FinalJudgement);
                }
                AudioManager.Instance.PlaySong(AudioManager.Songs.WanderSong);
                break;
            case CasePhase.Escort:
                break;
            
            case CasePhase.FinalJudgement:
                
                GameManager.Instance.ChangeGameState(GameState.Cutscene);
                
                UIManager.Instance.FadeIn(0.1f);
                
                yield return new WaitForSeconds(0.5f);
                
                InterrogationRoomManager.Instance.MoveFinalSuspects();
                
                UIManager.Instance.FaderMenu.TypeText("Make the decision...", 0.25f);
                
                
                PlayerController.Instance.ShowGun();
                
                yield return new WaitForSeconds(5f);

                BellRandomizer.Instance.BellActive = true;
                
                BellDialogue.SetActive(true);
                AudioManager.Instance.PlaySong(AudioManager.Songs.GunpointSong);
                
                UIManager.Instance.FadeOut(5f);
                
                yield return new WaitForSeconds(3f);
                
                GameManager.Instance.ChangeGameState(GameState.Gameplay);
                
                break;
            
            case CasePhase.WinEnding:
                
                GameManager.Instance.ChangeGameState(GameState.Cutscene);
                AudioManager.Instance.StopSong();
                AudioManager.Instance.PlayEntireSound(AudioManager.Sounds.Shot);
                UIManager.Instance.HideAllMenus();
                UIManager.Instance.shootTypewriter.gameObject.SetActive(false);
                
                UIManager.Instance.FadeIn(0.1f);
                
                Destroy(ChaseKael);
                
                yield return new WaitForSecondsRealtime(3f);
                
                AudioManager.Instance.PlaySong(AudioManager.Songs.Outro);
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.papers[0], 1f);
                
                yield return new WaitForSecondsRealtime(15f);
                
                GameManager.Instance.LoadScene(GameManager.Instance.TitleScene.SceneName, GameState.Title);
                UIManager.Instance.shootTypewriter.gameObject.SetActive(false);
                
                break;
            
            case CasePhase.LoseEnding:
                
                GameManager.Instance.ChangeGameState(GameState.Cutscene);
                AudioManager.Instance.StopSong();
                AudioManager.Instance.PlayEntireSound(AudioManager.Sounds.Shot);
                UIManager.Instance.HideAllMenus();
                UIManager.Instance.shootTypewriter.gameObject.SetActive(false);
                
                UIManager.Instance.FadeIn(0.1f);
                
                Destroy(ChaseKael);
                
                yield return new WaitForSecondsRealtime(3f);
                
                AudioManager.Instance.PlaySong(AudioManager.Songs.WanderSong);
                UIManager.Instance.FaderMenu.FadeInObject(UIManager.Instance.FaderMenu.papers[1], 1f);
                
                yield return new WaitForSecondsRealtime(15f);
                
                GameManager.Instance.LoadScene(GameManager.Instance.TitleScene.SceneName, GameState.Title);
                UIManager.Instance.shootTypewriter.gameObject.SetActive(false);
                
                break;
        }
        
        
    }
}
