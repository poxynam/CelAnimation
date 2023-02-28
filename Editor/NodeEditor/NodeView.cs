using System;
using CelAnimation;
using Metroidvania.CelAnimation;
using UnityEngine;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public CelAnimation.CelAnimation animation;
    public Action<NodeView> OnNodeSelected;
    public Action<NodeView> OnNodeDeselected;

    public NodeView(CelAnimation.CelAnimation celAnimation)
        : base("Packages/com.poxynam.celanimation/Editor/NodeEditor/NodeView.uxml")
    {
        this.animation = celAnimation;
        this.title = celAnimation.name;
        this.viewDataKey = celAnimation.guid;
        style.left = celAnimation.position.x;
        style.top = celAnimation.position.y;

        SetupClasses();
    }

    private void SetupClasses()
    {
        switch (animation)
        {
            case BasicCelAnimation:
                AddToClassList("basic");
                break;
            case MirroredCelAnimation:
                AddToClassList("mirrored");
                break;
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(animation, "(Animation Editor) Set Position");

        animation.position.x = newPos.xMin;
        animation.position.y = newPos.yMin;
        EditorUtility.SetDirty(animation);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        OnNodeDeselected?.Invoke(this);
    }
}
