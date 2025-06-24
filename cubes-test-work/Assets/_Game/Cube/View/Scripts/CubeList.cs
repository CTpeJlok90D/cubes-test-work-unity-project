using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CubeList : MonoBehaviour
    {
        [SerializeField] private CubeVisualisation _cubeVisualisationPrefab;
        [SerializeField] private Transform _elementsRoot;
        [Inject] private CubesConfiguration _cubesConfiguration;

        private List<CubeVisualisation> _cubeListElements = new();
        
        private void Start()
        {
            CreateCubesUI();
        }

        private void CreateCubesUI()
        {
            foreach (Cube activeCube in _cubesConfiguration.ActiveCubes)
            {
                CubeVisualisation instance = Instantiate(_cubeVisualisationPrefab, _elementsRoot).Init(activeCube.ToCubeData());
                _cubeListElements.Add(instance);
            }
        }
    }
}
