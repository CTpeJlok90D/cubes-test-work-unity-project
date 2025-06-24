using System;
using System.IO;
using UnityEngine;

namespace Game
{
    public class Saving
    {
        public const string FileName = "Save";
        public const string FileExtension = ".json";
        public static string FilePath => Path.Combine(Application.persistentDataPath, $"{FileName}{FileExtension}");

        private readonly Encryption _encryption =
    #if UNITY_EDITOR == false
            new UTF8Encryption();
    #else
            new NoneEncryption();
    #endif
            
        private SaveFile _saveFile = new SaveFile();

        public event Action BeforeSave;
        
        public void ReadFromFile()
        {
            try
            {
                if (File.Exists(FilePath) == false)
                {
                    Debug.Log("Save file doesn't exist");
                    return;
                }
                
                string encryptedSave = File.ReadAllText(FilePath);
                string jsonSave = _encryption.Decrypt(encryptedSave);
                _saveFile = JsonUtility.FromJson<SaveFile>(jsonSave);
#if UNITY_EDITOR
                Debug.Log($"Load success: \n{jsonSave}");
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while reading save file");
                Debug.LogException(ex);
                _saveFile = new();
            }
        }

        public void WriteToFile()
        {
            try
            {
                BeforeSave?.Invoke();
                
                string jsonSave = JsonUtility.ToJson(_saveFile, true);
                string encryptedSave = _encryption.Encrypt(jsonSave);
                File.WriteAllText(FilePath, encryptedSave);
#if UNITY_EDITOR
                Debug.Log($"Save success: \n{jsonSave}");
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while writing save file");
                Debug.LogException(ex);
            }
        }

        public void Save(string saveKey, object saveValue)
        {
            if (_saveFile.ContainsKey(saveKey) == false)
            {
                _saveFile.Add(saveKey, null);
            }

            string jsonFile = JsonUtility.ToJson(saveValue);
            _saveFile[saveKey] = jsonFile;
        }

        public bool TryLoad<T>(string saveKey, out T value)
        {
            try
            {
                if (_saveFile.ContainsKey(saveKey) == false)
                {
                    value = default;
                    return false;
                }

                string jsonValue = _saveFile[saveKey];
                value = JsonUtility.FromJson<T>(jsonValue);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while reading value:");
                Debug.LogException(ex);
                value = default;
                return false;
            }
        }

        public bool RemoveSave(string saveKey)
        {
            return _saveFile.Remove(saveKey);
        }
    }
}