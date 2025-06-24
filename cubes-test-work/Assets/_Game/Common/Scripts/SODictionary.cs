using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Common
{
    [Icon(EditorIcons.Book)]
    public class SODictionary<TKey, TValue> : ScriptableObject
    {
        [SerializedDictionary("ID", "RESULT")]
        [SerializeField] private SerializedDictionary<TKey, TValue> _values = new();
        [SerializeField] private TValue _errorValue;

        public IReadOnlyDictionary<TKey, TValue> Dictionary => _values;

        public TValue this[TKey key]
        {
            get
            {
                if (_values.ContainsKey(key))
                {
                    return _values[key];
                }

                Debug.LogError($"Value for key {key} was not found", this);
                return _errorValue;
            }
        }

        protected SerializedDictionary<TKey, TValue> Values => _values;
    }
}