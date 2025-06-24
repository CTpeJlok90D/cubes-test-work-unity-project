using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CubeSprite : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeVisualisation;
        [SerializeField] private Image _image;

        private void Start()
        {
            _image.sprite = _cubeVisualisation.Cube.Sprite;
        }
    }
}
