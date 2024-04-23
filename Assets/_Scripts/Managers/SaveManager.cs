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

        if (Bridge.storage.IsSupported(StorageType.PlatformInternal)) StorageTypeCurrent = StorageType.PlatformInternal;
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        //SavePositions
        Bridge.storage.Set("LastExplosion", MissileController.Instance.transform.position.x.ToString(), OnStorageSetCompleted, Bridge.storage.defaultType);
        Bridge.storage.Set("LastExplosionFloat", ((int)(GameManager.Instance.Distance/10)).ToString(), OnStorageSetCompleted, Bridge.storage.defaultType);
        if (GetFloat("BestExplosionFloat") < (int)(GameManager.Instance.Distance/10))
        {
            Bridge.storage.Set("BestExplosionFloat", ((int)(GameManager.Instance.Distance/10)).ToString(), OnStorageSetCompleted, Bridge.storage.defaultType);
            Bridge.storage.Set("BestExplosion", MissileController.Instance.transform.position.x.ToString(), OnStorageSetCompleted, Bridge.storage.defaultType);
        }
    }

    public void LoadData()
    {
        //Record Lines
        if (GetFloat("LastExplosion") != GetFloat("BestExplosion"))
        {
            RecordManager.Instance.PreviousLaunchLine.transform.position =
                new Vector3(GetFloat("LastExplosion"), 0f, 0f);
            RecordManager.Instance.PreviousLaunchLine.DistanceText.text =
                GetFloat("LastExplosionFloat").ToString() + "m";
            RecordManager.Instance.BestRecordLine.transform.position =
                new Vector3(GetFloat("BestExplosion"), 0f, 0f);
            RecordManager.Instance.BestRecordLine.DistanceText.text =
                GetFloat("BestExplosionFloat").ToString() + "m";
            print("if");
        }
        else
        {
            RecordManager.Instance.BestRecordLine.transform.position =
                new Vector3(GetFloat("BestExplosion"), 0f, 0f);
            RecordManager.Instance.BestRecordLine.DistanceText.text =
                GetFloat("BestExplosionFloat").ToString() + "m";
            print("else");
        }
        //UI Lines
        UIManager.Instance.bestSlider.value = GetFloat("BestExplosionFloat") / 3000f;
        UIManager.Instance.preciousSlider.value = GetFloat("LastExplosionFloat") / 3000f;

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
