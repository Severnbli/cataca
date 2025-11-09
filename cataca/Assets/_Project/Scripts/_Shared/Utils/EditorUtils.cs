#if UNITY_EDITOR

namespace _Project.Scripts._Shared.Utils
{
    public static class EditorUtils
    {
        public static bool Confirm(string msg)
        {
            return UnityEditor.EditorUtility.DisplayDialog("Confirm Action", msg, "Yes", "Cancel");
        }

        public static void ShowMessage(string msg)
        {
            UnityEditor.EditorUtility.DisplayDialog("Information", msg, "Ok");
        }
    }
}

#endif