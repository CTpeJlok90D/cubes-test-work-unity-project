using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
    public class CubeDragZone : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeToSpawnPrefab;
        [SerializeField] private InputActionReference _pointerPosition;
        [SerializeField] private InputActionReference _pointerEvents;
        [SerializeField] private Camera[] _cameras;

        [Inject] private CubeById _cubeById;
        
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
                Vector2 worldPosition = Vector2.zero;
                
                try
                {
                    worldPosition = GetPointerWorldPosition();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    DestroyCubeDrag();
                    return;
                }
                
                SpawnCubeInstance(CurrentDragInstance.Value.Cube, worldPosition);
                DestroyCubeDrag();
            }
        }

        private Vector2 GetPointerWorldPosition()
        {
            Vector2 pointerPosition = _pointerPosition.action.ReadValue<Vector2>();
            
            foreach (Camera camera in _cameras)
            {
                if (camera.pixelRect.Contains(pointerPosition))
                {
                    Vector2 result = camera.ScreenToWorldPoint(pointerPosition);

                    return result;
                }
            }

            throw new Exception("Pointer not in camera zone!");
        }

        public CubeVisualisation SpawnCubeDrag(CubeData cube)
        {
            CurrentDragInstance.Value = Instantiate(_cubeToSpawnPrefab, transform).Init(cube);
            return CurrentDragInstance.Value;
        }

        public void DestroyCubeDrag()
        {
            if (CurrentDragInstance.Value == null)
            {
                throw new InvalidOperationException("Cube is not spawned. Nothing to destroy");
            }
            
            Destroy(CurrentDragInstance.Value.gameObject);
            CurrentDragInstance.Value = null;
        }

        public void SpawnCubeInstance(CubeData cube, Vector2 position)
        {
            Cube cubeInstance = Instantiate(_cubeById[cube.ID], position, Quaternion.identity);
            DefaultCubeFall defaultCubeFall = cubeInstance.GetComponent<DefaultCubeFall>();
            defaultCubeFall?.Fall();
            cubeInstance.gameObject.SetActive(true);
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