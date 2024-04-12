using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSoundTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!MissileController.Instance.launched) return;
            if(!SoundsBaseCollection.Instance.tickSound.isPlaying) SoundsBaseCollection.Instance.tickSound.Play();
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(SoundsBaseCollection.Instance.tickSound.isPlaying) SoundsBaseCollection.Instance.tickSound.Stop();
        }
    }
}
