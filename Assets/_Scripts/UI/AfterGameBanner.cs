using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AfterGameBanner : MonoBehaviour
{
    public void DoubleCoins()
    {
        StartCoroutine(DoubleCoinsIEnumerator());
    }
    
    public IEnumerator DoubleCoinsIEnumerator()
    {
        YandexGame.RewVideoShow(4);
        GetComponent<Animator>().SetTrigger("end");
        
        YandexGame.SaveProgress();
        yield return new WaitForSeconds(1f);

        //SceneManager.LoadScene(0);
    }

    public void CloseBanner()
    {
        StartCoroutine(CloseBannerIEnumerator());
    }

    public IEnumerator CloseBannerIEnumerator()
    {
        GameManager.Instance.AddCoins(GameManager.Instance.LocalCoins);
        YandexGame.savesData.coins = GameManager.Instance.Coins;
        
        GetComponent<Animator>().SetTrigger("end");
        
        YandexGame.SaveProgress();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }
}
