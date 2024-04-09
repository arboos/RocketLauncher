using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody2D rb;

    private bool launched = false;
    public static MissileController Instance { get; private set; }
    
    [HideInInspector] public bool isRotating = true;
    [HideInInspector] public ParticleSystem burnParticleSystem;
    

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
        burnParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (transform.position.y <= -50f) GameManager.Instance.GameOver();
        
        if (launched)
        {
            burnParticleSystem.Stop();
            if(GameManager.Instance.movement.y >= 0.3f) Burn();
        }
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
    }

    public void Burn()
    {
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Force);
        burnParticleSystem.Play();
        print("Burn!");
    }


}
