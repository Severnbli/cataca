using System;
using Leopotam.EcsLite;

namespace _Project.Scripts._Shared.Extensions
{
    public static class EcsExtensions
    {
        public static bool TryGetFirst(this EcsFilter filter, out int entity)
        {
            entity = -1;
            if (filter.GetEntitiesCount() == 0) return false;

            entity = filter.GetRawEntities()[0];
            return true;
        }

        public static void AddOrDelComponentOnCondition<T>(this EcsPool<T> pool, int entity, Func<bool> condition)
            where T : struct
        {
            if (condition.Invoke())
            {
                if (!pool.Has(entity))
                {
                    pool.Add(entity);
                }
            }
            else
            {
                if (pool.Has(entity))
                {
                    pool.Del(entity);
                }
            }
        }

        public static void AddComponentIfNotExists<T>(this EcsPool<T> pool, int entity)
            where T : struct
        {
            if (pool.Has(entity)) return;
            
            pool.Add(entity);
        }

        public static void DelComponentIfExists<T>(this EcsPool<T> pool, int entity)
            where T : struct
        {
            if (!pool.Has(entity)) return;
            
            pool.Del(entity);
        }

        public static bool TryGetComponent<T>(this EcsPool<T> pool, int entity, ref T component)
            where T : struct
        {
            component = default;
            
            if (!pool.Has(entity)) return false;
            
            component = ref pool.Get(entity);
            return true;
        }
    }
}