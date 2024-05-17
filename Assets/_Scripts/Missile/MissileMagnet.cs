using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMagnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            if(!other.GetComponent<Coin>().magneted) other.GetComponent<Coin>().StartCoroutine(other.GetComponent<Coin>().Magneted());
        }
    }
}
