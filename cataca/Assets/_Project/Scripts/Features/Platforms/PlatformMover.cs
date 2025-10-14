using UnityEngine;
using DG.Tweening;

namespace _Project.Scripts.Features.Platforms
{
    public class PlatformMover : MonoBehaviour {
        [SerializeField] private SpriteRenderer platformSpriteRenderer;
        [SerializeField] private Transform platformTransform;
        [SerializeField] private Transform pointsParent;

        private Transform[] points;
        private int currentIndex;
        private Vector3 initialScale;

        private void Awake() {
            initialScale = platformTransform.localScale;
            points = new Transform[pointsParent.childCount];
            for (int i = 0; i < points.Length; i++) points[i] = pointsParent.GetChild(i);
            platformTransform.position = points[0].position;
            platformTransform.rotation = points[0].rotation;
        }

        public void ChangePlatformColor(Color color)
        {
            platformSpriteRenderer.color = color;
        }

        public void MoveNext(Ease ease, float duration) {
            if (points.Length <= 1) return;
            currentIndex = (currentIndex + 1) % points.Length;
            var target = points[currentIndex];
            var targetScale = new Vector3(
                initialScale.x * target.localScale.x,
                initialScale.y * target.localScale.y,
                initialScale.z * target.localScale.z
            );

            platformTransform.DOMove(target.position, duration).SetEase(ease);
            platformTransform.DORotateQuaternion(target.rotation, duration).SetEase(ease);
            platformTransform.DOScale(targetScale, duration).SetEase(ease);
        }
    }
}