using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsBaseCollection : MonoBehaviour
{
    public static SoundsBaseCollection Instance { get; private set; }

    public AudioSource burnSound;
    public AudioSource launchSound;
    public AudioSource tickSound;
    public AudioSource explosionSound;
    
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

    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None); //Add click sound to all buttons
    }
}