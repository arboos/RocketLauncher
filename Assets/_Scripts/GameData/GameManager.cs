using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Vector2 movement;

    public int Distance;
    public int DistanceDelta;
    public int Score;

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
        Distance = (int)((MissileController.Instance.transform.position.x + 9f) * 10f);
        DistanceDelta = (int)(Distance/10f) - DistanceDelta;
        DistanceDelta = (int)(MissileController.Instance.transform.position.x + 9f);
        if (movement.x >= 0.3f) Time.timeScale = 0.25f;
        else Time.timeScale = 1f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void OnMove(InputAction.CallbackContext context) => movement = context.ReadValue<Vector2>();

    public void OnBurn(InputAction.CallbackContext context) => print(context.ReadValue<float>());
}
