using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class CubeSprite : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeVisualisation;
        [SerializeField] private Image _image;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Inject] private CubeById _cubeById;
        
        private void Start()
        {
            if (_image != null)
            {
                _image.sprite = _cubeById[_cubeVisualisation.Cube.ID].Sprite;
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _cubeById[_cubeVisualisation.Cube.ID].Sprite;
            }
        }
    }
}
