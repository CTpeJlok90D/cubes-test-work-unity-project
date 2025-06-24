using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeHoleDisposeAnimation;
        [SerializeField] private Transform _holePosition;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Cube cube))
            {
                HoleCube(cube);
            }
        }

        private void HoleCube(Cube cube)
        {
            CubeVisualisation visualisation = Instantiate(_cubeHoleDisposeAnimation, cube.transform.position, cube.transform.rotation).Init(cube.ToCubeData());
            AnimateCube(visualisation);
            Destroy(cube.gameObject);
        }

        private void AnimateCube(CubeVisualisation visualisation)
        {
            DOTween.Sequence()
                .Append(visualisation.transform.DOMove(_holePosition.position, 0.4f).SetEase(Ease.InQuad))
                .Join(visualisation.transform.DOScale(0f, 0.4f).SetEase(Ease.InBack))
                .Join(visualisation.transform.DORotate(new Vector3(0, 0, 180), 0.4f, RotateMode.FastBeyond360))
                .OnComplete(() =>
                {
                    Destroy(visualisation.gameObject);
                });
        }
    }
}
