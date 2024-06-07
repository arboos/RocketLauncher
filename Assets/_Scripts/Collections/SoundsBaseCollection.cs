using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsBaseCollection : MonoBehaviour
{
    public static SoundsBaseCollection Instance { get; private set; }

    public AudioSource rotatingSound;
    public AudioSource burnSound;
    public AudioSource launchSound;
    public AudioSource tickSound;
    public AudioSource explosionSound;
    public AudioSource cloudSound;
    public AudioSource ringSound;
    public AudioSource bubbleSound;
    public AudioSource coinSound;
    public AudioSource menuSound;
    public AudioSource gameplaySound;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
            burnSound.time = 0.3f;
        }
        else
        {
            Destroy(gameObject);
        }

        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None); //Add click sound to all buttons
    }

    private void Start()
    {
        menuSound.Play();
        gameplaySound.Stop();
    }

    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None); //Add click sound to all buttons
        menuSound.Play();
        gameplaySound.Stop();
    }
}
