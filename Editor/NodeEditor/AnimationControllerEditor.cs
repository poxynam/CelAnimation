using System;
using CelAnimation;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Metroidvania.CelAnimation
{
    public class AnimationControllerEditor : EditorWindow
    {
        private ControllerEditorView _editorView;
        private InspectorView _inspectorView;

        // [SerializeField]
        // private VisualTreeAsset _visualTreeAsset = default;

        [MenuItem("Cel Animation/Controller Editor")]
        public static void OpenWindow()
        {
            AnimationControllerEditor wnd = GetWindow<AnimationControllerEditor>();
            wnd.titleContent = new GUIContent("Controller Editor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is not CelAnimationController)
                return false;

            OpenWindow();
            return true;
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.poxynam.celanimation/Editor/NodeEditor/AnimationControllerEditor.uxml"
            );

            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/com.poxynam.celanimation/Editor/NodeEditor/AnimationControllerEditor.uss"
            );
            root.styleSheets.Add(styleSheet);

            _editorView = root.Q<ControllerEditorView>();
            _inspectorView = root.Q<InspectorView>();
            _editorView.OnNodeSelected = OnNodeSelectionChanged;
            _editorView.OnNodeDeselected = OnUnselected;

            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            var anim = Selection.activeObject as CelAnimationController;

            if (anim && AssetDatabase.CanOpenAssetInEditor(anim.GetInstanceID()))
            {
                _editorView.PopulateView(anim);
            }
        }

        private void OnUnselected(NodeView node)
        {
            _inspectorView.Unselect(node);
        }

        private void OnNodeSelectionChanged(NodeView node)
        {
            _inspectorView.UpdateSelection(node);
        }
    }
}
