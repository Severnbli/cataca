using System;
using DG.Tweening;

namespace _Project.Scripts.Features.Mechanics.Anims.Requests
{
    public struct TweenQueueAppendRequest
    {
        public Func<Tween> Func;
    }
}