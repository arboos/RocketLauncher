using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class RewardedAwards : MonoBehaviour
{
    public static RewardedAwards Instance { get; private set; }
    
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

        // Подписываемся на событие открытия рекламы в OnEnable
        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

        // Отписываемся от события открытия рекламы в OnDisable
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

// Подписанный метод получения награды
        void Rewarded(int id)
        {
            Time.timeScale = 0f;
            if (id == 1)
            {
                GameManager.Instance.ForceLevel++;
                YandexGame.savesData.force = GameManager.Instance.ForceLevel;
                MenuManager.Instance.UpdateMenuUI();
            }
            
            else if (id == 2)
            {
                GameManager.Instance.FuelLevel++;
                YandexGame.savesData.fuel = GameManager.Instance.FuelLevel;
                MenuManager.Instance.UpdateMenuUI();
            }

            else if (id == 3)
            {
                GameManager.Instance.MagnetLevel++;
                YandexGame.savesData.magnet = GameManager.Instance.MagnetLevel;
                MenuManager.Instance.UpdateMenuUI();
            }

            else if (id == 4)
            {
                GameManager.Instance.AddCoins(GameManager.Instance.LocalCoins * 2);
                YandexGame.savesData.coins = GameManager.Instance.Coins;
                MenuManager.Instance.UpdateMenuUI();
            }

            YandexGame.SaveProgress();
            SceneManager.LoadScene(0);
        }
        
    
    }
