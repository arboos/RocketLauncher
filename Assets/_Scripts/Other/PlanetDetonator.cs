using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDetonator : MonoBehaviour
{
    [SerializeField] private string _text;
    [SerializeField] private string _textRU;
    [SerializeField] private int _points;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreGridManager.Instance.AddScore(_points, _textRU, _text, _text);
            MissileController.Instance.Explode();
        }
    }
}
