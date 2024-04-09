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
            MissileController.Instance.launched = false;
            ParticleCollection.Instance.WaterBling.transform.position = MissileController.Instance.transform.position;
            ParticleCollection.Instance.WaterBling.Play();
            MissileController.Instance.SetColor(new Color(100, 200, 255));
        }
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
