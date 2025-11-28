using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace _Project.Scripts._Shared.Extensions
{
    public static class AnimatorExtensions
    {
#if UNITY_EDITOR
        public struct Trigger {}
        
        public static bool TryGetParametersWithTypes(this AnimatorController animatorController, 
            out Dictionary<Type, List<string>> parameters)

        {
            parameters = new Dictionary<Type, List<string>>();
            
            if (animatorController is null)
            {
                return false;
            }
            
            var parametersArray = animatorController.parameters;

            foreach (var parameter in parametersArray)
            {
                if (parameter is null) continue;
                
                var type = GetParameterType(parameter.type);
                parameters.TryAdd(type, new List<string>());
                if (!parameters.TryGetValue(type, out var typeList)) continue;
                
                typeList.Add(parameter.name);
            }
            
            return true;
        }

        public static List<string> GetParametersWithType<T>(
            this Dictionary<Type, List<string>> parameters)
        {
            return GetParametersWithType(parameters, typeof(T));
        }
        
        public static List<string> GetParametersWithType(
            this Dictionary<Type, List<string>> parameters,
            Type type)
        {
            return parameters.TryGetValue(type, out var list) ? list : new List<string>();
        }

        public static Type GetParameterType(AnimatorControllerParameterType type)
        {
            return type switch
            {
                AnimatorControllerParameterType.Float => typeof(float),
                AnimatorControllerParameterType.Int => typeof(int),
                _ => type == AnimatorControllerParameterType.Bool ? typeof(bool) : typeof(Trigger)
            };
        }
#endif
    }
}