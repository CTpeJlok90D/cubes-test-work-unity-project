using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game
{
    public class MovableCube : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private Cube _cube;
        [SerializeField] private DefaultCubeFall _defaultCubeFall;
        [SerializeField] private SnapPlatform _snapPlatform;

        [Inject] private CubeDragZone _dragZone;

        private readonly CompositeDisposable _disposables = new();
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _defaultCubeFall.StandingPlatform.LinkedCube.Value = null;
            
            if (_snapPlatform.LinkedCube.Value != null)
            {
                _snapPlatform.LinkedCube.Value.transform.SetParent(null);
            }
            
            _dragZone.SpawnCubeDrag(_cube.ToCubeData());
            Destroy(_root);
            
            if (_snapPlatform.LinkedCube.Value != null && _snapPlatform.LinkedCube.Value.TryGetComponent(out DefaultCubeFall cubeFall))
            {
                cubeFall.Fall();
            }
            
            _dragZone.CurrentDragInstance.Subscribe(OnInstanceChange).AddTo(_disposables);
        }

        private void OnInstanceChange(CubeVisualisation cubeInstance)
        {
            if (cubeInstance != null && cubeInstance.Cube.ID == _cube.ID)
            {
                return;
            }
            
            _disposables.Clear();
            Destroy(_root);
        }
    }
}
