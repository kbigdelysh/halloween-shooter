using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Data
{
    /// <summary>
    /// This singleton class saves and load game data.
    /// </summary>
    public class Config : MonoBehaviour
    {
        public static Config instance = null;
        private ConfigData _configData;
        public string configFileName;
        private string _configFileFullPath;

        void Awake()
        {
            MakeSingleton();

            _configFileFullPath = Path.Combine(Application.dataPath, configFileName);

            Load();
        }

        /// <summary>
        /// Make this class as singleton to make sure there is a sigle instance of this script.
        /// </summary>
        private void MakeSingleton()
        {
            if (instance == null)
                instance = this;
            // If, mistakenly, this script is attached to another gameObject,
            // delete the gameObject (which consequently deletes this script instance).
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Saves game data into a text file in Json format.
        /// </summary>
        public void Save()
        {
            string json = JsonUtility.ToJson(_configData);
            // Write the json string to a text file.
            File.WriteAllText(_configFileFullPath, json);
        }

        /// <summary>
        /// Loads game data from a json file.
        /// </summary>
        public void Load()
        {
            if (_configData is null)
                _configData = new ConfigData();
            // Load the text file into json string. 
            try
            {
                string jsonText = File.ReadAllText(_configFileFullPath);
                JsonUtility.FromJsonOverwrite(jsonText, _configData);
            }
            catch (IOException e)
            {
                Debug.Log("The config file does not exist.");
                Save();
                Debug.Log("Created the config file");
            }
        }


        public string GetTitle()
        {
            return _configData.Title;
        }

        public string GetVersionNumber()
        {
            return _configData.VersionNumber;
        }
        public int GetGameSessionLengthInSec()
        {
            return _configData.GameSessionLenghtInSec;
        }

        public string GetPumpkinTypeSpawn()
        {
            return _configData.PumpkinTypeSpawn;
        }

        void OnApplicationQuit()
        {
            Save();
        }
    }
}
