using UnityEngine;
using UnityEngine.EventSystems;

public class CustomUIButtonGroup : MonoBehaviour
{
    private CustomUITextButton[] buttons;

    private void Start()
    {
        // récupère tous les scripts CustomUITextButton attachés aux boutons enfants
        buttons = GetComponentsInChildren<CustomUITextButton>();
    }

    public void OnButtonSelect(CustomUITextButton selectedButton)
    {
        foreach (CustomUITextButton button in buttons)
        {
            if (button != selectedButton)
            {
                button.isSelected = false;
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
            }
        }
    }
}
