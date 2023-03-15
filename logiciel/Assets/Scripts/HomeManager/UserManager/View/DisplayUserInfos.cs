using UnityEngine;
using UnityEngine.UI;

public class DisplayUserInfos : MonoBehaviour
{
	[SerializeField] private GameObject userObject;

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
}
