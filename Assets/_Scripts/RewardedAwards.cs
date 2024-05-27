using System;
using UnityEngine;
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
            // Если ID = 1, то выдаём "+100 монет"
            if (id == 1)
                GameManager.Instance.ForceLevel++;

            // Если ID = 2, то выдаём "+оружие".
            else if (id == 2)
                GameManager.Instance.FuelLevel++;
        
            else if (id == 3)
                GameManager.Instance.MagnetLevel++;
            
            else if (id == 4)
                GameManager.Instance.AddCoins(GameManager.Instance.LocalCoins * 2);
        }
    
    }
