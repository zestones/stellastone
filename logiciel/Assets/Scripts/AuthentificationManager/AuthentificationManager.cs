using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthentificationManager : MonoBehaviour
{
	[Header("UI")]
	public Text messageText;
	public InputField usernameInput;
	public InputField emailInput;
	public InputField passwordInput;
	public GameObject login;
	public GameObject register;
	public Canvas loader;
	public Slider slider;

	private const string HOME_SCENE_NAME = "HomeScene";

	void Start()
	{
		User.ForgetData();
		
		login.SetActive(false);
		loader.gameObject.SetActive(false);
		messageText.text = " ";
	}
	
	// ! ----------------

	public void AlreadyMember()
	{
		register.SetActive(false);
		login.SetActive(true);
	}

	public void NotMemberYet()
	{
		register.SetActive(true);
		login.SetActive(false);
	}
	
	// ! ----------------

	
	private bool IsPasswordValid() 
	{
		
		// ********** ADD TO CONDITION **************************************************
		// string.IsNullOrWhiteSpace(passwordInput.text) || passwordInput.text.Length < 8 ||
		// 	!passwordInput.text.Any(char.IsDigit) ||
		// 	!passwordInput.text.Any(char.IsLower) ||
		// 	!passwordInput.text.Any(char.IsUpper) ||
		// 	!passwordInput.text.Any(c => !char.IsLetterOrDigit(c))
		// *****************************************************************************	
		
		if (passwordInput.text.Length < 6)
		{
			messageText.text = "Mot de passe trop faible. Il doit contenir au moins 8 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.";
			return false;
		}
		return true;
	}
	
	public async void RegisterButton()
	{
		if (!IsPasswordValid()) return;

		// Enregistrement du joueur via la méthode de l'instance User
		bool isSuccess = await PlayFabAPI.RegisterUser(usernameInput.text, emailInput.text, passwordInput.text, messageText);
		if (isSuccess) PlayFabAPI.GetUserData(() => { SceneManager.LoadSceneAsync(HOME_SCENE_NAME); });
	} 

	public async void LoginButton()
	{
		// Connexion du joueur via la méthode de l'instance User
		bool isSuccess = await PlayFabAPI.LoginUser(emailInput.text, passwordInput.text, messageText);
		if (isSuccess) PlayFabAPI.GetUserData(() => { SceneManager.LoadSceneAsync(HOME_SCENE_NAME); });	
	}

	public void ResetPasswordButton() {	PlayFabAPI.ResetPassword(emailInput.text, messageText); }
}
