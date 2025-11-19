using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Witness : MonoBehaviour
{
    [field: Header("State Machine")]
    public StateMachine<Witness> StateMachine { get; protected set; }
    
  
    [field: SerializeField] public WitnessEnter EnterState { get; private set; } = new();
    [field: SerializeField] public WitnessExit ExitState { get; private set; } = new();
    [field: SerializeField] public WitnessTalking TalkingState { get; private set; } = new();
    [field: SerializeField] public WitnessIdle IdleState { get; private set; } = new();

    [field:SerializeField] public RuntimeDialogueGraph DialogueGraph { get; private set; }
    public Rigidbody Rb { get; private set; }

    [field:SerializeField] public Transform LookAt { get; private set; }
    
    [field: SerializeField] public string SuspectName { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    [field:SerializeField] public MMF_Player Footsteps { get; private set; }
    
    [field: SerializeField] public UnityEngine.GameObject InteractObject { get; private set; }

    
    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        InteractObject.SetActive(false);
    }
    protected void Start()
    {
        SetupStateMachine();
        StateMachine.SetState(EnterState);
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
        if (StateMachine.CurrentState == EnterState && StateMachine.CurrentState.IsComplete)
        {
            StateMachine.SetState(IdleState);
        }
        else if (StateMachine.CurrentState == IdleState && StateMachine.CurrentState.IsComplete)
        {
            StateMachine.SetState(TalkingState);
        }
        else if (StateMachine.CurrentState == TalkingState && StateMachine.CurrentState.IsComplete)
        {
            StateMachine.SetState(ExitState);
        }
        else if (StateMachine.CurrentState == ExitState && StateMachine.CurrentState.IsComplete)
        {
            WitnessManager.Instance.DestroyWitness();
        }
    }

    protected virtual void SetupStateMachine()
    {
        StateMachine = new StateMachine<Witness>(this);
        EnterState.Init(this);
        ExitState.Init(this);
        TalkingState.Init(this);
        IdleState.Init(this);
    }
    
    public virtual void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        if (Application.isPlaying && StateMachine != null)
        {
            List<State<Witness>> states = StateMachine.GetActiveStateBranch();
    
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
