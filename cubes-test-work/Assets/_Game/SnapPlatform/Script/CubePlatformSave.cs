using UnityEngine;
using Zenject;

namespace Game
{
    public class CubePlatformSave : MonoBehaviour
    {
        [SerializeField] private SnapPlatform _snapPlatform;
        [SerializeField] private Cube _cube;
        
        [Inject] private CubeById _cubeById;
        
        private const string SaveKey = "SNAP PLATRORM SAVE";

        private void Awake()
        {
            _cube.JsonSerializationStarted += OnJsonSerializationStart;
            _cube.LoadDataStarted += OnDataLoaded;
        }

        private void OnDestroy()
        {
            _cube.JsonSerializationStarted -= OnJsonSerializationStart;
            _cube.LoadDataStarted -= OnDataLoaded;
        }
        
        private void OnDataLoaded(SaveFile data)
        {
            if (data.TryGetValue(SaveKey, out string json))
            {
                CubeData cubeData = JsonUtility.FromJson<CubeData>(json);

                Cube cubePrefab = _cubeById[cubeData.ID];
                Cube cubeInstance = Instantiate(cubePrefab).LoadData(cubeData);

                DefaultCubeFall defaultCubeFall = cubeInstance.GetComponent<DefaultCubeFall>();
                defaultCubeFall.LinkCubeToPlatform(_snapPlatform);
            }
        }

        private void OnJsonSerializationStart(SaveFile data)
        {
            if (_snapPlatform.LinkedCube.Value == null)
            {
                return;
            }
            
            CubeData cubeData = _snapPlatform.LinkedCube.Value.ToCubeData();
            string json = JsonUtility.ToJson(cubeData);
            
            data.Add(SaveKey, json);
        }
    }
}
