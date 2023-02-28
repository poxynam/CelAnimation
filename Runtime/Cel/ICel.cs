using UnityEngine;

namespace CelAnimation
{
    public interface ICel
    {
        void ApplyToRenderer(SpriteRenderer renderer, bool flip, GameObject go);
    }
}
