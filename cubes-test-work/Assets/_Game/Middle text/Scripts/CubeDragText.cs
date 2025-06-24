using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Game
{
    public class CubeDragText : MonoBehaviour
    {
        [SerializeField] private MiddleText _middleText;
        [SerializeField] private LocalizedString _cubeTaken;
        [SerializeField] private LocalizedString _cubeDroped;
        [Inject] private CubeDragZone _cubeDragZone;

        private CompositeDisposable _compositeDisposable = new();
        private bool _first;
        
        private void Start()
        {
            _cubeDragZone.CurrentDragInstance.Subscribe(OnCurrentDragInstanceChange).AddTo(_compositeDisposable);
        }

        private void OnDestroy()
        {
            _compositeDisposable.Clear();
        }

        private void OnCurrentDragInstanceChange(CubeVisualisation cube)
        {
            if (_first == false)
            {
                _first = true;
                return;
            } // Да костыль. С UniRx работаю впервые, не знал, что при подписке метод автоматически вызывается.
            
            string text;
            if (cube == null)
            {
                text = _cubeDroped.GetLocalizedString();
            }
            else
            {
                text = _cubeTaken.GetLocalizedString();
            }
            
            _middleText.Show(text);
        }
    }
}
