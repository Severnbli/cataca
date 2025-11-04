using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class Record
    {
        private string id;

        public string Id
        {
            get => id;
            set => id = value;
        }

        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}