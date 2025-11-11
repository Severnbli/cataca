using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class Settings
    {
        public float GlobalVolume { get; set; } = 20f;
        public float MusicVolume { get; set; } = 20f;
        public float SoundsVolume { get; set; } = 20f;
    }
}