using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class HomeManager : MonoBehaviour
{
	public Text usernameText;
	public GameObject home;
	public GameObject parametres;
	public InputField emailInput;
	public InputField usernameInput;
	public InputField descriptionInput;
	
	private const string ATELIER_SCENE_NAME = "AtelierScene";

	void Start()
	{   InitUser();
		home.SetActive(true);
		parametres.SetActive(false);
	}
	
	void InitUser()
	{   usernameText.text=User.GetUsername();
		emailInput.text = User.GetEmail();
		descriptionInput.text = User.GetDescription();
		usernameInput.text = User.GetUsername();
	}
	
	public void redirectParametres(){
		home.SetActive(false);
		parametres.SetActive(true);
	}
	
	public void redirectHome(){
		home.SetActive(true);
		parametres.SetActive(false);
	}

	public void redirectAtelier(){
		SceneManager.LoadScene(ATELIER_SCENE_NAME);
	}
	
	public void updateUserInfos(){
		if (usernameInput.text != User.GetUsername()){
			UpdateUsername(usernameInput.text);
		}
		if (descriptionInput.text != User.GetDescription()){
			UpdateDescription(descriptionInput.text);
		}
	}

	public void UpdateUsername(string newUsername)
	{
		User.UpdateUsername(newUsername);
	}

	public void UpdateDescription(string newDescription)
	{
		User.UpdateDescription(newDescription);
	}

	public void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}
}