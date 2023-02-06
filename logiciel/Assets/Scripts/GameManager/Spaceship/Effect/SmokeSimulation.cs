using UnityEngine;

public class SmokeSimulation : MonoBehaviour
{
    public ParticleSystem smokeParticles;
    private ParticleSystem.MainModule smokeMain;
    private ParticleSystem.EmissionModule smokeEmission;
    private ParticleSystem.CollisionModule smokeCollision;
    private ParticleSystem.ForceOverLifetimeModule smokeForce;

    public GameObject floor;

    void Start()
    {
        smokeMain = smokeParticles.main;
        smokeEmission = smokeParticles.emission;
        smokeCollision = smokeParticles.collision;
        smokeForce = smokeParticles.forceOverLifetime;

        // Enable the CollisionModule
        smokeCollision.enabled = true;

        // Set the floor as the collider
        smokeCollision.SetPlane(0, floor.transform);

        // Enable the ForceOverLifetimeModule
        smokeForce.enabled = true;

        // Set the force to be directed downward
        smokeForce.y = -9.8f;
    }

    void Update()
    {
        // Increase the rate of particle emission over time
        float emissionRate = Mathf.Clamp(Time.timeSinceLevelLoad * 10f, 0, 500);
        smokeEmission.rateOverTime = emissionRate;

        // Change the color of the particles to gray as the smoke intensifies
        if (emissionRate > 400)
        {
            smokeMain.startColor = Color.gray;
        }
        else if (emissionRate > 200)
        {
            smokeMain.startColor = Color.white;
        }
    }
}
