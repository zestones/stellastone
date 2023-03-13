using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RocketController : MonoBehaviour
{
	public List<GameObject> rocketModels; // Liste des modèles de fusée présents dans la scène

	public GameObject leftArrowButton; // Bouton "flèche gauche"
	public GameObject rightArrowButton; // Bouton "flèche droite"
	public Button modalCloseButton; // Close modal Bouton

	public float rotationSpeed = 150f; // Vitesse de rotation de la fusée

	private Dictionary<int, Rocket> rockets; // Dictionnaire contenant les instances de Rocket pour chaque fusée
	private int currentRocketIndex = 0; // Index de la fusée actuellement affichée
	
	public GameObject modalRocket;

	void Start()
	{	
		rockets = new Dictionary<int, Rocket>();
		rockets.Add(0, new Rocket("Saturn V", "A powerful rocket used in the Apollo missions", 1000000, 100, rocketModels[0]));
		rockets.Add(1, new Rocket("Falcon Heavy", "A reusable rocket developed by SpaceX", 500000, 90, rocketModels[1]));
		rockets.Add(2, new Rocket("Delta IV", "A medium-to-heavy rocket operated by the United Launch Alliance", 3000000, 110, rocketModels[2]));

		// Ajoute les evenemets aux fleches
		leftArrowButton.GetComponent<Button>().onClick.AddListener(OnLeftArrowButtonClick);
		rightArrowButton.GetComponent<Button>().onClick.AddListener(OnRightArrowButtonClick);

		// Ajoute un écouteur d'événement pour le clic sur le bouton "close"
		modalCloseButton.onClick.AddListener(OnCloseModalButtonClick);

		AddRocketsEventListeners();
		UpdateRocketDisplay(); // Affiche la première fusée
	}

	void Update()
	{
		List<Rocket> rocketList = new List<Rocket>(rockets.Values);
		rocketList[currentRocketIndex].Model.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
	
	// On ajoute les liteners aux fusees
	private void AddRocketsEventListeners() 
	{
		foreach (Rocket rocket in rockets.Values)
		{
			// Ajoute un événement de clic à chaque modèle de fusée
			Collider collider = rocket.Model.GetComponent<Collider>();
			if (collider != null)
			{
				collider.gameObject.AddComponent<RocketEvents>().OnClick += () =>
				{
					// Affiche le GameObject "modalRocket" et met à jour son texte
					modalRocket.SetActive(true);
					UpdateModalContent(rocket);
				};
			}
		}
	}
	
	// Affiche la fusée correspondant à l'index donné
	private void DisplayRocket(int index)
	{
		currentRocketIndex = index;
		UpdateRocketDisplay();
	}

	// Affiche la fusée actuellement sélectionnée
	private void UpdateRocketDisplay()
	{
		// Désactive tous les modèles de fusée
		foreach (GameObject model in rocketModels)
		{
			model.SetActive(false);
		}

		// Active le modèle de la fusée actuellement sélectionnée
		List<Rocket> rocketList = new List<Rocket>(rockets.Values);
		rocketList[currentRocketIndex].Model.SetActive(true);
	}
	
	public void UpdateModalContent(Rocket rocket) 
	{
		modalRocket.transform.Find("Name").GetComponent<Text>().text = rocket.Name;
		modalRocket.transform.Find("Description").GetComponent<Text>().text = rocket.Description;
		modalRocket.transform.Find("Cost").GetComponent<Text>().text = rocket.Cost.ToString();
		modalRocket.transform.Find("Power").GetComponent<Text>().text = rocket.Power.ToString();
	}

	// Appelée lorsqu'on clique sur le bouton "flèche gauche"
	public void OnLeftArrowButtonClick()
	{
		currentRocketIndex--;
		if (currentRocketIndex < 0)
		{
			currentRocketIndex = rockets.Count - 1;
		}

		UpdateModalContent(rockets[currentRocketIndex]);
		UpdateRocketDisplay();		
	}

	// Appelée lorsqu'on clique sur le bouton "flèche droite"
	public void OnRightArrowButtonClick()
	{
		currentRocketIndex++;
		if (currentRocketIndex >= rockets.Count)
		{
			currentRocketIndex = 0;
		}

		UpdateModalContent(rockets[currentRocketIndex]);
		UpdateRocketDisplay();
	}
	
	void OnCloseModalButtonClick()
	{
		modalRocket.SetActive(false);
	}
}