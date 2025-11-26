using System;
using _Project.Scripts.Features.Data.Entities;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Components
{
    [Serializable]
    public struct RecordComponent
    {
        public RecordDto recordDto;
        public AudioClip AudioClip;
    }
}