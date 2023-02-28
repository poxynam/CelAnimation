using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CelAnimation
{
    [CustomPropertyDrawer(typeof(MirroredCel))]
    public class MirroredCelPropertyDrawer : CelPropertyDrawer
    {
        protected override IEnumerable<string> PropertiesToDraw =>
            new string[] { "_sprite", "_spriteLeft", "_event" };

        protected override Rect DrawPreview(Rect position, SerializedProperty property)
        {
            position = DrawSpritePreview(position, property, "_sprite");
            position = DrawSpritePreview(position, property, "_spriteLeft");

            return position;
        }
    }
}
