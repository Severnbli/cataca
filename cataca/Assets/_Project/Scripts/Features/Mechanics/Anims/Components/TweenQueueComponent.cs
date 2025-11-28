using System;
using System.Collections.Generic;
using DG.Tweening;

namespace _Project.Scripts.Features.Mechanics.Anims.Components
{
    public struct TweenQueueComponent
    {
        public Queue<Func<Tween>> Queue;
    }
}