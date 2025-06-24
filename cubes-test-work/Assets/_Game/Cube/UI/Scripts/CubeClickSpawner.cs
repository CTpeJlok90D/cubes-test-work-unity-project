using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
    public class CubeClickSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CubeVisualisation _selfCube;
        [SerializeField] private float _holdTimeToSpawn = 0.15f;
        [SerializeField] private float _maxAcceptablePointerOffset;
        [SerializeField] private InputActionReference _pointerMovement;

        [Inject] private CubeDragZone _cubeDragZone;

        private bool _isPressing;
        private Vector2 _pointerOffset;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressing = true;

            _ = SpawnWithDelay();
        }

        private async UniTask SpawnWithDelay()
        {
            _pointerOffset = Vector2.zero;
            float time = 0;
            
            while (time < _holdTimeToSpawn)
            {
                _pointerOffset = _pointerMovement.action.ReadValue<Vector2>();
                time += Time.deltaTime;
                
                await UniTask.NextFrame();

                if (_isPressing == false || _pointerOffset.magnitude >= _maxAcceptablePointerOffset)
                {
                    return;
                }
            }

            _cubeDragZone.SpawnCube(_selfCube.Cube);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressing = false;
        }
    }
}
