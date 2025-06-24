using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class CubeDragScrollViewLocker : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollView;
        [Inject] private CubeDragZone _cubeDragZone;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        private void OnEnable()
        {
            _cubeDragZone.CurrentDragInstance
                .Subscribe(OnCurrentDragChange)
                .AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Clear();
        }

        private void OnCurrentDragChange(CubeVisualisation newValue)
        {
            _scrollView.enabled = newValue == null;
        }
    }
}
