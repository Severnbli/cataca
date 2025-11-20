using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SpriteGlow;
using UnityEngine;

namespace _Project.Scripts._Shared.Extensions
{
    public static class SpriteGlowExtensions
    {
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this SpriteGlowEffect target, Color endValue,
            float duration)
        {
            var t = DOTween.To(() => target.GlowColor, x => target.GlowColor = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        public static TweenerCore<float, float, FloatOptions> DOBrightness(this SpriteGlowEffect target, float endValue,
            float duration)
        {
            var t = DOTween.To(() => target.GlowBrightness, x => target.GlowBrightness = x, endValue,  duration);
            t.SetTarget(target);
            return t;
        }
    }
}