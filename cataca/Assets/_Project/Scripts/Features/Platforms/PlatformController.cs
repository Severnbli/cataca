using UnityEngine;
using DG.Tweening;

namespace _Project.Scripts.Features.Platforms
{
    public class PlatformController : MonoBehaviour {
        [SerializeField] private Color platformColor;
        [SerializeField] private KeyCode interactionKey;
        [SerializeField] private Ease easeType = Ease.Linear;
        [SerializeField] private float duration = 1f;
        [SerializeField] private PlatformMover[] platforms;

        private void Start()
        {
            foreach (var platform in platforms)
            {
                platform.ChangePlatformColor(platformColor);
            }
        }
        
        private void Update() {
            if (Input.GetKeyDown(interactionKey)) {
                foreach (var platform in platforms) {
                    platform.MoveNext(easeType, duration);
                }
            }
        }
    }

}