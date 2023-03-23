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

	private float fuel = 1000f;
	private float chosenSpeed = 10f;

	private float launchTime = 0f; // temps écoulé depuis le début du décollage
	private float launchDuration = 20f; // durée du décollage en secondes

	private float initialAcceleration = 0f; // accélération initiale
	private float finalAcceleration = 1000f; // accélération finale

	private float maxVelocity = 1000f; // vitesse maximale à atteindre
	private float velocityExponent = 0.1f; // facteur d'ajustement de la loi exponentielle
	private const string SPACE_SCENE_NAME = "SpaceScene";
	private const float launchAltitude = 1000f;
	
	private GameObject rocketModel;

	void Start()
	{
		rocket.collisionDetectionMode = CollisionDetectionMode.Continuous;
		rocketModel = rocketModels.transform.GetChild(User.Rocket.Id).gameObject;
		rocketModel.SetActive(true);
		
		fireParticles.Stop();
		smokeParticles.Stop();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Space) && fuel > 0)
		{
			launchTime += Time.deltaTime;
			float acceleration = Mathf.Lerp(initialAcceleration, finalAcceleration, launchTime / launchDuration);
			rocket.AddRelativeForce(Vector3.up * acceleration, ForceMode.Acceleration);

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

		if (rocketModel.transform.position.y > launchAltitude)
		{
			SceneManager.LoadScene(SPACE_SCENE_NAME);
		}

		
		// Ajouter de la résistance à l'air
		float speed = rocket.velocity.magnitude;
		Vector3 drag = -speed * speed * rocket.velocity.normalized * 0.1f; // Modifiez 0.1f pour ajuster la force de traînée
		rocket.AddForce(drag, ForceMode.Force);

		// Calculer la vitesse et l'altitude pour affichage dans le jeu
		float speedMS = Mathf.Min(speed, GetMaxVelocity()); // limiter la vitesse à la valeur maximale
		float altitudeM = rocket.position.y;
		speedText.text = (speedMS * chosenSpeed).ToString("0");
		altitudeText.text = (altitudeM / 10f).ToString("0.0");
	}

	private float GetMaxVelocity()
	{
		// Fonction exponentielle inversée pour modéliser la relation entre la vitesse et le temps
		float timeRatio = Mathf.Clamp01(launchTime / launchDuration);
		float maxVelocityRatio = Mathf.Exp(timeRatio * velocityExponent);
		return maxVelocityRatio * maxVelocity;
	}
}
