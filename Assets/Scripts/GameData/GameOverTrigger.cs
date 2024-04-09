using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private float timeToInvoke;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("GameOver", timeToInvoke);
        }
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
