using UnityEngine;

public class WaterStreamControl : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private bool usingParticle; // Tracks whether the particle system is active

    void Start()
    {
        particleSystem.Stop(); // Ensure the particle system starts in a stopped state
        usingParticle = false;
    }

    void Update()
    {
        // Toggle particle system on/off when pressing P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (usingParticle)
            {
                particleSystem.Stop();
                usingParticle = false;
            }
            else
            {
                particleSystem.Play();
                usingParticle = true;
            }
        }
    }
}
