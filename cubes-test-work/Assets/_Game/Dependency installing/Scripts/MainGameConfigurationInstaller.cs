using System;
using UnityEngine;
using Zenject;

namespace Game
{
    [Icon(EditorIcons.Configuration)]
    [CreateAssetMenu(menuName = "Game/Main configuration")]
    public class MainGameConfigurationInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CubesConfigurationSO _cubesConfigurationSo;
        [SerializeField] private CubeById _cubeById;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CubesConfiguration>()
                .FromInstance(_cubesConfigurationSo.CubesConfiguration)
                .AsSingle();

            Container
                .Bind<CubeById>()
                .FromInstance(_cubeById)
                .AsSingle();
        }
    }
}
