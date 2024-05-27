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
        
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }

    public void CloseBanner()
    {
        StartCoroutine(CloseBannerIEnumerator());
    }

    public IEnumerator CloseBannerIEnumerator()
    {
        GameManager.Instance.AddCoins(GameManager.Instance.LocalCoins);
        GameManager.Instance.LocalCoins = 0;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }
}
