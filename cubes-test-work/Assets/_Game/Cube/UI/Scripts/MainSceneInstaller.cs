using UnityEngine;
using Zenject;

namespace Game
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private CubeDragZone _zone;

        public override void InstallBindings()
        {
            Container
                .Bind<CubeDragZone>()
                .FromInstance(_zone)
                .AsSingle();
        }
    }
}