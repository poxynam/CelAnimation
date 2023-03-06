using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace CelAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CelAnimator : MonoBehaviour
    {
        [SerializeField]
        private CelAnimationController _controller;

        [SerializeField]
        private CelAnimation _currentAnimation;

        [CanBeNull]
        [SerializeField]
        private CelAnimation _nextAnimation;

        private float _clock;

        [SerializeField]
        private int _fps = 12;

        private SpriteRenderer _renderer;

        private int _currentFrameCount;

        private int _currentFrame;

        public bool flip;

        [SerializeField]
        private bool _animateOnAwake;

        private bool _animate;

        [SerializeField]
        private int _animationAmount;

        public void Play(string animationName)
        {
            _currentFrame = 0;
            _nextAnimation = _controller.animations?.Find(item => item.name == animationName);
            _animate = true;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_controller != null && _controller.animations != null)
            {
                _animationAmount = _controller.animations.Count;
            }
        }
#endif

        private void Awake()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<SpriteRenderer>();
            }

            _currentAnimation ??= _controller.animations?.FirstOrDefault();

            _animationAmount = _controller.animations!.Count;

            if (_animateOnAwake)
                _animate = true;
        }

        private void Update()
        {
            if (_currentAnimation)
                _fps = _currentAnimation.Fps;

            _clock += Time.deltaTime;
            var secondsPerFrame = 1 / (float)_fps;
            while (_clock >= secondsPerFrame)
            {
                _clock -= secondsPerFrame;
                UpdateFrame();
            }
        }

        private void UpdateFrame()
        {
            if (!_animate)
                return;

            if (_currentAnimation != _nextAnimation && _nextAnimation)
            {
                _currentAnimation = _nextAnimation;
                _currentFrameCount = _currentAnimation!.GetCelCount();
            }

            if (_currentFrameCount == 0)
            {
                _currentFrameCount = _currentAnimation.GetCelCount();
            }

            _currentAnimation.Play(
                _renderer,
                _currentFrame++ % _currentFrameCount,
                flip,
                gameObject
            );
        }
    }
}
