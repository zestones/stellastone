using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class RocketManager : MonoBehaviour
{
    [SerializeField] private GameObject[] rocketModels;
    private Dictionary<string, Rocket> rockets;
    private int currentRocketIndex;
    public Text description;

    public GameObject modalFusee;

    private void Start()
    {
        rockets = new Dictionary<string, Rocket>();
        rockets.Add("Saturn V", new Rocket("Saturn V", "A powerful rocket used in the Apollo missions", 1000000, 100,rocketModels[0]));
        rockets.Add("Falcon Heavy", new Rocket("Falcon Heavy", "A reusable rocket developed by SpaceX", 500000, 90,rocketModels[1]));
        rockets.Add("Delta IV", new Rocket("Delta IV", "A medium-to-heavy rocket operated by the United Launch Alliance", 3000000, 110,rocketModels[2]));


        currentRocketIndex = 0;
        DisplayCurrentRocketModel();
    }

    private void DisplayCurrentRocketModel()
    {
        foreach (Rocket rocket in rockets.Values)
        {
            rocket.Model.SetActive(false);
        }

        Rocket currentRocket = rockets.Values.ToList()[currentRocketIndex];
        currentRocket.Model.SetActive(true);
        description.text = currentRocket.DisplayInfo();
    }

    public void OnClickChange()
    {
        currentRocketIndex++;
        if (currentRocketIndex >= rockets.Count)
        {
            currentRocketIndex = 0;
        }

        DisplayCurrentRocketModel();
    }

    public void OnClickShowModal(){
        modalFusee.SetActive(true); 
    }
    public void OnClickCloseModal(){
        modalFusee.SetActive(false); 
    }

    public void redirectHome(){
        SceneManager.LoadScene("Home");
    }
    
}
  
