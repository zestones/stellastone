using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RocketLauncher : MonoBehaviour
{
	public Rigidbody rocket;
	public ParticleSystem fireParticles;
	public ParticleSystem smokeParticles;
	public TextMeshProUGUI speedText;
	public TextMeshProUGUI altitudeText;
	public Text Status;
	public GameObject rocketModels;

	private float fuel; // quantity of fuel left in the tank

	private float launchTime = 0f; // time since launch in seconds
	private float launchDuration = 30f; // duration of the launch in seconds

	private float initialAcceleration = 0f; // initial acceleration
	private float finalAcceleration; // final acceleration

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
	private AudioSource audioSource; 
	void Start()
	{
		rocket.collisionDetectionMode = CollisionDetectionMode.Continuous;
		rocketModel = rocketModels.transform.GetChild(User.Rocket.Id).gameObject;
		rocketModel.SetActive(true);
		
		// Unity uses SI units, and the force is in Newtons, so we need to convert it to kN
		finalAcceleration = User.Rocket.Thrust * 1000000f / ROCKET_MASS;
		fuel = User.Rocket.FuelCapacity;

		fireParticles.Stop();
		smokeParticles.Stop();
		audioSource = GameObject.Find("AudioDecollage").GetComponent<AudioSource>();
	}

	void Update()
	{	
		if(Input.GetKeyDown(KeyCode.Space)) audioSource.Play(); 
	
		if (Input.GetKey(KeyCode.Space) && fuel > 0)
		{
			launchTime += Time.deltaTime; // increment the launch time
			float currentAcceleration = Mathf.Lerp(initialAcceleration, finalAcceleration, launchTime / launchDuration); // in m/s^2  		
			rocket.AddRelativeForce(Vector3.up * currentAcceleration, ForceMode.Acceleration); // add the force to the rocket
			
			float fuelConsumption = FUEL_CONSUMPTION_RATE * Time.deltaTime; // L/s * s = L = kg / FUEL_DENSITY = kg of fuel consumed in this frame
			float massLoss = fuelConsumption * FUEL_DENSITY; // kg of fuel consumed in this frame * kg/L = kg of fuel consumed in this frame
			
			// Update the rocket mass and fuel remaining
			fuel -= fuelConsumption; 
			ROCKET_MASS -= massLoss; 
	
			fireParticles.Play();
			smokeParticles.Play();
			Status.text = "Décollage en cours... Maintenez la touche espace.";
			
		}
		else if (fuel <= 0)
		{
			rocket.AddForce(Physics.gravity * rocket.mass);
			fireParticles.Stop();
			smokeParticles.Stop();
		}
		else if (!Input.GetKey(KeyCode.Space) && launchTime > 0){
			Status.text = "Votre fusée risque de se crasher, appuyez sur Espace!";
		}
		else 
		{
			fireParticles.Stop();
			smokeParticles.Stop();
			
		}

		// Change the Scene when the rocket reaches the limit altitude
		if (rocketModel.transform.position.y > launchAltitude) SceneManager.LoadScene(SPACE_SCENE_NAME);

		float speed = rocket.velocity.magnitude; // in m/s 
		// The drag force is proportional to the square of the speed
		Vector3 drag = Mathf.Pow(speed, 2) * 0.5f * AIR_DENSITY * ROCKET_DRAG_COEFFICIENT * defaultCrossSectionalArea * -rocket.velocity.normalized;

		rocket.AddForce(drag, ForceMode.Force);
		
		// Compute the rocket's speed and altitude
		// We divide by 10 because the rocket's velocity is in m/s, but the speed is in km/h
		float speedMS = Mathf.Min(speed, User.Rocket.Velocity) / 10f; // limit the speed to the maximum allowed
		float altitudeM = rocket.position.y; // deduce the altitude from the rocket's position
		
		// Convert speed and altitude to km/h and km
		float speedKMH = speedMS * 3.6f * 985f;
		float altitudeKM = altitudeM / 7f;
								
		// display the real speed and altitude of the rocket on the screen
		speedText.text = speedKMH.ToString("0");
		altitudeText.text = altitudeKM.ToString("0.0");

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