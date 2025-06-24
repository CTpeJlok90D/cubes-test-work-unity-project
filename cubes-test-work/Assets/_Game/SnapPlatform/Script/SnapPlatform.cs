using UniRx;
using UnityEngine;

namespace Game
{
    public class SnapPlatform : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider;

        private readonly CompositeDisposable _disposable = new();
        
        public ReactiveProperty<Cube> LinkedCube { get; private set; } = new();
        public float MinYPosition => (_boxCollider.transform.position.y + _boxCollider.offset.y + _boxCollider.size.y * 0.5f) * _boxCollider.transform.localScale.y;
        
        private void OnEnable()
        {
            LinkedCube
                .Subscribe(OnLinkedCubeChange)
                .AddTo(_disposable);
        }

        private void OnLinkedCubeChange(Cube cube)
        {
            if (cube != null)
            {
                cube.transform.SetParent(transform);
            }
        }
    }
}
