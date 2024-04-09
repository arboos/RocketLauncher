using UnityEngine;

public class ParticleCollection : MonoBehaviour
{
    public static ParticleCollection Instance { get; private set; }

    public ParticleSystem FireParticleBurn;
    public ParticleSystem FireParticleRotation;
    //public ParticleSystem LauchParticle;
    public ParticleSystem WaterBling;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
