using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class LevelDto
    {
        public int Id;
        public string Name;
        
        public static bool operator ==(LevelDto a, LevelDto b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(LevelDto a, LevelDto b)
        {
            return !(a == b);
        }
        
        protected bool Equals(LevelDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((LevelDto)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 46;
        }
    }
}