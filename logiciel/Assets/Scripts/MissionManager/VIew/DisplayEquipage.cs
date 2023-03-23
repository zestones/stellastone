using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEquipage : MonoBehaviour 
{   public List<Astronaut> astronauts;
	public GameObject astronautCardPrefab; // le préfab de la carte d'astronaute
	public GameObject Equipage;
	public GameObject EquipageCards;
	private Button closeButton;
	
	void Start()
	{
		Equipage.SetActive(false);
		if (User.Rocket != null) DisplayCards();
	}

	void DisplayCards()
	{
		GridLayoutGroup gridLayout = EquipageCards.GetComponent<GridLayoutGroup>();
		astronauts = User.Rocket.mission.Astronauts;
		for (int i = 0; i < astronauts.Count; i++)
		{
			// Instancier une nouvelle carte d'astronaute
			if (gridLayout != null) 
			{
				GameObject astronautCard = Instantiate(astronautCardPrefab, gridLayout.transform);
				// Set the position and size of the astronaut card based on the gridLayout cell size
				

				// Remplir les informations de la carte d'astronaute
				Astronaut astronaut = astronauts[i];
				astronautCard.transform.Find("Name").GetComponent<Text>().text = astronaut.Name;
				astronautCard.transform.Find("Weight").GetComponent<Text>().text = astronaut.Weight.ToString("0.0 Kg");
				astronautCard.transform.Find("Height").GetComponent<Text>().text = astronaut.Height.ToString("0.0 M");
				
				// Créez un nouveau sprite à partir de la texture de l'astronaute
				// Assigne le nouveau sprite à l'image de la carte d'astronaute
				astronautCard.transform.Find("Picture").GetComponent<Image>().sprite = astronaut.Image;
				string flag = "Images/" + astronauts[i].Nationality;
				astronautCard.transform.Find("Flag").GetComponent<Image>().sprite = Resources.Load<Sprite>(flag);
			}
		}
	}
	
	public void OnEquipageButtonClick()
	{
		Equipage.SetActive(true);
		closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
		if(closeButton != null) closeButton.onClick.AddListener(OnCloseClick);
	}

	public void OnCloseClick() { Equipage.SetActive(false);	}
}