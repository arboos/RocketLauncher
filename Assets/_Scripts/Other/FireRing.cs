using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FireRing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D missileRB = MissileController.Instance.gameObject.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(missileRB.velocity.x, missileRB.velocity.y);
            MissileController.Instance.gameObject.GetComponent<Rigidbody2D>().AddForce(force.normalized * 30f, ForceMode2D.Impulse);
            SoundsBaseCollection.Instance.launchSound.Play();
            
            ScoreGridManager.Instance.AddScore(500, "Кольцо!" , "Fire ring!", "Halka!" );
            
            print("Forced");
            Destroy(this);
        }
    }
}
