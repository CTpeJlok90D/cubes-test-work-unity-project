using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private CubeDragZone _zone;

        private Saving _saving;
        
        public override void InstallBindings()
        {
            _saving = new();
            _saving.ReadFromFile();
            
            Container
                .Bind<CubeDragZone>()
                .FromInstance(_zone)
                .AsSingle();
            
            Container
                .Bind<Saving>()
                .FromInstance(_saving)
                .AsSingle();
        }
        
        private void OnDestroy()
        {
            _saving.WriteToFile();
        }

        private void OnApplicationQuit()
        {
            _saving.WriteToFile();
        }

        private void OnApplicationPause(bool value)
        {
            if (value == false)
            {
                return;
            }
            
            _saving.WriteToFile();
        }
    }
}