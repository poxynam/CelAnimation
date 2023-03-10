using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CelAnimation
{
    [CustomEditor(typeof(MirroredCelAnimation))]
    public class MirroredCelAnimationEditor : CelAnimationEditor
    {
        protected readonly List<Sprite> SpritesLeft = new List<Sprite>();

        protected override void UpdateSpritesCache()
        {
            Sprites.Clear();
            SpritesLeft.Clear();
            for (var i = 0; i < Cels.arraySize; i++)
            {
                var frameProp = Cels.GetArrayElementAtIndex(i);
                var sprite =
                    frameProp.FindPropertyRelative("_sprite").objectReferenceValue as Sprite;
                if (sprite != null)
                    Sprites.Add(sprite);
                var spriteLeft =
                    frameProp.FindPropertyRelative("_spriteLeft").objectReferenceValue as Sprite;
                if (spriteLeft != null)
                    SpritesLeft.Add(spriteLeft);
            }
        }

        public override void OnPreviewGUI(Rect position, GUIStyle background)
        {
            DrawPlaybackControls();

            if (Sprites.Count == 0)
                return;
            int index = ShouldPlay
                ? (int)(EditorApplication.timeSinceStartup * FPS % Sprites.Count)
                : CurrentFrame;

            position.width /= 2;
            if (index < SpritesLeft.Count)
                Helpers.DrawTexturePreview(position, SpritesLeft[index]);
            position.x += position.width;
            Helpers.DrawTexturePreview(position, Sprites[index]);
        }
    }
}
