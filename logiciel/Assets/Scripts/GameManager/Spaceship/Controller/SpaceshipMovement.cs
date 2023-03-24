using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
	public GameObject moon;
	public GameObject rocketModels;
	public float baseSpeed = 5.0f;
	
	public float avoidDistance = 100.0f;
	public float avoidMargin = 15.0f;
	public float avoidanceStrength = 100.0f; // Puissance d'évitement
	public float avoidSpeedMultiplier = 0.5f; // Multiplicateur de vitesse lors de l'évitement
	public float rotationSpeed = 10.0f;

	private GameObject rocket; // Choosed rocket
	private Vector3 avoidTarget; // Destination évitement
	private bool isAvoiding = false; // Indique si la fusée est en train d'éviter un objet
	
	void Start()
	{
		rocket = rocketModels.transform.GetChild(User.Rocket.Id).gameObject;		
		rocket.SetActive(true);
	}
	
	void Update()
	{
		Vector3 target = moon.transform.position;
		float currentSpeed = baseSpeed;

		// Utilisation d'un Raycast pour détecter les collisions à venir
		RaycastHit hit;
		if (Physics.Raycast(rocket.transform.position, rocket.transform.forward, out hit, avoidDistance))
		{
			// Si la fusée va entrer en collision avec un objet autre que la lune, ajuster la trajectoire si elle est trop proche
			if (hit.collider.gameObject != moon && hit.distance < avoidDistance - avoidMargin)
			{
				avoidTarget = rocket.transform.position + Mathf.Clamp(hit.distance - avoidMargin, 0, avoidDistance) * rocket.transform.forward;
				isAvoiding = true;
			}
			else isAvoiding = false;
		}

		// Interpolation linéaire pour ajuster la trajectoire lors de l'évitement
		if (isAvoiding)
		{
			Vector3 avoidanceDirection = (avoidTarget - rocket.transform.position).normalized;
			RaycastHit hit2;
			if (!Physics.Raycast(rocket.transform.position, avoidanceDirection, out hit2, baseSpeed * Time.deltaTime))
			{
				target += avoidanceDirection * avoidanceStrength;
				currentSpeed = Mathf.Lerp(baseSpeed, baseSpeed * avoidSpeedMultiplier, hit.distance / avoidDistance);
			}
		}
		
		rocket.transform.LookAt(target);// Obtenir la direction de mouvement actuelle de la fusée
		Vector3 currentVelocity = (target - transform.position).normalized;

		// Obtenir la rotation cible qui pointe dans la direction de la cible sur l'axe z
		Quaternion targetRotation = Quaternion.LookRotation(currentVelocity, Vector3.forward);

		// Aligner progressivement la fusée avec l'axe z
		rocket.transform.rotation = Quaternion.RotateTowards(rocket.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		rocket.transform.Rotate(Vector3.right, 90);

		rocket.transform.position = Vector3.MoveTowards(rocket.transform.position, target, currentSpeed * Time.deltaTime);
	}
}