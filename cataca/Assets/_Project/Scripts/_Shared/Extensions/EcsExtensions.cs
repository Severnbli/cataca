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
    }
}