using UnityEngine;

public class ExampleDialogueTrigger : MonoBehaviour
{
    public RuntimeDialogueGraph graph;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(graph);
    
    
    }



}
