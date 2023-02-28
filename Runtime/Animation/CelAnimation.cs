using UnityEngine;

namespace CelAnimation
{
    [System.Serializable]
    public abstract class CelAnimation : ScriptableObject
    {
        public string guid;

        public virtual void Play(
            SpriteRenderer renderer,
            int currentCell,
            bool flip,
            GameObject go
        ) { }

        public virtual int GetCelCount()
        {
            return 0;
        }

        public int Fps
        {
            get => _fps;
            set => _fps = value;
        }

        [SerializeField]
        protected int _fps;

#if UNITY_EDITOR
        public Vector2 position;
#endif
    }
}
