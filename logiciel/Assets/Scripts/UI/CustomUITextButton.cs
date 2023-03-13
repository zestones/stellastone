using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomUITextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    public GameObject objectToToggle; // GameObject à activer/désactiver
    private Text buttonText;
    private Color normalColor;
    public Color hoverColor;
    public Color selectedColor;
    public bool isHovered = false;
    public bool isSelected = false;
    public bool isInitiallySelected;
    private CustomUIButtonGroup buttonGroup;

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
        else
        {
            OnDeselect(eventData);
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
}
