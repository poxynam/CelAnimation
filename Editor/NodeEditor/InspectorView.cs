using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, InspectorView.UxmlTraits> { }

    private Editor _editor;

    private IMGUIContainer container;

    public InspectorView() { }

    public void UpdateSelection(NodeView node)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(_editor);
        _editor = Editor.CreateEditor(node.animation);
        container = new IMGUIContainer(() =>
        {
            _editor.OnInspectorGUI();
        });

        Add(container);
    }

    public void Unselect(NodeView node)
    {
        Clear();
    }
}
