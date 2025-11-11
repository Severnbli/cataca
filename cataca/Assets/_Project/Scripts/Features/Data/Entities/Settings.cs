using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class Settings
    {
        public float GlobalVolume { get; set; }
        public float MusicVolume { get; set; }
        public float SoundsVolume { get; set; }
    }
}