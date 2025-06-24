using UnityEngine;

namespace Game
{
    public class CubeVisualisation : MonoBehaviour
    {
        public Cube Cube { get; private set; }

        public CubeVisualisation Init(Cube cube)
        {
            Cube = cube;
            
            return this;
        }
    }
}