using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class AudioUtils
    {
        public static float LinearToDecibels(float linear)
        {
            linear = Mathf.Clamp(linear, 0f, 1f);
            return Mathf.Log10(linear) * 20f;
        }

        public static float DecibelsToLinear(float decibel) {
            var value = Mathf.Pow(10f, decibel / 20f);
            return Mathf.Clamp(value, 0f, 1f);
        }
    }
}