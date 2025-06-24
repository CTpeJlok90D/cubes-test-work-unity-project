using System;
using UnityEngine;

namespace Game
{
    [Icon(EditorIcons.Configuration)]
    [CreateAssetMenu(menuName = "Game/Cubes config", fileName = "Cubes config")]
    public class CubesConfigurationSO : ScriptableObject
    {
        [field: SerializeField] public CubesConfiguration CubesConfiguration { get; private set; }
    }
}
