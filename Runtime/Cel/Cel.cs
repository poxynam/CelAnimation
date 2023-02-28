using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace CelAnimation
{
    [System.Serializable]
    public class Cel : ICel
    {
        public Cel() { }

        public Cel(Sprite sprite = null)
        {
            if (sprite != null)
                _sprite = sprite;
        }

        [SerializeField]
        protected Sprite _sprite;

        [SerializeField]
        private CelAnimationEvent _event;

        public virtual void ApplyToRenderer(SpriteRenderer renderer, bool flip, GameObject go)
        {
            renderer.flipX = flip;
            renderer.sprite = _sprite;
            _event.Invoke(go);
        }
    }
}
