using System;
using System.Collections;
using System.Collections.Generic;
using InstantGamesBridge;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private int[] Prices = new[] { 20, 40, 80, 120, 240, 400, 650, 880, 1020, 1420};

    [SerializeField] private Button ForceBuy;
    [SerializeField] private Button FuelBuy;
    [SerializeField] private Button MagnetBuy;
    
    [SerializeField] private Button ForceBuyAdv;
    [SerializeField] private Button FuelBuyAdv;
    [SerializeField] private Button MagnetBuyAdv;

    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Sprite ActiveButton;
    [SerializeField] private Sprite DisabledButton;

    [SerializeField] private AudioMixer mixer;
    
    [SerializeField] private GameObject SoundOnB;
    [SerializeField] private GameObject SoundOffB;

    private Button[] Buttons;
    
    private void Start()
    {
        Buttons = new []{ForceBuy, FuelBuy, MagnetBuy};
        
        ForceBuy.onClick.AddListener(delegate { BoostForce(); });
        FuelBuy.onClick.AddListener(delegate { BoostFuel(); });
        MagnetBuy.onClick.AddListener(delegate { BoostMagnet(); });
        
        ForceBuyAdv.onClick.AddListener(delegate { RewardedButton(1); UpdateMenuUI(); });
        FuelBuyAdv.onClick.AddListener(delegate { RewardedButton(2); UpdateMenuUI(); });
        MagnetBuyAdv.onClick.AddListener(delegate { RewardedButton(3); UpdateMenuUI(); });

        float tempVolume;
        mixer.GetFloat("mainVolume", out tempVolume);
        if (tempVolume < -10f)
        {
            SoundOnB.SetActive(true);
            SoundOffB.SetActive(false);
        }
        else
        {
            SoundOnB.SetActive(false);
            SoundOffB.SetActive(true);
        }
        
        Invoke("UpdateMenuUI", 0.1f);
    }

    public void SoundOff()
    {
        mixer.SetFloat("mainVolume", -80f);
    }
    
    public void SoundOn()
    {
        mixer.SetFloat("mainVolume", 0);
    }

    public void RewardedButton(int rewardID)
    {
        YandexGame.RewVideoShow(rewardID);
        
        YandexGame.SaveProgress();
        UpdateMenuUI();
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

            MissileController.Instance.missileMagnet.GetComponent<CircleCollider2D>().radius =
                20f * GameManager.Instance.MagnetLevel;
            UpdateMenuUI();
        }
    }

    public void UpdateMenuUI()
    {
        coinsText.text = GameManager.Instance.Coins.ToString();

        if (GameManager.Instance.ForceLevel >= 10)
        {
            ForceBuy.gameObject.SetActive(false);
            ForceBuyAdv.gameObject.SetActive(false);
        }
        else
        {
            ForceBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ForceLevel.ToString();
            ForceBuyAdv.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ForceLevel.ToString();
            ForceBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.ForceLevel - 1].ToString().ToString();
            ForceBuyAdv.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.ForceLevel - 1].ToString().ToString();
        }
        
        if (GameManager.Instance.FuelLevel >= 10)
        {
            FuelBuy.gameObject.SetActive(false);
            FuelBuyAdv.gameObject.SetActive(false);
        }
        else
        {
            FuelBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.FuelLevel - 1].ToString().ToString();
            FuelBuyAdv.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.FuelLevel - 1].ToString().ToString();
            FuelBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.FuelLevel.ToString();
            FuelBuyAdv.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.FuelLevel.ToString();
        }
        
        if (GameManager.Instance.MagnetLevel >= 10)
        {
            MagnetBuy.gameObject.SetActive(false);
            MagnetBuyAdv.gameObject.SetActive(false);
        }

        else
        {
            
            MagnetBuy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.MagnetLevel - 1].ToString().ToString();
            MagnetBuyAdv.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Prices[GameManager.Instance.MagnetLevel - 1].ToString().ToString();
            MagnetBuyAdv.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.MagnetLevel.ToString();
            MagnetBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.MagnetLevel.ToString();
        }

        if (GameManager.Instance.FuelLevel >= 10 || GameManager.Instance.ForceLevel >= 10 ||
            GameManager.Instance.MagnetLevel >= 10) return;
        
        ForceBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.ForceLevel - 1];
        FuelBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.FuelLevel - 1];
        MagnetBuy.interactable = GameManager.Instance.Coins >= Prices[GameManager.Instance.MagnetLevel - 1];
        

        foreach (var butt in Buttons)
        {
            if (butt.interactable) butt.GetComponent<Image>().sprite = ActiveButton; 
            else butt.GetComponent<Image>().sprite = DisabledButton;
        }

        #region Govnokod

        if (!ForceBuy.interactable)
        {
            ForceBuy.gameObject.SetActive(false);
            ForceBuyAdv.gameObject.SetActive(true);
        }
        else
        {
            ForceBuy.gameObject.SetActive(true);
            ForceBuyAdv.gameObject.SetActive(false);
        }

        if (!FuelBuy.interactable)
        {
            FuelBuy.gameObject.SetActive(false);
            FuelBuyAdv.gameObject.SetActive(true);
        }
        else
        {
            FuelBuy.gameObject.SetActive(true);
            FuelBuyAdv.gameObject.SetActive(false);
        }
        
        if (!MagnetBuy.interactable)
        {
            MagnetBuy.gameObject.SetActive(false);
            MagnetBuyAdv.gameObject.SetActive(true);
        }
        
        else
        {
            MagnetBuy.gameObject.SetActive(true);
            MagnetBuyAdv.gameObject.SetActive(false);
        }
        #endregion
    }

    public void StartGameplay()
    {
        GameManager.Instance.Gameplay = true;
        MissileController.Instance.CurrentFuel = 9 + (2 * GameManager.Instance.FuelLevel);
        SoundsBaseCollection.Instance.menuSound.Stop();
        SoundsBaseCollection.Instance.gameplaySound.Play();
    }
    
}
