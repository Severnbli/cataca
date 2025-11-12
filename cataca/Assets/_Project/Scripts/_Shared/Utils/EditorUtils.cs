#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace _Project.Scripts._Shared.Utils
{
    public static class EditorUtils
    {
        public static bool Confirm(string msg)
        {
            return EditorUtility.DisplayDialog("Confirm Action", msg, "Yes", "Cancel");
        }

        public static void ShowMessage(string msg)
        {
            EditorUtility.DisplayDialog("Information", msg, "Ok");
        }

        public static void DrawHandle(Vector3 at, string text, Color color)
        {
            Handles.Label(
                at,
                new GUIContent(text),
                new GUIStyle
                {
                    normal = { textColor = color },
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                });
        }
    }
}

#endif