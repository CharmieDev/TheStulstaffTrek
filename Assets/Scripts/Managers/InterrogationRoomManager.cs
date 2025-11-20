using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class InterrogationRoomManager : Singleton<InterrogationRoomManager>
{ 
    [SerializeField] private Transform interrogationSpawn;
    [field:SerializeField] public Suspect CurrentSuspect {get; private set;}

    [SerializeField] private Transform finalJudgement1, finalJudgement2;
    
    public void MoveToInterrogation(Suspect _suspect)
    {
        CurrentSuspect = _suspect;
        _suspect.transform.position = interrogationSpawn.position;
        _suspect.transform.forward = interrogationSpawn.forward;
        _suspect.transform.forward = interrogationSpawn.forward;

    }

    public void StartInterrogation()
    {
        if (CurrentSuspect == null) return;
        
        AudioManager.Instance.PlaySong(AudioManager.Songs.InterrogationSong);
        DialogueManager.Instance.OnDialogueEnd.AddListener(EndInterrogation);
        CurrentSuspect.StartInterrogation();
    }

    public void EndInterrogation()
    {
        AudioManager.Instance.StopSong();
        DialogueManager.Instance.OnDialogueEnd.RemoveListener(EndInterrogation);
        GameManager.Instance.ChangeGameState(GameState.Gameplay);
        CaseManager.Instance.ChangePhase(CasePhase.Judgement);
        CurrentSuspect.hasInterrogated = true;

        if (CaseManager.Instance.holdingCellParent.childCount != 2)
        {
            StartCoroutine(FadeOutTeleport());
        }
        else
        {
            CurrentSuspect.transform.position = CurrentSuspect.HoldingCellSpawn.position;
            CurrentSuspect.transform.forward = CurrentSuspect.HoldingCellSpawn.forward;
            CurrentSuspect = null;
        }
        
        
    }

    public IEnumerator FadeOutTeleport()
    {
        UIManager.Instance.FadeIn(1f);
        yield return new WaitForSecondsRealtime(1f);
        CurrentSuspect.transform.position = CurrentSuspect.HoldingCellSpawn.position;
        CurrentSuspect.transform.forward = CurrentSuspect.HoldingCellSpawn.forward;
        CurrentSuspect = null;
        UIManager.Instance.FadeOut(1f);
    }

    public void MoveFinalSuspects()
    {
        CaseManager.Instance.holdingCellParent.transform.GetChild(0).GetComponentInChildren<Suspect>().transform.position = finalJudgement1.position;
        CaseManager.Instance.holdingCellParent.transform.GetChild(1).GetComponentInChildren<Suspect>().transform.position = finalJudgement2.position;
    }
}
