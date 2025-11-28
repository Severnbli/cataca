using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class AudioUtils
    {
        public static float LinearToDecibels(float linear, float min = 0.0001f, float max = 1f)
        {
            linear = Mathf.Clamp(linear, min, max);
            return Mathf.Log10(linear) * 20f;
        }

        public static float DecibelsToLinear(float decibel, float min = 0.0001f, float max = 1f) {
            var value = Mathf.Pow(10f, decibel / 20f);
            return Mathf.Clamp(value, min, max);
        }
    }
}