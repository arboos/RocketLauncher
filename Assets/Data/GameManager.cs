using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Vector2 movement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (movement.x >= 0.3f) Time.timeScale = 0.25f;
        else Time.timeScale = 1f;
        
    }

    public void OnMove(InputAction.CallbackContext context) => movement = context.ReadValue<Vector2>();

    public void OnBurn(InputAction.CallbackContext context) => print(context.ReadValue<float>());
}
