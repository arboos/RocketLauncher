using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotForce;
    
    private bool launch;
    public float rotation;
    [SerializeField] private float maxRotation;
    
    private void Update()
    {
        rotation += GameManager.Instance.movement.y * rotForce * Time.deltaTime;
        transform.Rotate(0f, 0f, -rotation * Time.deltaTime);
        rotation -= Time.deltaTime * 100f;
        rotation = Mathf.Clamp(rotation, 0, maxRotation);
    }
    
    public void OnLaunch()
    {
        MissileController.Instance.Launch(rotation);
        Destroy(this);
    }
    
}
