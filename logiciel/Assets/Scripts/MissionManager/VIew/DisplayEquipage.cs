using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEquipage : MonoBehaviour 
{   public List<Astronaut> astronauts;
    public GameObject astronautCardPrefab; // le préfab de la carte d'astronaute
    public GameObject Equipage;
    public GameObject EquipageCards;
    private Button equipageButton,closeButton;
    void Start(){
        Equipage.SetActive(false);
        equipageButton = GameObject.Find("EquipageButton").GetComponent<Button>();
        equipageButton.onClick.AddListener(OnEquipageButtonClick);
        DisplayCards();
    }

    void DisplayCards(){
        GridLayoutGroup gridLayout = EquipageCards.GetComponent<GridLayoutGroup>();
        astronauts = User.Rocket.mission.Astronauts;
        for (int i = 0; i < astronauts.Count; i++)
            {
                // Instancier une nouvelle carte d'astronaute
                if (gridLayout != null) {
                GameObject astronautCard = Instantiate(astronautCardPrefab, gridLayout.transform);
                 // Set the position and size of the astronaut card based on the gridLayout cell size
                

                // Remplir les informations de la carte d'astronaute
                Astronaut astronaut = astronauts[i];
                astronautCard.transform.Find("Name").GetComponent<Text>().text = astronaut.Name;
                astronautCard.transform.Find("Weight").GetComponent<Text>().text = astronaut.Weight.ToString();
                astronautCard.transform.Find("Height").GetComponent<Text>().text = astronaut.Height.ToString();
                // Créez un nouveau sprite à partir de la texture de l'astronaute
                // Assigne le nouveau sprite à l'image de la carte d'astronaute
                Debug.Log(User.Rocket.mission.Astronauts[0].Image);
                astronautCard.transform.Find("Picture").GetComponent<Image>().sprite = astronaut.Image;
                }
                
            }
    }
    public void OnEquipageButtonClick(){
        Equipage.SetActive(true);
        closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        if(closeButton != null) {
        closeButton.onClick.AddListener(OnCloseClick);
        }
    }

    public void OnCloseClick(){
        Equipage.SetActive(false);
    }
}