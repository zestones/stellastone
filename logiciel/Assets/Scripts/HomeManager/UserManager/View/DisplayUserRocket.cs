using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DisplayUserRocket : MonoBehaviour {
	
	public GameObject rocketModels;
	
	private const string ATELIER_SCENE_NAME = "AtelierScene";
	public float rotationSpeed = 150f; // Vitesse de rotation de la fusée
	
	private const int ID_DELTA_IV = 0;
	private GameObject rocketModel;
	
	void Start() 
	{
		GameObject newUserInterface = GameObject.Find("NewUserInterface");
		if (User.Rocket != null)
		{
			newUserInterface.SetActive(false);
			
			rocketModel = rocketModels.transform.GetChild(User.Rocket.Id).gameObject;
			rocketModel.SetActive(true);
		}
		else {
			newUserInterface.SetActive(true);
			Button newMissionButton = GameObject.Find("NewMissionButton").GetComponent<Button>();
			newMissionButton.onClick.AddListener(OnNewMissionClick);
		}
	}

	void Update()
	{
		if(User.Rocket != null)
		{
			if (User.Rocket.Id == ID_DELTA_IV) 
			{
				rocketModel.transform.Rotate(0f,  0f , rotationSpeed * Time.deltaTime);
			}
			else 
			{
				rocketModel.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
			}
		}
	}
	
	void OnNewMissionClick()
	{
		SceneManager.LoadSceneAsync(ATELIER_SCENE_NAME);
	}
}
