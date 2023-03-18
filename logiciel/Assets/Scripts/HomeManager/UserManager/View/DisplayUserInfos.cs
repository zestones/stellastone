using UnityEngine;
using UnityEngine.UI;

public class DisplayUserInfos : MonoBehaviour
{
	[SerializeField] private GameObject userObject;
    [SerializeField] private GameObject rocketInfos;
	private static Text usernameText, descriptionText;
	private static Image avatarImage;
	
	private const string USERNAME = "username";
	private const string AVATAR = "avatar";
	private const string DESCRIPTION = "description";
	
	void Start()
	{
		// Récupère les enfants du GameObject parent
		Transform[] children = userObject.GetComponentsInChildren<Transform>(true);
		
		// Cherche les enfants avec les noms spécifiques
		foreach (Transform child in children)
		{
			if (child.name == USERNAME)
			{
				usernameText = child.GetComponent<Text>();
			}
			else if (child.name == AVATAR)
			{
				avatarImage = child.GetComponent<Image>();
			}
			else if (child.name == DESCRIPTION)
			{
				descriptionText = child.GetComponent<Text>();
			}
		}
		
		SetUserInfos();
		if (User.Rocket!=null) {
		DisplayRocketsInfos();
		}
		else{
			rocketInfos.SetActive(false);
		}
	}

	public static void SetUserInfos(Sprite avatar = null)
	{
		// Met à jour les éléments d'interface utilisateur avec les informations d'utilisateur
		if (usernameText != null)
		{
			usernameText.text = User.Username;
		}
		
		if (descriptionText != null)
		{
			descriptionText.text = User.Description;
		}
		
		if (avatarImage != null)
		{
			if (avatar == null) 
			{
				avatarImage.sprite = User.Avatar;
			}
			else 
			{
				avatarImage.sprite = avatar;
			}
		}
	}

	void DisplayRocketsInfos()
	{
		Slider thrust = rocketInfos.transform.Find("Thrust").GetComponent<Slider>();
		Text thrustValue = thrust.transform.Find("Text").GetComponent<Text>();
		thrust.value = User.Rocket.Thrust;
		thrustValue.text = "Poussée: " + User.Rocket.Thrust + " MN ";

		Slider power = rocketInfos.transform.Find("Power").GetComponent<Slider>();
		Text powerValue = power.transform.Find("Text").GetComponent<Text>();
		power.value = User.Rocket.Power;
		powerValue.text = "Puissance: " + User.Rocket.Power + " MW";

		Slider velocity = rocketInfos.transform.Find("Velocity").GetComponent<Slider>();
		Text velocityValue = velocity.transform.Find("Text").GetComponent<Text>();
		velocity.value = User.Rocket.Velocity;
		velocityValue.text = "Vitesse: " + User.Rocket.Velocity + " m/s";

		Slider payloadmass = rocketInfos.transform.Find("PayloadMass").GetComponent<Slider>();
		Text payloadmassValue = payloadmass.transform.Find("Text").GetComponent<Text>();
		payloadmass.value = User.Rocket.PayloadMass;
		payloadmassValue.text = "Masse utile: " + User.Rocket.PayloadMass + " kg";

		Slider height = rocketInfos.transform.Find("Height").GetComponent<Slider>();
		Text heightValue = height.transform.Find("Text").GetComponent<Text>();
		height.value = User.Rocket.Height;
		heightValue.text = "Hauteur: " + User.Rocket.Height + " m";

		Slider diameter = rocketInfos.transform.Find("Diameter").GetComponent<Slider>();
		Text diameterValue = diameter.transform.Find("Text").GetComponent<Text>();
		diameter.value = User.Rocket.Diameter;
		diameterValue.text = "Diamètre: " + User.Rocket.Diameter + " m";

		Slider fuelCapacity = rocketInfos.transform.Find("FuelCapacity").GetComponent<Slider>();
		Text fuelCapacityValue = fuelCapacity.transform.Find("Text").GetComponent<Text>();
		fuelCapacity.value = User.Rocket.FuelCapacity;
		fuelCapacityValue.text = "Réservoirs: " + User.Rocket.FuelCapacity + " L";
    }
}
