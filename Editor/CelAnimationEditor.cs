using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace CelAnimation
{
    [CustomEditor(typeof(BasicCelAnimation))]
    public class CelAnimationEditor : Editor
    {
        private readonly List<SerializedProperty> _propertiesToDraw =
            new List<SerializedProperty>();

        protected static float FPS = 10;
        protected static int CurrentFrame;
        protected static bool ShouldPlay = true;
        protected readonly List<Sprite> Sprites = new List<Sprite>();
        protected SerializedProperty Fps;
        protected SerializedProperty Cels;

        protected virtual void OnEnable()
        {
            Fps = AddCustomProperty("_fps");
            Cels = AddCustomProperty("_cels");
        }

        public override bool HasPreviewGUI()
        {
            return true;
        }

        protected SerializedProperty AddCustomProperty(string propertyName)
        {
            var property = serializedObject.FindProperty(propertyName);
            _propertiesToDraw.Add(property);
            return property;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawCustomProperties();
            EditorGUILayout.Separator();
            UpdateSpritesCache();

            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawCustomProperties()
        {
            foreach (var property in _propertiesToDraw)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(property);
            }
        }

        protected virtual void UpdateSpritesCache()
        {
            Sprites.Clear();
            for (var i = 0; i < Cels.arraySize; i++)
            {
                var frameProp = Cels.GetArrayElementAtIndex(i);
                var sprite =
                    frameProp.FindPropertyRelative("_sprite").objectReferenceValue as Sprite;
                if (sprite != null)
                    Sprites.Add(sprite);
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

            Helpers.DrawTexturePreview(position, Sprites[index]);
        }

        protected void DrawPlaybackControls()
        {
            EditorGUILayout.BeginHorizontal();
            ShouldPlay = EditorGUILayout.ToggleLeft("Play", ShouldPlay, GUILayout.MaxWidth(100));
            if (ShouldPlay)
                FPS = EditorGUILayout.FloatField("Frames per seconds", FPS);
            else
                CurrentFrame = EditorGUILayout.IntSlider(CurrentFrame, 0, Sprites.Count - 1);
            EditorGUILayout.EndHorizontal();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        [MenuItem("Assets/Create/Cel Animation/Basic Cel Animation (From Textures)", false, 400)]
        private static void CreateFromTextures()
        {
            var trailingNumbersRegex = new Regex(@"(\d+$)");

            var sprites = new List<Sprite>();
            var textures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
            foreach (var texture in textures)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                sprites.AddRange(AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>());
            }

            var cels = sprites
                .OrderBy(sprite =>
                {
                    var match = trailingNumbersRegex.Match(sprite.name);
                    return match.Success ? int.Parse(match.Groups[0].Captures[0].ToString()) : 0;
                })
                .Select(sprite => new Cel(sprite))
                .ToArray();

            var asset = BasicCelAnimation.Create<BasicCelAnimation>(cels: cels);
            string baseName = trailingNumbersRegex.Replace(textures[0].name, "");
            asset.name = baseName + "";

            string assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(textures[0]));
            AssetDatabase.CreateAsset(
                asset,
                Path.Combine(assetPath ?? Application.dataPath, asset.name + ".asset")
            );
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Assets/Create/Cel Animation/Basic Cel Animation (From Textures)", true, 400)]
        private static bool CreateFromTexturesValidation()
        {
            return Selection.GetFiltered<Texture2D>(SelectionMode.Assets).Length > 0;
        }
    }
}
