using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace CelAnimation
{
    [System.Serializable]
    public class MirroredCel : ICel
    {
        [SerializeField]
        private Sprite _spriteLeft;

        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private CelAnimationEvent _event;

        public void ApplyToRenderer(SpriteRenderer renderer, bool flip, GameObject go)
        {
            renderer.sprite = !flip ? _sprite : _spriteLeft;
            renderer.flipX = false;
            _event.Invoke(go);
        }
    }
}
