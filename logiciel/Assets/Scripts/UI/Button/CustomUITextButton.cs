using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CustomUITextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
	public Color hoverColor;
	public Color selectedColor;
	public GameObject objectToToggle; // GameObject à activer/désactiver
	public bool isInitiallySelected;
	
	public string sceneName;
	
	private Text buttonText;
	private Color normalColor;
	
	private bool isHovered = false;
	private bool isSelected = false;
		
	private CustomUIButtonGroup buttonGroup;

	public bool IsHovered { get { return isHovered; } }
	public bool IsSelected
	{
		get { return isSelected; }
		set { isSelected = value; }
	}

	private void Start()
	{
		buttonText = GetComponentInChildren<Text>();
		normalColor = buttonText.color;
		buttonGroup = GetComponentInParent<CustomUIButtonGroup>();
		
		if (isInitiallySelected)
		{
			OnSelect(null);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isSelected) // Ne modifiez pas la couleur si le bouton est sélectionné
		{
			buttonText.color = hoverColor;
			isHovered = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isSelected) // Ne modifiez pas la couleur si le bouton est sélectionné
		{
			buttonText.color = normalColor;
			isHovered = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!isSelected)
		{
			OnSelect(eventData);
			if (buttonGroup != null)
			{
				buttonGroup.OnButtonSelect(this);
			}
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		buttonText.color = selectedColor;
		isSelected = true;
		isHovered = false;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		buttonText.color = normalColor;
		isSelected = false;
		isHovered = false;
	}
	
	public void ChangeScene() {
        SceneManager.LoadScene(sceneName);
    }
}
