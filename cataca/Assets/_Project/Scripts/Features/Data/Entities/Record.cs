using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class Record
    {
        public int Id;

        public static bool operator ==(Record a, Record b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Record a, Record b)
        {
            return !(a == b);
        }
        
        protected bool Equals(Record other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Record)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 47;
        }
    }
}