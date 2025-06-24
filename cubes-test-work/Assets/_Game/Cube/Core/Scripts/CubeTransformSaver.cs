using UnityEngine;

namespace Game
{
    public class CubeTransformSaver : MonoBehaviour
    {
        [SerializeField] private Cube _cube;

        private const string key = "TRANSFORM";
        
        private void OnEnable()
        {
            _cube.JsonSerializationStarted += OnJsonSerializationStart;
            _cube.LoadDataStarted += OnLoadDataStart;
        }

        private void OnDisable()
        {
            _cube.JsonSerializationStarted -= OnJsonSerializationStart;
            _cube.LoadDataStarted -= OnLoadDataStart;
        }

        private void OnLoadDataStart(SaveFile data)
        {
            if (data.TryGetValue(key, out string transformJson))
            {
                TransformData transformData = JsonUtility.FromJson<TransformData>(transformJson);
                _cube.transform.position = transformData.Position;
            }
        }

        private void OnJsonSerializationStart(SaveFile data)
        {
            TransformData transformData = new()
            {
                Position = _cube.transform.position
            };

            string json = JsonUtility.ToJson(transformData);
            data.Add(key, json);
        }
    }
}
