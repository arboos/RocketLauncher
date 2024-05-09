using System;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Storage;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public StorageType StorageTypeCurrent = StorageType.LocalStorage;

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
        if (GetFloat("Force") <= 0)
        {
            Bridge.storage.Set("LastExplosion", -200, OnStorageSetCompleted);
            Bridge.storage.Set("LastExplosionFloat", -200, OnStorageSetCompleted);
            Bridge.storage.Set("BestExplosionFloat", -200, OnStorageSetCompleted);
            Bridge.storage.Set("BestExplosion", -200, OnStorageSetCompleted);
            Bridge.storage.Set("Force", 1, OnStorageSetCompleted);
            Bridge.storage.Set("Fuel", 1, OnStorageSetCompleted);
            Bridge.storage.Set("Magnet", 1, OnStorageSetCompleted);
        }
        LoadData();
    }

    public void DeleteData()
    {
        Bridge.storage.Delete("LastExplosion");
        Bridge.storage.Delete("LastExplosionFloat");
        Bridge.storage.Delete("BestExplosionFloat");
        Bridge.storage.Delete("BestExplosion");
        print("Data deleted");
    }
    
    public void SaveData()
    {
        //SavePositions
        Bridge.storage.Set("LastExplosion", MissileController.Instance.transform.position.x.ToString(), OnStorageSetCompleted);
        print("Data by key: " + "LastExplosion" + " has " + GetFloat("LastExplosion"));
        Bridge.storage.Set("LastExplosionFloat", ((int)(GameManager.Instance.Distance/10)).ToString(), OnStorageSetCompleted);
        
        if (GetFloat("BestExplosionFloat") < (int)(GameManager.Instance.Distance/10))
        {
            Bridge.storage.Set("BestExplosionFloat", ((int)(GameManager.Instance.Distance/10)).ToString(), OnStorageSetCompleted);
            print("Data by key: " + "BEST EXPLOSION!!" + " has " + GetFloat("BestExplosion"));
            Bridge.storage.Set("BestExplosion", MissileController.Instance.transform.position.x.ToString(), OnStorageSetCompleted);
        }
    }

    public void LoadData()
    {
        //Record Lines
        RecordManager.Instance.PreviousLaunchLine.transform.position =
                new Vector3(GetFloat("LastExplosion"), 0f, 0f);
        print("Data by key(Load Data): " + "LastExplosion" + " has " + GetFloat("LastExplosion"));
        RecordManager.Instance.PreviousLaunchLine.DistanceText.text =
                GetFloat("LastExplosionFloat").ToString() + "m";
        RecordManager.Instance.BestRecordLine.transform.position =
                new Vector3(GetFloat("BestExplosion"), 0f, 0f);
        RecordManager.Instance.BestRecordLine.DistanceText.text = GetFloat("BestExplosionFloat").ToString() + "m";
        print("L != B");
        
        //UI Lines
        UIManager.Instance.bestSlider.value = GetFloat("BestExplosionFloat") / 3000f;
        UIManager.Instance.preciousSlider.value = GetFloat("LastExplosionFloat") / 3000f;
        UIManager.Instance.BestScoreDATA.text = ((int)GetFloat("BestExplosion")).ToString() + "m";
        UIManager.Instance.PreviousScoreDATA.text = ((int)GetFloat("LastExplosion")).ToString() + "m";
        
        //Gameplay
        GameManager.Instance.ForceLevel = GetInt("Force");
        GameManager.Instance.FuelLevel = GetInt("Fuel");
        GameManager.Instance.MagnetLevel = GetInt("Magnet");

        print("Data loaded");
    }

    private void OnApplicationQuit()
    {
        DeleteData();
    }


    #region SaveMethods

    public void Save(string key, string value)
    {
        Bridge.storage.Set(key, value, OnStorageSetCompleted);
    }
    
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
                localVar = data == "true";
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
                float.TryParse(data, out localVar);
            }
            else
            {
                Debug.LogError($"No data from key {key}!");
            }
        });
        return localVar;
    }
    
    public int GetInt(string key)
    {
        int localVar = 0;
        Bridge.storage.Get(key, (success, data) =>
        {
            if (success)
            {
                int.TryParse(data, out localVar);
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
        //Debug.Log($"OnStorageSetCompleted, success: {success}");
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
                print("NO DATA FOR GET!");
            }
        }
        else
        {
            // Ошибка, что-то пошло не так
        }
    }
    #endregion
}
