using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    public float timeToStopParticle; 
    [HideInInspector] public float currentTimeToStopParticle;
    
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
        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        burnParticleSystem = ParticleCollection.Instance.FireParticleRotation;
    }

    private void Update()
    {
        if (transform.position.y <= -50f) GameManager.Instance.GameOver();
        
        if (launched)
        {
            if(GameManager.Instance.movement.y >= 0.3f) Burn();
        }

        currentTimeToStopParticle -= Time.deltaTime;
        if (currentTimeToStopParticle <= 0 && burnParticleSystem.isEmitting) burnParticleSystem.Stop();

    }

    public void Launch(float velocity)
    {
        // Missile Actions
        transform.parent = null;
        print("Launch!");
        rb.gravityScale = 1;
        rb.angularVelocity = -400f;
        rb.AddForce(transform.up * velocity / 20f, ForceMode2D.Impulse);
        launched = true;
        
        //Camera Actions
        Camera.main.GetComponent<CameraFollow>().follow = true;
        
        //UI Actions
        UIManager.Instance.distanceText.gameObject.SetActive(true);
        
        //Particle Actions
        burnParticleSystem.Stop();
        burnParticleSystem = ParticleCollection.Instance.FireParticleBurn;
    }

    public void Burn()
    {
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Force);
        print("Burn!");

    }

    public void ParticleBurn()
    {
        if (!burnParticleSystem.isEmitting) burnParticleSystem.Play();
        currentTimeToStopParticle = timeToStopParticle;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
        print("Color changed!");
    }


}
