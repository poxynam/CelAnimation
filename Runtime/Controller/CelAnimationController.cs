using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CelAnimation
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "New Cel Animation Controller",
        menuName = "Cel Animation/Controller",
        order = 400
    )]
    public class CelAnimationController : ScriptableObject
    {
        [SerializeField]
        public List<CelAnimation> animations = new List<CelAnimation>();

#if UNITY_EDITOR
        public void DeleteAnimation(CelAnimation celAnimation)
        {
            animations.Remove(celAnimation);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
