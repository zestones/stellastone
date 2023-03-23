using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DisplayMissionInfos : MonoBehaviour
{
	[SerializeField] private GameObject MissionDetails;
	private const string NAME = "name";
	private const string DESCRIPTION = "description";
	private const string LAUNCHDATE = "launchDate";
	private const string DESTINATION = "destination";
	private const string DESTINATION_DISTANCE = "distance";
	private static Text nameText, descriptionText,launchDateText, destinationText, destinationDistanceText;

	public GameObject LaunchMissionButton;
	private const string GAME_SCENE_NAME = "GameScene";
	
	void Start()
	{
		Transform[] children = MissionDetails.GetComponentsInChildren<Transform>(true);
		
		// Cherche les enfants avec les noms spécifiques
		foreach (Transform child in children)
		{
			if (child.name == NAME)
			{
				nameText = child.GetComponent<Text>();
			}
			else if (child.name == LAUNCHDATE)
			{
				launchDateText = child.GetComponent<Text>();
			}
			else if (child.name == DESTINATION)
			{
				destinationText = child.GetComponent<Text>();
			}
			else if (child.name == DESCRIPTION)
			{
				descriptionText = child.GetComponent<Text>();
			}
			else if (child.name == DESTINATION_DISTANCE)
			{
				destinationDistanceText = child.GetComponent<Text>();
			}
		}

		if (User.Rocket != null ) 
		{
			DisplayMissionDetails();
			LaunchMissionButton.SetActive(true);
		} 
			
		else 
		{
			MissionDetails.SetActive(false);
			LaunchMissionButton.SetActive(false);
		}
	}

	void DisplayMissionDetails()
	{
		nameText.text = User.Rocket.mission.Name;
		descriptionText.text =User.Rocket.mission.Description;
		launchDateText.text = User.Rocket.mission.LaunchDate;
		destinationText.text = User.Rocket.mission.Destination;
		destinationDistanceText.text = User.Rocket.mission.DestinationDistance.ToString("0 Km");
	}
	
	public void LaunchMissionButtonEvent() 
	{
		SceneManager.LoadScene(GAME_SCENE_NAME);
	}
}