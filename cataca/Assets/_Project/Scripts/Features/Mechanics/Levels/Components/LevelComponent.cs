using System;
using _Project.Scripts.Features.Data.Entities;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Components
{
    [Serializable]
    public struct LevelComponent
    {
        public LevelDto LevelDto;
        public GameObject Prefab;
    }
}