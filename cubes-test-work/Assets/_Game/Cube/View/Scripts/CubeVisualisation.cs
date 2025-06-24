using UnityEngine;

namespace Game
{
    public class CubeVisualisation : MonoBehaviour
    {
        public CubeData Cube { get; private set; }

        public CubeVisualisation Init(CubeData cube)
        {
            Cube = cube;
            
            return this;
        }
    }
}