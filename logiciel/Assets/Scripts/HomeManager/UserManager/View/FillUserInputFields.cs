using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillUserInputFields : MonoBehaviour
{
	
	public InputField emailInput;
	public InputField usernameInput;
	public InputField descriptionInput;
	
	// Start is called before the first frame update
	void Start()
	{
		emailInput.text = User.Email;
		descriptionInput.text = User.Description;
		usernameInput.text = User.Username;
	}
}
