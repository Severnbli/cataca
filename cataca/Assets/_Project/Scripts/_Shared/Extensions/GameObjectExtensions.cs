using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts._Shared.Extensions
{
    public static class GameObjectExtensions
    {
        public static List<T> GetChildComponents<T>(this GameObject gameObject) where T : Component
        {
            
            var components = gameObject != null 
                ? gameObject.GetComponentsInChildren<T>().ToList()
                : new List<T>();

            if (!components.Any()) return components;
            
            var ownComponent = gameObject.GetComponent<T>();
            components = components
                .Where(x => x != ownComponent)
                .ToList();

            return components;
        }
    }
}