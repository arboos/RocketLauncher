using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool magneted = false;
    
    public IEnumerator Magneted()
    {
        magneted = true;
        while (Vector2.Distance(transform.position, MissileController.Instance.transform.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, MissileController.Instance.transform.position,
                Time.deltaTime * 100f);
            yield return new WaitForEndOfFrame();
        }
        ScoreGridManager.Instance.AddScore(25, "Coin");
        GameManager.Instance.AddLocalCoins(10);
        Destroy(gameObject);
    }
}
