using UnityEditor;
using UnityEngine;

namespace CelAnimation
{
    [CustomPropertyDrawer(typeof(CelAnimationEvent))]
    public class CelAnimationEventPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var send = property.FindPropertyRelative("_sendEvent");
            var name = property.FindPropertyRelative("_eventName");

            var rect = new Rect(position.x, position.y + 20, position.width, position.height);

            EditorGUI.PropertyField(position, send);

            if (send.boolValue)
            {
                EditorGUI.PropertyField(rect, name);
            }

            EditorGUI.EndProperty();
        }
    }
}
