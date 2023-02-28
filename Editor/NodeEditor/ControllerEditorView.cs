using System;
using CelAnimation;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class ControllerEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<ControllerEditorView, UxmlTraits> { }

    public Action<NodeView> OnNodeSelected;
    public Action<NodeView> OnNodeDeselected;

    public DragEnterEvent DragEnterEvent;
    public DragPerformEvent DragPerformEvent;

    public CelAnimationController anim;

    public ControllerEditorView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
            "Packages/com.poxynam.celanimation/Editor/NodeEditor/AnimationControllerEditor.uss"
        );
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(anim);
        AssetDatabase.SaveAssets();
    }

    internal void PopulateView(CelAnimationController anim)
    {
        this.anim = anim;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        anim.animations.ForEach(CreateNodeView);
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        graphviewchange.elementsToRemove?.ForEach(elem =>
        {
            if (elem is NodeView nodeView)
            {
                anim.DeleteAnimation(nodeView.animation);
            }
        });
        return graphviewchange;
    }

    private void CreateNodeView(CelAnimation.CelAnimation celAnimation)
    {
        var nodeView = new NodeView(celAnimation)
        {
            OnNodeSelected = OnNodeSelected,
            OnNodeDeselected = OnNodeDeselected
        };
        AddElement(nodeView);
    }
}
