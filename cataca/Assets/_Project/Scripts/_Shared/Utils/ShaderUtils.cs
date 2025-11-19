using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts._Shared.Utils
{
    public static class ShaderUtils
    {
        public static List<string> GetPropertiesNames(Shader shader)
        {
            var count = shader.GetPropertyCount();
            var properties = new List<string>(count);
            
            for (var i = 0; i < count; i++)
            {
                properties.Add(shader.GetPropertyName(i));
            }
            
            return properties;
        }

        public static List<ShaderPropertyType> GetPropertiesTypes(Shader shader)
        {
            var count = shader.GetPropertyCount();
            var properties = new List<ShaderPropertyType>(count);

            for (var i = 0; i < count; i++)
            {
                properties.Add(shader.GetPropertyType(i));
            }
            
            return properties;
        }

        public static Dictionary<ShaderPropertyType, List<string>> GetPropertiesNamesTypes(Shader shader)
        {
            var count = shader.GetPropertyCount();
            var properties = new Dictionary<ShaderPropertyType, List<string>>(count);

            for (var i = 0; i < count; i++)
            {
                var propertyType = shader.GetPropertyType(i);
                if (!properties.TryGetValue(propertyType, out var names))
                {
                    names = new List<string>();
                    if (!properties.TryAdd(propertyType, names)) continue;
                }
                
                names.Add(shader.GetPropertyName(i));
            }
            
            return properties;
        }

        public static List<string> GetPropertiesNamesByType(Dictionary<ShaderPropertyType, List<string>> properties,
            ShaderPropertyType type)
        {
            if (!properties.TryGetValue(type, out var names))
            {
                names = new List<string>();
            }
            
            return names;
        }
    }
}