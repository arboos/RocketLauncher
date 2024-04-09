using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lerpSpeed;

    public bool follow;

    private Vector3 offset;

    private Camera camera;

    private void Start()
    {
        offset = transform.position - target.position;
        offset -= new Vector3(0f, 0f, 20f);

        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (follow)
        {
            float camSize = target.position.y + 5;
            camSize = Mathf.Clamp(camSize, 7f, 25f);
            
            transform.position = Vector3.Lerp(transform.position, offset + target.position, lerpSpeed * Time.deltaTime);
            
            camera.orthographicSize = camSize;
        }
    }

}
