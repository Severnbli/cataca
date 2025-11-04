#if UNITY_EDITOR

namespace _Project.Scripts._Shared.Utils
{
    public static class EditorUtils
    {
        public static bool Confirm(string msg)
        {
            return UnityEditor.EditorUtility.DisplayDialog("Confirm Action", msg, "Yes", "Cancel");
        }
    }
}

#endif