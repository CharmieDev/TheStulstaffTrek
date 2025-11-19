using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Singleton<PlayerController>
{
    
    [field: Header("State Machine")]
    public StateMachine<PlayerController> StateMachine { get; private set; }
    [field: SerializeField] public PlayerMove MoveState { get; private set; } = new();
    [field: SerializeField] public PlayerIdle IdleState { get; private set; } = new();

    [field:Header("Components")] 
    [field: SerializeField] public MMF_Player Footsteps { get; private set; }
    [field:SerializeField] public Transform Orientation { get; private set; }
    [field:SerializeField] public UnityEngine.GameObject Gun { get; private set; }
    private GameObject aimedAtGameObject;
    public Animator Animator { get; private set; }
    public InputManager InputManager { get; private set; }   
    public Rigidbody Rb {get; private set;}

    protected override void Awake()
    {
        base.Awake();
        Rb = GetComponent<Rigidbody>();
        SetupStateMachine();
        StateMachine.SetState(IdleState);
    }

    private void Start()
    {
        InputManager = InputManager.Instance;
    }

    private void Update()
    {
        StateMachine.Update();
        if (StateMachine.CurrentState == IdleState && InputManager.MoveVector != Vector2.zero)
        {
            StateMachine.SetState(MoveState);
        }
        else if (StateMachine.CurrentState == MoveState && InputManager.MoveVector == Vector2.zero)
        {
            StateMachine.SetState(IdleState);
        }

        if (aimedAtGameObject != null && Input.GetMouseButtonDown(0))
        {
            if(aimedAtGameObject.TryGetComponent(out DrKChase drKChase))
            {
                CaseManager.Instance.ChangePhase(CasePhase.WinEnding);
            }
            else
            {
                CaseManager.Instance.ChangePhase(CasePhase.LoseEnding);
            }
            
        }
        
        
    }
    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();

        if (Gun.activeInHierarchy)
        {
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity);
            {
                if (hit.transform.TryGetComponent(out Suspect suspect))
                {
                    if (suspect.gameObject == aimedAtGameObject) return;
                    
                    aimedAtGameObject = suspect.gameObject;
                    UIManager.Instance.shootTypewriter.gameObject.SetActive(true);
                    UIManager.Instance.DisplayShootText("Shoot " + suspect.SuspectName);
                }
                else if (hit.transform.TryGetComponent(out DrKChase drkChase))
                {
                    if (drkChase.gameObject == aimedAtGameObject) return;
                    
                    aimedAtGameObject = drkChase.gameObject;
                    UIManager.Instance.shootTypewriter.gameObject.SetActive(true);
                    UIManager.Instance.DisplayShootText("Shoot Dr. Kael");
                }
                else
                {
                    UIManager.Instance.shootTypewriter.gameObject.SetActive(false);
                    aimedAtGameObject = null;
                }
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && InputManager.InteractHeld)
        {
            interactable.Interact();
        }
    }

    private void SetupStateMachine()
    {
        StateMachine = new StateMachine<PlayerController>(this);
        MoveState.Init(this);
        IdleState.Init(this);
    }

    public void PlayFootstep()
    {
        Footsteps?.PlayFeedbacks();
    }

    public void ShowGun()
    {
        Gun.SetActive(true);
    }
    public virtual void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        if (Application.isPlaying && StateMachine != null)
        {
            List<State<PlayerController>> states = StateMachine.GetActiveStateBranch();
    
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
