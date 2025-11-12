#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void Validate(HashSet<KeyValuePair<Func<bool>, string>> checks, string name)
        {
            var output = new List<string>(checks.Count);
            
            foreach (var check in checks)
            {
                if (!check.Key.Invoke()) output.Add(check.Value);
            }

            if (!output.Any())
            {
                Debug.Log($"Validate {name} succeeded.");
                return;
            }
            
            var finalOutput = output
                .Select((txt, i) => $"{i + 1}) {txt}")
                .ToList();
            Debug.LogWarning($"Validation {name} failed: {string.Join(";\n", finalOutput)}");
        }
    }
}

#endif