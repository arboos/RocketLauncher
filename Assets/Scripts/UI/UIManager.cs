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
        if (MissileController.Instance.launched)
        {
            distanceText.text = (GameManager.Instance.Distance / 10).ToString() + "m";
            playerSlider.value = MissileController.Instance.transform.position.x / 3000f;
        }

    }
}
