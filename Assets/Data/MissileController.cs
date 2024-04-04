using UnityEngine;
using UnityEngine.InputSystem;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private ParticleSystem burnParticleSystem;

    public static MissileController Instance { get; private set; }

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

    public void Launch(float velocity)
    {
        // Missile Actions
        transform.parent = null;
        print("Launch!");
        rb.gravityScale = 1;
        rb.angularVelocity = -200f;
        rb.AddForce(transform.up * velocity / 20f, ForceMode2D.Impulse);

        //Camera Actions
        Camera.main.GetComponent<CameraFollow>().follow = true;
    }

    public void Burn()
    {
        rb.AddForce(transform.up * speed * Time.deltaTime);
        burnParticleSystem.Play();
    }


}
