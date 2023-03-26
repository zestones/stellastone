using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectModal : MonoBehaviour
{
	public GameObject Modal;
	
	void Start()
	{
		Modal.SetActive(false);
		
		// Add a listener to the close button
		Button closeButton = Modal.transform.Find("CloseButton").GetComponent<Button>();
		closeButton.onClick.AddListener(() => {
			Modal.SetActive(false);
		});
	}
	
	public void ShowModal(GameObject obj)
	{
		Modal.SetActive(true);

		// Display the name of the GameObject
		Text title = Modal.transform.Find("Title").GetComponent<Text>();
		title.text = obj.name;

		Text description = Modal.transform.Find("Description").GetComponent<Text>();

		if (obj.GetComponent<Text>() != null)
		{
			string text = obj.GetComponent<Text>().text;
			if (text != null) { description.text = text; }
			else { description.text = "Description du GameObject a afficher"; }
		}
	}
}
