using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class CubeDestroyedText : MonoBehaviour
    {
        [SerializeField] private LocalizedString _cubeDestroyedText;
        [SerializeField] private MiddleText _middleText;

        private void OnEnable()
        {
            Cube.Destroyed += OnCubeDestroy;
        }

        private void OnDisable()
        {
            Cube.Destroyed -= OnCubeDestroy;
        }

        private void OnCubeDestroy(Cube cube)
        {
            _middleText.Show(_cubeDestroyedText.GetLocalizedString());
        }
    }
}
