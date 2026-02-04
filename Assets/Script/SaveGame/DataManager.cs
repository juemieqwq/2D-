using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.IO;
using Newtonsoft.Json;

//运行的优先级设置，越小优先级越大
[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    private DataManager() { }
    public static DataManager instance;

    private List<ISaveableGameObject> saveableGameObjects = new List<ISaveableGameObject>();

    public SavebleGameObjectDate saveDates;
    [Header("监听事件")]
    [SerializeField]
    private VoidEventSO saveDataEventSO;
    [SerializeField]
    private VoidEventSO gameOverEventSO;
    [SerializeField]
    private VoidEventSO loadOverEventSO;
    private string dataFolder;
    private string dateFile;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        saveDataEventSO.AddEventListener(Save);
        loadOverEventSO.AddEventListener(Load);
        dataFolder = Application.persistentDataPath + "/SaveData/";
        dateFile = dataFolder + "data.qwq";
        saveDates = new SavebleGameObjectDate();
        ReadSaveDataFlie();
    }

    private void OnDisable()
    {
        saveDataEventSO.RemoveEventListener(Save);
        loadOverEventSO.RemoveEventListener(Load);
    }

    public void RegisterSaveDate(ISaveableGameObject saveableGameObject)
    {
        if (!saveableGameObjects.Contains(saveableGameObject))
            saveableGameObjects.Add(saveableGameObject);
    }

    public void UnRegisterSaveDate(ISaveableGameObject saveableGameObject)
    {
        if (saveableGameObjects.Contains(saveableGameObject))
            saveableGameObjects.Remove(saveableGameObject);
    }

    public void Save()
    {
        foreach (var saveable in saveableGameObjects)
        {
            saveable.SaveDate(ref saveDates);
        }

        var jsonData = JsonConvert.SerializeObject(saveDates);
        if (!File.Exists(dataFolder))
        {
            Directory.CreateDirectory(dataFolder);
        }
        File.WriteAllText(dateFile, jsonData);
    }

    public void Load()
    {
        foreach (var saveable in saveableGameObjects)
        {
            saveable.LoadSaveDate(ref saveDates);
        }

    }

    private void ReadSaveDataFlie()
    {
        if (File.Exists(dateFile))
        {
            var stringData = File.ReadAllText(dateFile);
            var date = JsonConvert.DeserializeObject<SavebleGameObjectDate>(stringData);
            saveDates = date;
        }
    }
}

