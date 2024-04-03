using UnityEngine;

public class AddForceToRocket : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform par;
    public float rotForce;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan(rb.velocity.x / rb.velocity.y);
        //transform.rotation = Quaternion.EulerAngles(0, 0, -angle);
        //if(transform.rotation.z < 0) transform.rotation = Quaternion.EulerAngles(0, 0, angle + 90-angle);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            print(2);
            rotForce -= 0.12f;
            print(1);
        }
        par.Rotate(0, 0, rotForce);
        rotForce += Time.deltaTime;
        rotForce = Mathf.Clamp(rotForce, -25f, 0f);
    }
}
