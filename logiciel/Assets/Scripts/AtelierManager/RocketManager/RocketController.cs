using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RocketController : MonoBehaviour
{
	public List<GameObject> rocketModels; // Liste des modèles de fusée présents dans la scène
	public List<GameObject> rocketsComponents;
	public GameObject leftArrowButton; // Bouton "flèche gauche"
	public GameObject rightArrowButton; // Bouton "flèche droite"
	
	public Button modalCloseButton; // Close modal Bouton
	
	public Button buttonEquiper; // Close modal Bouton
	public float rotationSpeed = 150f; // Vitesse de rotation de la fusée

	private List<Rocket> rocketsList; // Dictionnaire contenant les instances de Rocket pour chaque fusée
	private int currentRocketIndex = 0; // Index de la fusée actuellement affichée
	
	public GameObject modalRocket;
	
	private const string HOME_SCENE_NAME = "HomeScene";
	private const int ID_DELTA_IV = 0;

	void Start()
	{	
		RocketInitializer ri = new RocketInitializer();
		ri.Init(rocketModels, rocketsComponents);
		rocketsList = ri.rocketList;
		
		// Ajoute les evenemets aux fleches
		leftArrowButton.GetComponent<Button>().onClick.AddListener(OnLeftArrowButtonClick);
		rightArrowButton.GetComponent<Button>().onClick.AddListener(OnRightArrowButtonClick);

		// Ajoute un écouteur d'événement pour le clic sur le bouton "close"
		modalCloseButton.onClick.AddListener(OnCloseModalButtonClick);
		buttonEquiper.onClick.AddListener(OnEquiperButtonClick);

		AddRocketsEventListeners();
		UpdateRocketDisplay(); // Affiche la première fusée
	}

	void Update()
	{	
		if(currentRocketIndex == ID_DELTA_IV)
		{
			rocketsList[currentRocketIndex].Model.transform.Rotate(0f, 0f , rotationSpeed * Time.deltaTime);
		} 
		else 
		{
			rocketsList[currentRocketIndex].Model.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
		}
	}
	
	// On ajoute les liteners aux fusees
	private void AddRocketsEventListeners() 
	{
		foreach (var rocket in rocketsList)
		{
			int index = rocket.Id;

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

			Button button = rocket.Image.gameObject.GetComponent<Button>();
			if (button != null)
			{   
				button.onClick.AddListener(() => {
					currentRocketIndex = index;
					OnButtonClick(rocket);});
			}
		}
	}

	private void OnButtonClick(Rocket rocket)
	{	
		
		modalRocket.SetActive(true);
		UpdateModalContent(rocket);
		UpdateRocketDisplay();
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
		foreach (Rocket r in rocketsList)
		{
			r.Model.SetActive(false);
			r.SelectedBackground.gameObject.SetActive(false);
			if(!(r.Equals(rocketsList[currentRocketIndex]))) {
				r.UnselectedBackground.gameObject.SetActive(true);
			}
			else{
				r.UnselectedBackground.gameObject.SetActive(false);
			}
		}
		rocketsList[currentRocketIndex].SelectedBackground.gameObject.SetActive(true);
		rocketsList[currentRocketIndex].Model.SetActive(true);
		
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
			currentRocketIndex = rocketsList.Count - 1;
		}

		UpdateModalContent(rocketsList[currentRocketIndex]);
		UpdateRocketDisplay();		
	}

	// Appelée lorsqu'on clique sur le bouton "flèche droite"
	public void OnRightArrowButtonClick()
	{
		currentRocketIndex++;
		if (currentRocketIndex >= rocketsList.Count)
		{
			currentRocketIndex = 0;
		}

		UpdateModalContent(rocketsList[currentRocketIndex]);
		UpdateRocketDisplay();
	}

	public void OnEquiperButtonClick()
	{	
		User.Rocket = rocketsList[currentRocketIndex];
		SceneManager.LoadSceneAsync(HOME_SCENE_NAME);
	}
	
	void OnCloseModalButtonClick()
	{
		modalRocket.SetActive(false);
	}
}