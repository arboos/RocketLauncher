using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGridManager : MonoBehaviour
{
    public static ScoreGridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddScore(int count, string text)
    {
        TextMeshProUGUI scoreGridText = Instantiate(UIManager.Instance.ScoreGridTextPrefab);
        scoreGridText.transform.parent = UIManager.Instance.ScoreGrid;
        scoreGridText.text = count.ToString() + " " + text;
        GameManager.Instance.Score += count;
    }
    
    public void AddScore(int count)
    {
        GameManager.Instance.Score += count;
    }
    
    
}
