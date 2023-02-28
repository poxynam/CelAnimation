using CelAnimation;
using UnityEditor;

[CustomEditor(typeof(CelAnimator))]
public class CelAnimatorEditor : Editor
{
    private SerializedProperty _controllerProp;

    private SerializedProperty _current;

    private SerializedProperty _animateOnAwake;

    private SerializedProperty _fps;

    private SerializedProperty _animationAmount;

    public override void OnInspectorGUI()
    {
        _controllerProp = serializedObject.FindProperty("_controller");
        _current = serializedObject.FindProperty("_currentAnimation");
        _animateOnAwake = serializedObject.FindProperty("_animateOnAwake");
        _fps = serializedObject.FindProperty("_fps");
        _animationAmount = serializedObject.FindProperty("_animationAmount");

        EditorGUILayout.PropertyField(_controllerProp);

        EditorGUILayout.LabelField(
            "Current Animation:",
            _current.objectReferenceValue != null ? _current.objectReferenceValue.name : "Null"
        );

        EditorGUILayout.PropertyField(_animateOnAwake);

        EditorGUILayout.HelpBox(
            "Animation Count: " + _animationAmount.intValue + " FPS: " + _fps.intValue,
            MessageType.Info
        );

        serializedObject.ApplyModifiedProperties();
    }
}
