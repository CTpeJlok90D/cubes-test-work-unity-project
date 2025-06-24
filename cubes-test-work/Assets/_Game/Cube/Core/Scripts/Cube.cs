using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _id;

        public Sprite Sprite => _spriteRenderer.sprite;
        public string ID => _id;
        
        public delegate void JsonSerializationStartedDelegate(SaveFile data);
        public delegate void LoadDataStartedDelegate(SaveFile data);

        public delegate void DestroyedDelegate(Cube cube);
        public event JsonSerializationStartedDelegate JsonSerializationStarted;
        public event LoadDataStartedDelegate LoadDataStarted;
        public static event DestroyedDelegate Destroyed;
        
        public void Destroy()
        {
            Sequence destroySequence = DOTween.Sequence();
            
            Destroyed?.Invoke(this);
            destroySequence
                .Append(transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutBack))
                .Append(transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetEase(Ease.InOutSine))
                .Append(transform.DORotate(new Vector3(0, 0, -10), 0.2f).SetLoops(2, LoopType.Yoyo))
                .Append(transform.DOScale(0f, 0.2f).SetEase(Ease.InBack))
                .OnComplete(() => Destroy(gameObject));
        }

        public CubeData ToCubeData()
        {
            SaveFile data = new();
            JsonSerializationStarted?.Invoke(data);

            CubeData cubeData = new()
            {
                ID = _id,
                Data = data
            };
            return cubeData;
        }

        public Cube LoadData(CubeData data)
        {
            LoadDataStarted?.Invoke(data.Data);
            return this;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.IsPlaying(this))
            {
                return;
            }
            
            _id = name;
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }
#endif
    }
}
