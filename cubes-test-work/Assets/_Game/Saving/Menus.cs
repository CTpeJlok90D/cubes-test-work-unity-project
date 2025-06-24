#if UNITY_EDITOR
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DevTools 
{
    public static class Menus
    {
        [MenuItem("Saving/Clear saves")]
        private static void ClearSaves()
        {
            Directory.Delete(Application.persistentDataPath, true);
            Debug.Log("Saves was deleted");
        }
    
        [MenuItem("Saving/Open save folder")]
        private static void OpenSaveFile()
        {
            string path = Application.persistentDataPath;
            Debug.Log("Opening folder: " + path);
            Process.Start(path);
        }
    }
}
#endif