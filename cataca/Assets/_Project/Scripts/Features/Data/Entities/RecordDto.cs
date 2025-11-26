using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class RecordDto
    {
        public int Id;

        public static bool operator ==(RecordDto a, RecordDto b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(RecordDto a, RecordDto b)
        {
            return !(a == b);
        }
        
        protected bool Equals(RecordDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((RecordDto)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 47;
        }
    }
}