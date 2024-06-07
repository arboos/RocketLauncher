using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

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

    public void AddScore(int count, string textRU, string textEN, string textTR)
    {
        TextMeshProUGUI scoreGridText = Instantiate(UIManager.Instance.ScoreGridTextPrefab);
        scoreGridText.transform.parent = UIManager.Instance.ScoreGrid;

        print("Language = " + YandexGame.EnvironmentData.language);
        
        switch (YandexGame.EnvironmentData.language)
        {
            case "ru":
                scoreGridText.text = count.ToString() + " " + textRU;
                break;
            
            case "en":
                scoreGridText.text = count.ToString() + " " + textEN;
                break;
            
            case "tr":
                scoreGridText.text = count.ToString() + " " + textTR;
                break;
                
        }
        GameManager.Instance.Score += count;
        StartCoroutine(DeleteScorePrefab(scoreGridText.gameObject));
    }
    
    public void AddScore(int count)
    {
        GameManager.Instance.Score += count;
    }

    private IEnumerator DeleteScorePrefab(GameObject prefab)
    {
        yield return new WaitForSeconds(3f);
        Destroy(prefab);
    }
    
}
