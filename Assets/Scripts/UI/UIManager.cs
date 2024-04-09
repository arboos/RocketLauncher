using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        distanceText.text = "0";
    }

    private void FixedUpdate()
    {
        if(!MissileController.Instance.isRotating) distanceText.text = GameManager.Instance.Distance.ToString();
    }
}
