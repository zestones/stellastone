using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Rigidbody rocket;
    public float launchForce = 100f;
    public float fuel = 1000f;
    public ParticleSystem fireParticles;
    public ParticleSystem smokeParticles;

    void Start()
    {
        rocket.collisionDetectionMode = CollisionDetectionMode.Continuous;
        fireParticles.Stop();
        smokeParticles.Stop();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rocket.AddRelativeForce(Vector3.up * launchForce, ForceMode.Acceleration);
            fuel -= Time.deltaTime;
            fireParticles.Play();
            smokeParticles.Play();
        }
        else if (fuel <= 0)
        {
            rocket.AddForce(Physics.gravity * rocket.mass);
            fireParticles.Stop();
            smokeParticles.Stop();
        }
    }
}
