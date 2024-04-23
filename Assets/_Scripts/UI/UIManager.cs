using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    [SerializeField] private Slider playerSlider;
    public Slider bestSlider;
    public Slider preciousSlider;
    public RectTransform ScoreGrid;
    public TextMeshProUGUI ScoreGridTextPrefab;
    public TextMeshProUGUI ScoreText;
    
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        distanceText.text = "0";
    }

    private void Update()
    {
        ScoreText.text = GameManager.Instance.Score.ToString();
        if (MissileController.Instance.launched)
        {
            distanceText.text = (GameManager.Instance.Distance / 10).ToString() + "m";
            ScoreText.text = ((GameManager.Instance.Distance / 10) + GameManager.Instance.Score).ToString();
            playerSlider.value = MissileController.Instance.transform.position.x / 3000f;
        }
    }
}
