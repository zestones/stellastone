using UnityEngine;
using UnityEngine.UI;

public class DisplayMissionInfos : MonoBehaviour
{
    [SerializeField] private GameObject MissionDetails;
    private const string NAME = "name";
    private const string DESCRIPTION = "description";
	private const string LAUNCHDATE = "launchDate";
	private const string DESTINATION = "destination";
    private const string DESTINATION_DISTANCE = "distance";
    private static Text nameText, descriptionText,launchDateText, destinationText, destinationDistanceText;

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

		if (User.Rocket!=null) {
        DisplayMissionDetails();
		}
		else{
			MissionDetails.SetActive(false);
		}
    }

    void DisplayMissionDetails(){
        nameText.text = User.Rocket.mission.Name;
        descriptionText.text =User.Rocket.mission.Description;
        launchDateText.text = User.Rocket.mission.LaunchDate;
        destinationText.text = User.Rocket.mission.Destination;
        destinationDistanceText.text = User.Rocket.mission.DestinationDistance.ToString("0 Km");
    }
}