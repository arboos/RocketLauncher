using System;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Storage;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        //SavePositions
        Bridge.storage.Set("LastExplosion", MissileController.Instance.transform.position.x.ToString(), OnStorageSetCompleted, StorageType.PlatformInternal);
    }

    public void LoadData()
    {
        RecordManager.Instance.PreviousLaunchLine.transform.position = new Vector3(GetFloat("LastExplosion"), 0f, 0f);
        print("Data loaded");
    }
    

    #region SaveMethods
    
    public string GetString(string key)
    {
        string localVar = null;
        Bridge.storage.Get(key, (success, data) =>
        {
            if (success)
            {
                print(data);
                localVar = data;
            }
            else
            {
                Debug.LogError($"No data from key {key}!");
            }
        });
        return localVar;
    }

    
    public bool GetBool(string key)
    {
        bool localVar = false;
        Bridge.storage.Get(key, (success, data) =>
        {
            if (success)
            {
                print(data);
                localVar = data == "True";
            }
            else
            {
                Debug.LogError($"No data from key {key}!");
            }
        });
        return localVar;
    }
    
    public float GetFloat(string key)
    {
        float localVar = 0f;
        Bridge.storage.Get(key, (success, data) =>
        {
            if (success)
            {
                print(data);
                localVar = float.Parse(data);
            }
            else
            {
                Debug.LogError($"No data from key {key}!");
            }
        });
        return localVar;
    }
    
    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
    
    private void OnStorageGetCompleted(bool success, string data)
    {
        // Загрузка произошла успешно
        if (success)
        {
            if (data != null) {
                Debug.Log(data);
            }
            else
            {
                // Данных по ключу level нет
            }
        }
        else
        {
            // Ошибка, что-то пошло не так
        }
    }
    #endregion
}
