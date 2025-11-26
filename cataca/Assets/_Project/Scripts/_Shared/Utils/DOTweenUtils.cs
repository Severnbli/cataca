using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class DOTweenUtils
    {
        public static float GetTimeToChange(Vector3 current, Vector3 target, float speed)
        {
            var distance = Vector3.Distance(current, target);
            return distance / speed;
        }

        public static float GetTimeToChange(Quaternion current, Quaternion target, float speed)
        {
            var distance = Quaternion.Angle(current, target);
            return distance / speed;
        }
    }
}