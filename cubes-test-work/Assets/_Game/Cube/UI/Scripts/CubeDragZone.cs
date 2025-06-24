using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class CubeDragZone : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeToSpawnPrefab;
        [SerializeField] private InputActionReference _pointerPosition;
        [SerializeField] private InputActionReference _pointerEvents;

        public ReactiveProperty<CubeVisualisation> CurrentDragInstance { get; private set; } = new();
        
        private void Awake()
        {
            _pointerPosition.action.Enable();
            _pointerEvents.action.Enable();
        }

        private void OnEnable()
        {
            _pointerEvents.action.canceled += OnPointerUp;
        }

        private void OnDisable()
        {
            _pointerEvents.action.canceled -= OnPointerUp;
        }
        
        private void OnPointerUp(InputAction.CallbackContext obj)
        {
            if (CurrentDragInstance.Value != null)
            {
                DestroyCube();
            }
        }

        public CubeVisualisation SpawnCube(Cube cube)
        {
            CurrentDragInstance.Value = Instantiate(_cubeToSpawnPrefab, transform).Init(cube);
            return CurrentDragInstance.Value;
        }

        public void DestroyCube()
        {
            if (CurrentDragInstance.Value == null)
            {
                throw new InvalidOperationException("Cube is not spawned. Nothing to destroy");
            }
            
            Destroy(CurrentDragInstance.Value.gameObject);
            CurrentDragInstance.Value = null;
        }

        private void Update()
        {
            if (CurrentDragInstance.Value != null)
            {
                Vector2 position = _pointerPosition.action.ReadValue<Vector2>();
            
                CurrentDragInstance.Value.transform.position = position;
            }
        }
    }
}