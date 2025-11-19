using System;
using Febucci.UI.Core;
using UnityEngine;
using Unity.GraphToolkit.Editor;


[Serializable]
public class StartNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort("out").Build();
    }
}

[Serializable]
public class EndNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
    }
}

[Serializable]
public class FlagNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
        
        context.AddInputPort<string>("Key").Build();
        context.AddInputPort<bool>("Value").Build();
        
        context.AddOutputPort("out").Build();
    }
}


[Serializable]
public class DialogueNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
        context.AddOutputPort("out").Build();
        
        context.AddInputPort<string>("Speaker").Build();
        context.AddInputPort<string>("Dialogue").Build();
        
        context.AddInputPort<Faces>("Face").Build();
        
        context.AddInputPort<Sprite>("SpeakerSprite").Build();

    }
}

public class ChoiceNode : Node
{
    private const string optionID = "portCount";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();

        context.AddInputPort<string>("Speaker").Build();
        context.AddInputPort<string>("Dialogue").Build();
        
        context.AddInputPort<Faces>("Face").Build();
        
        context.AddInputPort<Sprite>("SpeakerSprite").Build();

        var option = GetNodeOptionByName(optionID);
        option.TryGetValue(out int portCount);
        for (int i = 0; i < portCount; i++)
        {
            context.AddInputPort<string>($"Choice Text {i}").Build();
            context.AddOutputPort($"Choice {i}").Build();
        }
    }

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        context.AddOption<int>(optionID).Delayed().WithDefaultValue(2);
    }
}
