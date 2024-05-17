using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCollider : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private string text;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreGridManager.Instance.AddScore(count, text);
            Destroy(gameObject);
        }
    }
}
