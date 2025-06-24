using UnityEngine;
using Zenject;

namespace Game
{
    [Icon(EditorIcons.Configuration)]
    [CreateAssetMenu(menuName = "Game/Main configuration")]
    public class MainGameConfigurationInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CubesConfigurationSO _cubesConfigurationSo;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CubesConfiguration>()
                .FromInstance(_cubesConfigurationSo.CubesConfiguration)
                .AsSingle();
        }
    }
}
