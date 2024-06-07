using System;
using TMPro;
using UnityEngine;
using YG;

public class SaverData : MonoBehaviour
{

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

// Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем Ваш метод для загрузки
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

// Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
        RecordManager.Instance.BestRecordLine.transform.position =
            new Vector2(YandexGame.savesData.bestFlag, 0);
        RecordManager.Instance.PreviousLaunchLine.transform.position =
            new Vector2(YandexGame.savesData.lastFlag, 0);

        //UI Lines
        UIManager.Instance.bestSlider.value = YandexGame.savesData.bestFlag / 5000f;
        UIManager.Instance.preciousSlider.value = YandexGame.savesData.lastFlag / 5000f;
        
        RecordManager.Instance.PreviousLaunchLine.DistanceText.text =
            YandexGame.savesData.lastFlag + "m";

        RecordManager.Instance.BestRecordLine.DistanceText.text =
            YandexGame.savesData.bestFlag + "m";

        if (YandexGame.savesData.bestFlag <= -150f)
        {
            UIManager.Instance.BestScoreDATA.text =  "0m";
            UIManager.Instance.PreviousScoreDATA.text = "0m";
        }
        else
        {
            UIManager.Instance.BestScoreDATA.text = YandexGame.savesData.bestFlag + 9 + "m";
            UIManager.Instance.PreviousScoreDATA.text = YandexGame.savesData.lastFlag + 9 + "m";
        }

        //Gameplay
        GameManager.Instance.Coins = YandexGame.savesData.coins;

        GameManager.Instance.ForceLevel = YandexGame.savesData.force;
        GameManager.Instance.FuelLevel = YandexGame.savesData.fuel;
        GameManager.Instance.MagnetLevel = YandexGame.savesData.magnet;
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        //textMoney.text = YandexGame.savesData.money.ToString();
    }

    // Допустим, это Ваш метод для сохранения
    public void MySave()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        //YandexGame.savesData.money = money;

        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }

    private void OnApplicationQuit()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
