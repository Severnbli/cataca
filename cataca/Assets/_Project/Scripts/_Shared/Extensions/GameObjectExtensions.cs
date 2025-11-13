using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts._Shared.Extensions
{
    public static class GameObjectExtensions
    {
        public static List<T> GetChildComponents<T>(this GameObject gameObject) where T : Component
        {
            var ownComponent = gameObject.GetComponent<T>();
            var components = gameObject != null ? 
                gameObject.GetComponentsInChildren<T>()
                    .Where(x => x != ownComponent)
                    .ToList()
                : new List<T>();
            
            return components;
        }
    }
}