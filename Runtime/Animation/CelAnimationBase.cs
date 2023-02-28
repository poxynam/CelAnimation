using System.Linq;
using UnityEngine;

namespace CelAnimation
{
    public abstract class CelAnimationBase<TCel> : CelAnimation where TCel : ICel
    {
        public static TNode Create<TNode>(TCel[] cels = null) where TNode : CelAnimationBase<TCel>
        {
            var instance = CreateInstance<TNode>();

            if (cels != null)
                instance._cels = cels;

            return instance;
        }

        [SerializeField]
        protected TCel[] _cels;

        public override void Play(
            SpriteRenderer renderer,
            int currentCell,
            bool flip,
            GameObject go
        )
        {
            _cels[currentCell].ApplyToRenderer(renderer, flip, go);
        }

        public override int GetCelCount()
        {
            return _cels.Length;
        }
    }
}
