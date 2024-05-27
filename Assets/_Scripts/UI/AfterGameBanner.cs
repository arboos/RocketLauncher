using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGameBanner : MonoBehaviour
{
    public void DoubleCoins()
    {
        StartCoroutine(DoubleCoinsIEnumerator());
    }
    
    public IEnumerator DoubleCoinsIEnumerator()
    {
        

        yield return new WaitForSeconds(2f);


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
