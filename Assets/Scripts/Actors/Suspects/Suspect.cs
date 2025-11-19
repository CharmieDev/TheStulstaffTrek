using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Suspect : MonoBehaviour
{
    [field: Header("State Machine")]
    public StateMachine<Suspect> StateMachine { get; protected set; }
    
    [field: SerializeField] public SuspectTalking TalkingState { get; private set; } = new();
    [field: SerializeField] public SuspectIdle IdleState { get; private set; } = new();
    [field: SerializeField] public SuspectInterrogate MoveInterrogateState { get; private set; } = new();
    [field: SerializeField] public SuspectRelease ReleaseState { get; private set; } = new();
    [field: SerializeField] public SuspectRelease GoAwayState { get; private set; } = new();
    [field: SerializeField] public SuspectFollow FollowState { get; private set; } = new();

    [field: SerializeField] public string SuspectName { get; private set; }
    
    [field:SerializeField] public RuntimeDialogueGraph HoldingCellDialogueGraph { get; private set; }
    [field:SerializeField] public RuntimeDialogueGraph JudgementDialogue { get; private set; }
    [field:SerializeField] public RuntimeDialogueGraph InterrogationDialogue { get; private set; }
    public Rigidbody Rb { get; private set; }

    [field:SerializeField] public Transform HoldingCellSpawn { get; private set; }

    [field:SerializeField] public Transform LookAt { get; private set; }
    
    [field:SerializeField] public NavMeshAgent agent { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public bool hasInterrogated;
    [field:SerializeField] public TextMeshProUGUI interactPromptText { get; private set; }

    
    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    protected void Start()
    {
        SetupStateMachine();
        StateMachine.SetState(IdleState);
        interactPromptText.transform.parent.gameObject.SetActive(false);
    }

    protected void Update()
    {
        StateMachine.Update();
        HandleTransitions();
    }
    
    protected virtual void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    protected virtual void HandleTransitions()
    {
        if (StateMachine.CurrentState == TalkingState && StateMachine.CurrentState.IsComplete)
        {
            // Interrogate Suspect
            if (DialogueManager.Instance.CurrentFlags.TryGetValue("Interrogate", out bool value) && value)
            {
                CaseManager.Instance.ChangePhase(CasePhase.Interrogation);
                StateMachine.SetState(MoveInterrogateState);
            }

            else if (DialogueManager.Instance.CurrentFlags.TryGetValue("Judgement", out value) && value)
            {
                CaseManager.Instance.ChangePhase(CasePhase.Escort);
                StateMachine.SetState(ReleaseState);
            }
            
            // Player chose to not interrogate
            else
            {
                StateMachine.SetState(IdleState);
            }

            return;
        }

        if (StateMachine.CurrentState == MoveInterrogateState && StateMachine.CurrentState.IsComplete)
        {
            StateMachine.SetState(IdleState);
            return;
        }
        if (StateMachine.CurrentState == ReleaseState && StateMachine.CurrentState.IsComplete)
        {
            StateMachine.SetState(FollowState);
            return;
        }

        if (StateMachine.CurrentState == GoAwayState && StateMachine.CurrentState.IsComplete)
        {
            WitnessManager.Instance.StartNextWitnessPhase();
            Destroy(transform.parent.gameObject);
        }
    }

    protected virtual void SetupStateMachine()
    {
        StateMachine = new StateMachine<Suspect>(this);
        TalkingState.Init(this);
        IdleState.Init(this);
        MoveInterrogateState.Init(this);
        ReleaseState.Init(this);
        FollowState.Init(this);
        GoAwayState.Init(this);
    }

    public void TalkInHoldingCell()
    {
        StateMachine.SetState(TalkingState);
        DialogueManager.Instance.StartDialogue(HoldingCellDialogueGraph);
    }

    public void StartInterrogation()
    {
        StateMachine.SetState(TalkingState);
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        DialogueManager.Instance.StartDialogue(InterrogationDialogue);
    }

    public void JudgementTalk()
    {
        StateMachine.SetState(TalkingState);
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        DialogueManager.Instance.StartDialogue(JudgementDialogue);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoAway"))
        {
            StateMachine.SetState(GoAwayState);
        }
    }

    public virtual void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        if (Application.isPlaying && StateMachine != null)
        {
            List<State<Suspect>> states = StateMachine.GetActiveStateBranch();
    
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            if(StateMachine.CurrentState.IsComplete)
            {
                style.normal.textColor = Color.green;
            }
            else
            {
                style.normal.textColor = Color.red;
            }
            style.fontSize = 40;
            UnityEditor.Handles.Label(transform.position + Vector3.up, "Active States: " + string.Join(" > ", states), style);
        
        }
#endif
    }
}
