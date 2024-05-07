using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotForce;
    
    private bool launch;
    public float rotation;
    [SerializeField] private float maxRotation;
    
    private void Update()
    {
        if (MissileController.Instance.CurrentFuel > 0)
        {
            rotation += GameManager.Instance.movement.y * rotForce * Time.deltaTime;
            transform.Rotate(0f, 0f, -rotation * Time.deltaTime);
            rotation -= Time.deltaTime * 100f;
            rotation = Mathf.Clamp(rotation, 0, maxRotation);
            if (!MissileController.Instance.launched)
            {
                if (GameManager.Instance.movement.y > 0.01f)
                {
                    MissileController.Instance.CurrentFuel -= Time.deltaTime;
                }

                if (GameManager.Instance.movement.y >= 0.3f && !SoundsBaseCollection.Instance.rotatingSound.isPlaying)
                {
                    SoundsBaseCollection.Instance.rotatingSound.time = rotation / 21f;
                    SoundsBaseCollection.Instance.rotatingSound.Play();
                }
                else if (GameManager.Instance.movement.y < 0.1f &&
                         SoundsBaseCollection.Instance.rotatingSound.isPlaying)
                {
                    SoundsBaseCollection.Instance.rotatingSound.Stop();
                }
            }
        }
    }
    
    public void OnLaunch()
    {
        MissileController.Instance.Launch(rotation);
        Destroy(this);
    }
    
}
