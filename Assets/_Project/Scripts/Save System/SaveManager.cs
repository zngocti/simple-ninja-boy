using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    static SaveManager _instance;
    static public SaveManager Instance { get => _instance; }

    [SerializeField] string _statsSaveFileName = "/stats.json";
    [SerializeField] string _gameObjectsSaveFileName = "/gobjects.json";
    [SerializeField] string _inventorySaveFileName = "/inventory.json";

    PlayerStatsSaveableData _statsData = new PlayerStatsSaveableData();
    public PlayerStatsSaveableData PlayerStatsData { get => _statsData; }
    SaveableGameObjectData _gameObjectData = new SaveableGameObjectData();
    public SaveableGameObjectData GameObjectData { get => _gameObjectData; }
    SaveableItemData _inventoryData = new SaveableItemData();
    public SaveableItemData InventoryData { get => _inventoryData; }

    List<ISaveableData> _saveableObjects = new List<ISaveableData>();

    bool _loadingGame = false;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PrepareToLoad()
    {
        _loadingGame = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_loadingGame)
        {
            _loadingGame = false;
            LoadGameData();
        }
    }

    public void SaveGameData()
    {
        for (int i = 0; i < _saveableObjects.Count; i++)
        {
            _saveableObjects[i]?.OnSave(this);
        }

        SaveData<PlayerStatsSaveableData>(_statsData, _statsSaveFileName);
        SaveData<SaveableGameObjectData>(_gameObjectData, _gameObjectsSaveFileName);
        SaveData<SaveableItemData>(_inventoryData, _inventorySaveFileName);
    }

    public void LoadGameData()
    {
        PlayerStatsSaveableData stats = LoadData<PlayerStatsSaveableData>(_statsSaveFileName);
        _statsData = stats == null ? new PlayerStatsSaveableData() : stats;
        SaveableGameObjectData go = LoadData<SaveableGameObjectData>(_statsSaveFileName);
        _gameObjectData = go == null ? new SaveableGameObjectData() : go;
        SaveableItemData inventory = LoadData<SaveableItemData>(_inventorySaveFileName);
        _inventoryData = inventory == null ? new SaveableItemData() : inventory;

        _saveableObjects.Clear();
        var saveables = GameObject.FindObjectsByType<PersistentUniqueID>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        ISaveableData data;

        for (int i = 0; i < saveables.Length; i++)
        {
            if (saveables[i].TryGetComponent<ISaveableData>(out data))
            {
                _saveableObjects.Add(data);
            }            
        }

        for (int i = 0; i < _saveableObjects.Count; i++)
        {
            _saveableObjects[i]?.SetVariablesToSave();
            _saveableObjects[i]?.OnLoad(this);
        }
    }

    public void DeleteGameData()
    {
        DeleteData(_statsSaveFileName);
    }

    void SaveData<T>(T myData, string myFileName)
    {
        string dataPath = Application.persistentDataPath + myFileName;

        string jsonData = JsonUtility.ToJson(myData);
        File.WriteAllText(dataPath, jsonData);

        Debug.Log("Data saved: " + dataPath);
    }

    T LoadData<T>(string myFileName)
    {
        string dataPath = Application.persistentDataPath + myFileName;

        if (!File.Exists(dataPath))
        {
            Debug.LogWarning("No save file found in " + dataPath);
            return default(T);
        }

        string jsonData = File.ReadAllText(dataPath);
        
        T data = JsonUtility.FromJson<T>(jsonData);

        Debug.Log("Data loaded from " + dataPath);

        return data;
    }

    void DeleteData(string myFileName)
    {
        string dataPath = Application.persistentDataPath + myFileName;

        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            Debug.Log("Data deleted from " + dataPath);
        }
        else
        {
            Debug.LogWarning("No save file found to delete in " + dataPath);
        }
    }
}
