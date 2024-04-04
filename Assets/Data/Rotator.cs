using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotForce;
    
    private bool launch;
    private float rotation;
    
    private void Update()
    {
        rotation += GameManager.Instance.movement.y * rotForce * Time.deltaTime;
        transform.Rotate(0f, 0f, -rotation * Time.deltaTime);
    }
    
    public void OnLaunch()
    {
        MissileController.Instance.Launch(rotation);
        Destroy(this);
    }
    
}
