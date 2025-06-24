using UnityEngine;
using Zenject;

namespace Game
{
    public class RootSnapPlatformSave : MonoBehaviour
    {
        [SerializeField] private SnapPlatform _snapPlatform;
        
        [Inject] private Saving _saving;
        [Inject] private CubeById _cubeById;

        private const string SaveKey = "ROOT SNAP PLATRORM SAVE";
        
        private void Start()
        {
            Load();
            _saving.BeforeSave += OnSave;
        }

        private void OnDestroy()
        {
            _saving.BeforeSave -= OnSave;
        }

        private void OnSave()
        {
            Save();
        }

        private void Load()
        {
            if (_saving.TryLoad(SaveKey, out CubeData cubeData) == false)
            {
                return;
            }

            Cube cubePrefab = _cubeById[cubeData.ID];
            Cube cubeInstance = Instantiate(cubePrefab, _snapPlatform.transform).LoadData(cubeData);
            
            DefaultCubeFall defaultCubeFall = cubeInstance.GetComponent<DefaultCubeFall>();
            defaultCubeFall.LinkCubeToPlatform(_snapPlatform);
        }
        
        private void Save()
        {
            Cube cube = _snapPlatform.LinkedCube.Value;

            if (_snapPlatform.LinkedCube.Value == null)
            {
                _saving.RemoveSave(SaveKey);
                return;
            }
            
            CubeData data = cube.ToCubeData();
            _saving.Save(SaveKey, data);
        }
    }
}
