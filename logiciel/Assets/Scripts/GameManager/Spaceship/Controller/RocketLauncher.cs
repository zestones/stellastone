using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RocketLauncher : MonoBehaviour
{
	public Rigidbody rocket;
	public ParticleSystem fireParticles;
	public ParticleSystem smokeParticles;
	public TextMeshProUGUI speedText;
	public TextMeshProUGUI altitudeText;

	public GameObject rocketModels;

	private float fuel; // quantity of fuel left in the tank
	private float chosenSpeed = 0.001f; // factor to multiply the max velocity by

	private float launchTime = 0f; // time since launch in seconds
	private float launchDuration = 30f; // duration of the launch in seconds

	private float initialAcceleration = 0f; // initial acceleration
	private float finalAcceleration; // final acceleration

	private float maxVelocity; // maximum velocity of the rocket in m/s
	private const string SPACE_SCENE_NAME = "SpaceScene"; // name of the scene to load when the rocket reaches the launch altitude
	private const float launchAltitude = 1000f; // altitude at which the rocket will be launched in meters

	private GameObject rocketModel; // game object containing the rocket models

	// constantes physiques
	private const float GRAVITY = 9.81f; // m/s^2
	private const float AIR_DENSITY = 1.2f; // kg/m^3
	private const float ROCKET_DRAG_COEFFICIENT = 0.75f; // unitless
	private const float FUEL_DENSITY = 0.85f; // kg/L
	private const float FUEL_CONSUMPTION_RATE = 0.75f; // L/s

	private float ROCKET_AREA = Mathf.PI * 0.25f * User.Rocket.Diameter * User.Rocket.Diameter; // m^2
	private float ROCKET_MASS = User.Rocket.PayloadMass + User.Rocket.FuelCapacity * FUEL_DENSITY; // kg
	
	float defaultCrossSectionalArea = 0.1f; // Example cross-sectional area of a rocket in m^2
	private const int ROCKET_ROTATION_SPEED = 25; // Speed at which the rocket rotates in degrees per second

	void Start()
	{
		rocket.collisionDetectionMode = CollisionDetectionMode.Continuous;
		rocketModel = rocketModels.transform.GetChild(User.Rocket.Id).gameObject;
		rocketModel.SetActive(true);

		maxVelocity = User.Rocket.Velocity / chosenSpeed;
		// Unity uses SI units, and the force is in Newtons, so we need to convert it to kN
		finalAcceleration = User.Rocket.Thrust * 1000000f / ROCKET_MASS;
		fuel = User.Rocket.FuelCapacity;

		fireParticles.Stop();
		smokeParticles.Stop();
	}

	void Update()
	{	
		if (Input.GetKey(KeyCode.Space) && fuel > 0)
		{
			launchTime += Time.deltaTime;
			// calculate the current acceleration
			float currentAcceleration = Mathf.Lerp(initialAcceleration, finalAcceleration, launchTime / launchDuration);			
			rocket.AddRelativeForce(Vector3.up * currentAcceleration, ForceMode.Acceleration); // add the force to the rocket
			
			float fuelConsumption = FUEL_CONSUMPTION_RATE * Time.deltaTime; // L/s * s = L = kg / FUEL_DENSITY = kg of fuel consumed in this frame
			float massLoss = fuelConsumption * FUEL_DENSITY; // kg of fuel consumed in this frame * kg/L = kg of fuel consumed in this frame
			
			// Update the rocket mass and fuel remaining
			fuel -= fuelConsumption;
			ROCKET_MASS -= massLoss;
	
			fireParticles.Play();
			smokeParticles.Play();
		}
		else if (fuel <= 0)
		{
			rocket.AddForce(Physics.gravity * rocket.mass);
			fireParticles.Stop();
			smokeParticles.Stop();
		}
		else 
		{
			fireParticles.Stop();
			smokeParticles.Stop();
		}

		if (rocketModel.transform.position.y > launchAltitude) SceneManager.LoadScene(SPACE_SCENE_NAME);

		// Ajouter de la résistance à l'air
		float speed = rocket.velocity.magnitude;
		Vector3 drag = Mathf.Pow(speed, 2) * 0.5f * AIR_DENSITY * ROCKET_DRAG_COEFFICIENT * defaultCrossSectionalArea * -rocket.velocity.normalized;

		rocket.AddForce(drag, ForceMode.Force);
		
		// Calculer la vitesse et l'altitude pour affichage dans le jeu
		float speedMS = Mathf.Min(speed, User.Rocket.Velocity) / 10f; // limiter la vitesse à la valeur maximale et mettre à l'échelle pour éviter des valeurs trop grandes
		float altitudeM = rocket.position.y - User.Rocket.Height; // déduire la hauteur initiale pour obtenir l'altitude par rapport au sol
		speedText.text = speedMS.ToString("0.0") + " m/s";
		altitudeText.text = altitudeM.ToString("0.0") + " m";

		// Compute rocket mass and thrust based on fuel remaining
		float mass = User.Rocket.PayloadMass + fuel * User.Rocket.FuelDensity; // compute rocket mass based on fuel remaining
		float thrust = User.Rocket.Thrust * Mathf.Clamp01(fuel / User.Rocket.FuelCapacity); // adjust thrust based on fuel remaining
		float acceleration = (thrust - drag.magnitude - mass * GRAVITY) / mass; // compute the acceleration of the rocket based on the forces acting on it
		acceleration = Mathf.Clamp(acceleration, -User.Rocket.MaxAcceleration, User.Rocket.MaxAcceleration); // limit the acceleration to the maximum allowed

		// Add acceleration to rocket
		Vector3 rocketAcceleration = Vector3.up * acceleration;
		rocket.AddRelativeForce(rocketAcceleration, ForceMode.Acceleration); 

		// Display fuel percentage
		float fuelPercentage = Mathf.Clamp01(fuel / User.Rocket.FuelCapacity);
	}

	// Update the rocket physicals properties when the rocket is launched
	private void FixedUpdate()
	{
		// Rotate the rocket to point towards the sky (inverse of the Physics.gravity vector)
		Vector3 gravityDirection = -Physics.gravity.normalized;
		Vector3 rocketDirection = rocket.transform.up;
		Quaternion targetRotation = Quaternion.FromToRotation(rocketDirection, gravityDirection) * rocket.transform.rotation;
		rocket.transform.rotation = Quaternion.Slerp(rocket.transform.rotation, targetRotation, Time.deltaTime * ROCKET_ROTATION_SPEED);
	}
}