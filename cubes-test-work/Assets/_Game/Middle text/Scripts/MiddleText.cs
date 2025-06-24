using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MiddleText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _caption;

        private Color _defaultColor;
        private Sequence _sequence;
        
        private void Awake()
        {
            _defaultColor = _caption.color;
            _caption.color = Color.clear;
        }

        public void Show(string text, float time = 1f)
        {
            _caption.text = text;

            _sequence = DOTween.Sequence()
                .Append(_caption.DOColor(_defaultColor, 0.1f))
                .AppendInterval(time)
                .Append(_caption.DOColor(Color.clear, 0.1f));
        }
    }
}
