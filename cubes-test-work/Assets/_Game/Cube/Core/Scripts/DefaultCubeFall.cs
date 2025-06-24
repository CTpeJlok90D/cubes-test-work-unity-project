using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class DefaultCubeFall : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private BoxCollider2D _collision;
        [SerializeField] private float _fallSpeed = 4;
        [SerializeField] private bool _fallOnStart;
        public SnapPlatform StandingPlatform { get; private set; }
        private const float SnapYOffset = 0.02f;
        private const int DefaultCollision = 2;
        private const int CollisionLayerAfterFall = 0;
        
        public Vector2 SnapPoint => (Vector2)transform.position - new Vector2(0, _collision.size.y * 0.5f);
        public RaycastHit2D RaycastDown => Physics2D.Raycast(SnapPoint, Vector2.down);

        public event Action Falled;
        
        private void Start()
        {
            if (_fallOnStart)
            {
                Fall();
            }
        }

        public void Fall(bool skipAnimation = false)
        {
            transform.SetParent(null);
            _collision.gameObject.layer = DefaultCollision;
            
            Vector2 fallPosition = FindFallPosition();
            if (TryDestroyCubeOnInvalidPosition(fallPosition) == false && StandingPlatform != null)
            {
                LinkCubeToPlatform();
                if (skipAnimation == false)
                {
                    MoveToFallPosition(fallPosition);
                }
                else
                {
                    TeleportToFallPosition(fallPosition);
                }
            }
        }

        private Vector2 FindFallPosition()
        {
            RaycastHit2D raycastHit2D = RaycastDown;
            
            if (raycastHit2D == false)
            {
                return transform.position;
            }
            
            return raycastHit2D.point + new Vector2(0, SnapYOffset);
        }

        private bool TryDestroyCubeOnInvalidPosition(Vector2 fallPosition)
        {
            if (RaycastDown == false || RaycastDown.collider.gameObject.TryGetComponent(out SnapPlatform platform) == false)
            {
                return false;
            }

            StandingPlatform = platform;
            
            if (fallPosition.y < platform.MinYPosition)
            {
                _cube.Destroy();
                return true;
            }

            return false;
        }

        private void LinkCubeToPlatform()
        {
            if (RaycastDown == false || RaycastDown.collider.gameObject.TryGetComponent(out SnapPlatform platform) == false)
            {
                return;
            }

            LinkCubeToPlatform(platform);
        }

        public void LinkCubeToPlatform(SnapPlatform snapPlatform)
        {
            StandingPlatform = snapPlatform;
            
            if (StandingPlatform.LinkedCube.Value == null)
            {
                StandingPlatform.LinkedCube.Value = _cube;
            }
            else
            {
                _cube.Destroy();
            }
        }

        private void MoveToFallPosition(Vector2 fallPosition)
        {
            Vector2 offset = (Vector2)transform.position - SnapPoint;
            Vector2 destination = fallPosition + offset;

            float distance = Vector2.Distance(transform.position, destination);
            float fallDuration = distance / _fallSpeed;

            Vector3 originalScale = transform.localScale;
            Vector3 stretchedScale = new Vector3(originalScale.x * 0.9f, originalScale.y * 1.1f, originalScale.z);

            DOTween.Sequence()
                .Append(transform.DOMove(destination, fallDuration).SetEase(Ease.Linear))
                .Join(transform.DOScale(stretchedScale, fallDuration * 0.2f).SetEase(Ease.OutQuad))
                .AppendCallback(() =>
                {
                    _collision.gameObject.layer = CollisionLayerAfterFall;
                    Falled?.Invoke();
                })
                .Append(transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutElastic))
                .SetLink(gameObject);
        }

        private void TeleportToFallPosition(Vector2 fallPosition)
        {
            Vector2 offset = (Vector2)transform.position - SnapPoint;
            transform.position = fallPosition + offset;
            _collision.gameObject.layer = CollisionLayerAfterFall;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(SnapPoint, Vector2.down * 10);
        }
#endif
    }
}
