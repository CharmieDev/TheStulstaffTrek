using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogueManager : Singleton<DialogueManager>
{
    private RuntimeDialogueGraph _runtimeGraph;

    [field:Header("UI Components")]
    [field:SerializeField] public UnityEngine.GameObject DialoguePanel {get; private set;}

    [field:SerializeField] public TextMeshProUGUI SpeakerNameText {get; private set;}
    
    [field:SerializeField] public TextMeshProUGUI DialogueText {get; private set;}
    [field: SerializeField] public Image SpeakerSprite {get; private set;}
    
    [field:Header("Text Animator")]
    [field:SerializeField] public TypewriterByCharacter Typewriter {get; private set;}
    [field:SerializeField] public TextAnimator_TMP TextAnimator {get; private set;}

    [Header("Choice Buttons UI")] 
    [field:SerializeField] public Button ChoiceButtonPrefab {get; private set;}
    [field:SerializeField] public Transform ChoiceButtonContainer {get; private set;}
    
    [Header("Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;
    public UnityEvent OnFaceChanged;
    
    
    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    private RuntimeDialogueNode _currentNode;
    
    [field:SerializeField] public Dictionary<string, bool> CurrentFlags { get; private set; }

    private void Start()
    {
        
    }


    public void StartDialogue(RuntimeDialogueGraph runtimeGraph)
    {
        _runtimeGraph = runtimeGraph;
        CurrentFlags = new Dictionary<string, bool>();
        StartDialogue();
    }

    
    private void StartDialogue()
    {
        OnDialogueStart?.Invoke();
        
        foreach (var node in _runtimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }

        if (!string.IsNullOrEmpty(_runtimeGraph.EntryNodeID))
        {
            Debug.Log("starting dialogue");
            ShowNode(_runtimeGraph.EntryNodeID);
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Typewriter.isShowingText)
        {
            Typewriter.SkipTypewriter();
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && _currentNode != null && _currentNode.Choices.Count == 0)
        {
            if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                ShowNode(_currentNode.NextNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
    }


    private void ShowNode(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            EndDialogue();
            return;
        }
        
        Debug.Log("Showing Next Node");
        
        _currentNode = _nodeLookup[nodeID];
        
        Debug.Log("FlagKey" + _currentNode.FlagKey);
        
        // Flag Node Processing
        if (!string.IsNullOrEmpty(_currentNode.FlagKey))
        {
            CurrentFlags.Add(_currentNode.FlagKey, _currentNode.FlagValue);
            if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                ShowNode(_currentNode.NextNodeID);
            }
            else
            {
                EndDialogue();
            }
            return;
        }
        
        // Only show speaker sprite if we have one
        if (SpeakerSprite != null && _currentNode.SpeakerSprite != null)
        {
            SpeakerSprite.gameObject.SetActive(true);
            SpeakerSprite.sprite = _currentNode.SpeakerSprite;
        }
        else
        {
            SpeakerSprite.gameObject.SetActive(false);
        }

        OnFaceChanged?.Invoke();
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        Typewriter.ShowText(_currentNode.DialogueText);
        DialoguePanel.SetActive(true);
        Typewriter.StartShowingText(true);


        


        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        if (_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
            {
                Button button = Instantiate(ChoiceButtonPrefab, ChoiceButtonContainer);
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choice.ChoiceText;
                }

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.DestinationNodeID))
                        {
                            ShowNode(choice.DestinationNodeID);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    });
                }
            }
        }
    }

    private void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        _currentNode = null;
        
        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
        
        OnDialogueEnd?.Invoke();
    }

}