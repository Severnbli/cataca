using System;
using UnityEngine;

namespace _Project.Scripts.Features._Shared.Monos
{
    public class OpenCloseRequestHandler : MonoBehaviour
    {
        [NonSerialized] public bool openRequested;
        [NonSerialized] public bool closeRequested;
    }
}