using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleByTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var pos = particle.transform;
            pos.position = other.transform.position;
            pos.position += new Vector3(2f, 0f, 0f);
            particle.Play();
            sound.Play();
        }
    }
}

