using System.Collections.Generic;
using DataPersistence.Data;
using UnityEngine;
using System.Linq;

namespace DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")] [SerializeField]
        private string fileName;
        
        
        public static DataPersistenceManager instance;
        private GameData _gameData;
        private List<IDataPersistence> _dataPersistences;
        private FileDataHandler _dataHandler;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            this._dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this._dataPersistences = FindAllPersistenceObjects();
            LoadGame();
        }

        public void NewGame()
        {
            this._gameData = new GameData();
        }

        public void LoadGame()
        {
            this._gameData = _dataHandler.Load();
            
            if (this._gameData == null)
            {
                NewGame();
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistences)
            {
                dataPersistenceObj.LoadData(_gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObj in _dataPersistences)
            {
                dataPersistenceObj.SaveData(ref _gameData);
            }
            
            Debug.Log("game was saved");
            _dataHandler.Save(_gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType
                    <MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

    }
}
