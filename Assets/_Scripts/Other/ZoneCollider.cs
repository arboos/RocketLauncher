using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCollider : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private string textRU;
    [SerializeField] private string textEn;
    [SerializeField] private string textTr;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreGridManager.Instance.AddScore(count, textRU, textEn, textTr);
            Destroy(gameObject);
        }
    }
}
