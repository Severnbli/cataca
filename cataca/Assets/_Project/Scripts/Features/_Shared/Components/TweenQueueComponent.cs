using System;
using System.Collections.Generic;
using DG.Tweening;

namespace _Project.Scripts.Features._Shared.Components
{
    public struct TweenQueueComponent
    {
        public Queue<Func<Tween>> Queue;
    }
}