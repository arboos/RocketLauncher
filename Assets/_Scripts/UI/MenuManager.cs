using System;
using System.Collections;
using System.Collections.Generic;
using InstantGamesBridge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuManager : MonoBehaviour
{
    private int[] Prices = new[] { 20, 40, 80, 120, 240, 400, 650, 880, 1020, 1420 };

    [SerializeField] private Button ForceBuy;
    [SerializeField] private Button FuelBuy;
    [SerializeField] private Button MagnetBuy;

    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Sprite ActiveButton;
    [SerializeField] private Sprite DisabledButton;

    private Button[] Buttons;
    
    private void Start()
    {
        Buttons = new []{ForceBuy, FuelBuy, MagnetBuy};
        
        ForceBuy.onClick.AddListener(delegate { BoostForce(); });
        FuelBuy.onClick.AddListener(delegate { BoostFuel(); });
        MagnetBuy.onClick.AddListener(delegate { BoostMagnet(); });
        
        Invoke("UpdateMenuUI", 0.1f);
    }

    public void BoostForce()
    {
        int price = Prices[GameManager.Instance.ForceLevel - 1];
        if (GameManager.Instance.Coins >= price) // Повторная проверка для УВЕРЕННОСТИ
        {
            GameManager.Instance.Coins -= price;
            GameManager.Instance.ForceLevel++;
            
            YandexGame.savesData.force = GameManager.Instance.ForceLevel;
            YandexGame.SaveProgress();
            
            UpdateMenuUI();
        }
    }
    
    public void BoostFuel()
    {
        int price = Prices[GameManager.Instance.FuelLevel - 1];
        if (GameManager.Instance.Coins >= price) // Повторная проверка для УВЕРЕННОСТИ
        {
            GameManager.Instance.Coins -= price;
            GameManager.Instance.FuelLevel++;
            // 1 SaveManager.Instance.Save("Fuel", GameManager.Instance.FuelLevel.ToString());
            MissileController.Instance.CurrentFuel = 9 + (2 * GameManager.Instance.FuelLevel);
            
            YandexGame.savesData.fuel = GameManager.Instance.FuelLevel;
            YandexGame.SaveProgress();
            
            UpdateMenuUI();
        }
    }
    
    public void BoostMagnet()
    {
        int price = Prices[GameManager.Instance.MagnetLevel - 1];
        if (GameManager.Instance.Coins >= price) // Повторная проверка для УВЕРЕННОСТИ
        {
            GameManager.Instance.Coins -= price;
            GameManager.Instance.MagnetLevel++;
            // 1 SaveManager.Instance.Save("Magnet", GameManager.Instance.MagnetLevel.ToString());
            
            YandexGame.savesData.magnet = GameManager.Instance.MagnetLevel;
            YandexGame.SaveProgress();
            
            UpdateMenuUI();
        }
    }

    private void UpdateMenuUI()
    {
        coinsText.text = GameManager.Instance.Coins.ToString();
        
        ForceBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.ForceLevel - 1];
        FuelBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.FuelLevel - 1];
        MagnetBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.MagnetLevel - 1];
        
        ForceBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.ForceLevel - 1].ToString().ToString();
        FuelBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.FuelLevel - 1].ToString().ToString();
        MagnetBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.MagnetLevel - 1].ToString().ToString();

        ForceBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ForceLevel.ToString();
        FuelBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.FuelLevel.ToString();
        MagnetBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.MagnetLevel.ToString();

        foreach (var butt in Buttons)
        {
            if (butt.interactable) butt.GetComponent<Image>().sprite = ActiveButton; 
            else butt.GetComponent<Image>().sprite = DisabledButton;
        }

        #region Govnokod

        

        #endregion
    }

    public void StartGameplay()
    {
        GameManager.Instance.Gameplay = true;
        MissileController.Instance.CurrentFuel = 9 + (2 * GameManager.Instance.FuelLevel);
    }
    
}
