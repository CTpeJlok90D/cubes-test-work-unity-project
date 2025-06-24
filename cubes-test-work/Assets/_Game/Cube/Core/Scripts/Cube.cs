using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Sprite Sprite => _spriteRenderer.sprite;
        
        public void Destroy()
        {
            Sequence destroySequence = DOTween.Sequence();
            
            destroySequence
                .Append(transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutBack))
                .Append(transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetEase(Ease.InOutSine))
                .Append(transform.DORotate(new Vector3(0, 0, -10), 0.2f).SetLoops(2, LoopType.Yoyo))
                .Append(transform.DOScale(0f, 0.2f).SetEase(Ease.InBack))
                .OnComplete(() => Destroy(gameObject));
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }
#endif
    }
}
