using UnityEngine;
using UnityEngine.EventSystems;

public class CustomUIButtonGroup : MonoBehaviour
{
	private CustomUITextButton[] textButtons;

	private void Start()
	{
		// récupère tous les scripts CustomUITextButton attachés aux boutons enfants
		textButtons = GetComponentsInChildren<CustomUITextButton>();
	}

	public void OnButtonSelect(CustomUITextButton selectedButton)
	{
		foreach (CustomUITextButton button in textButtons)
		{
			if (button != selectedButton)
			{
				button.IsSelected = false;
				button.OnDeselect(null); // appelle la méthode OnDeselectHandler pour réinitialiser la couleur du texte
				if (button.objectToToggle != null)
				{
					button.objectToToggle.SetActive(false);
				}
			}
			else {
				if (button.objectToToggle != null)
				{
					button.objectToToggle.SetActive(true);
				}
				else if (button.sceneName != null) 
				{
					button.ChangeScene();
				}
			}
		}
	}
}
