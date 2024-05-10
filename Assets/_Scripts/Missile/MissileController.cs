using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float CurrentFuel;


    public float timeToStopBurn; 
    [HideInInspector] public float currentTimeToStopBurn;
    
    public static MissileController Instance { get; private set; }
    
    [HideInInspector] public ParticleSystem burnParticleSystem;
    [HideInInspector] public bool launched = false;

    void Awake()
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

    private void Start()
    {
        burnParticleSystem = ParticleCollection.Instance.FireParticleRotation;
        CurrentFuel = 9 + (2 * GameManager.Instance.FuelLevel);
        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (transform.position.y <= -50f) GameManager.Instance.GameOver();
        
        
        if (launched)
        {
            if (rb.velocity.x > 60f || rb.velocity.y > 60f)
            {
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -100f, 100f), Mathf.Clamp(rb.velocity.y, -100f, 100f));
            }
            if(GameManager.Instance.movement.y >= 0.3f) Burn();
        }

        currentTimeToStopBurn -= Time.deltaTime;
        if (currentTimeToStopBurn <= 0)
        {
            if(burnParticleSystem.isEmitting) burnParticleSystem.Stop();
            if(SoundsBaseCollection.Instance.burnSound.isPlaying) SoundsBaseCollection.Instance.burnSound.Stop();
        }
    }

    public void Launch(float velocity)
    {
        // Missile Actions
        transform.parent = null;
        print("Launch!");
        rb.gravityScale = 1;
        rb.angularVelocity = -400f;
        rb.AddForce(transform.up * velocity / 30f, ForceMode2D.Impulse);
        launched = true;
        
        //Camera Actions
        Camera.main.GetComponent<CameraFollow>().follow = true;
        
        //UI Actions
        UIManager.Instance.Gameplay.gameObject.SetActive(true);
        UIManager.Instance.Menu.gameObject.SetActive(false);
        ScoreGridManager.Instance.AddScore(100, "Launch!");
        
        //Particle Actions
        burnParticleSystem.Stop();
        burnParticleSystem = ParticleCollection.Instance.FireParticleBurn;
        
        
        //Audio Actions
        SoundsBaseCollection.Instance.launchSound.Play();
        SoundsBaseCollection.Instance.rotatingSound.Stop();

    }

    public IEnumerator Explode()
    {
        gameObject.SetActive(false);
        SoundsBaseCollection.Instance.explosionSound.Play();
        currentTimeToStopBurn = -1f;
        SoundsBaseCollection.Instance.burnSound.Stop();
        foreach (var par in ParticleCollection.Instance.ExplosionParticles)
        {
            par.gameObject.SetActive(true);
            par.transform.position = transform.position;
            par.Play();
        }
                
        //RecordActions
        SaveManager.Instance.SaveData();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
        
        
    }

    public void Burn()
    {
        if (CurrentFuel > 0)
        {
            CurrentFuel -= Time.deltaTime;
            rb.AddForce(transform.up * ((force + (125f * GameManager.Instance.ForceLevel)) * Time.deltaTime), ForceMode2D.Force);
            ParticleBurn();
        }
    }

    public void ParticleBurn()
    {
        if (!burnParticleSystem.isEmitting) burnParticleSystem.Play();
        if (!SoundsBaseCollection.Instance.burnSound.isPlaying)
        {
            SoundsBaseCollection.Instance.burnSound.time = 0.5f;
            SoundsBaseCollection.Instance.burnSound.Play();
        }
        currentTimeToStopBurn = timeToStopBurn;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
        print("Color changed!");
    }


}