using Core.Common;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Cubes by ids", fileName = "Cubes by ids")]
    public class CubeById : SODictionary<string, Cube>
    {
        private const string LabelName = "l:Cube";
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            string[] assetGUIDs = AssetDatabase.FindAssets(LabelName);
            Values.Clear();
            
            foreach (string assetGUID in assetGUIDs)
            {
                GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assetGUID));
                Cube cube = asset.GetComponent<Cube>();

                if (cube == null)
                {
                    Debug.LogError($"{asset} is not {nameof(Cube)}");
                }
                
                Values.Add(cube.ID, cube);
            }
        }
#endif
    }
}
