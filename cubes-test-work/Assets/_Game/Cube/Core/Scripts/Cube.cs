using System;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Sprite Sprite => _spriteRenderer.sprite;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }
#endif
    }
}
