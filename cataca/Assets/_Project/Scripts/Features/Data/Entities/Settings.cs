using System;

namespace _Project.Scripts.Features.Data.Entities
{
    [Serializable]
    public class Settings
    {
        public float GlobalVolume { get; set; } = 0f;
        public float MusicVolume { get; set; } = 0f;
        public float SoundsVolume { get; set; } = 0f;
    }
}