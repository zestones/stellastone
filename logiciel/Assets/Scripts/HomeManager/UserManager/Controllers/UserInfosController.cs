using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab.CloudScriptModels;
using System.IO;

public class UserInfosController : MonoBehaviour
{
	public Text messageText;	
	public InputField usernameInput, descriptionInput;
	
	private static string localImagePath;
	private const string PROFIL_SCENE_NAME = "ProfilScene";
	private Sprite tmpAvatar;
	
	public void ResetPassword()
	{
		Debug.Log("Reset password");
		Debug.Log(User.Email);
		PlayFabAPI.ResetPassword(User.Email, messageText);
	}
	
	public void UpdateUserInfos()
	{	
		if (usernameInput.text != User.Username)
		{
			PlayFabAPI.UpdateUsername(usernameInput.text);
		}
		
		if (descriptionInput.text != User.Description)
		{
			Debug.Log("Description updated");
			Debug.Log(descriptionInput.text);
			PlayFabAPI.UpdateDescription(descriptionInput.text);
		}
		
		if (localImagePath != null && localImagePath != User.AvatarUrl) 
		{
			UpdateAvatar();
		}
		
		SceneManager.LoadSceneAsync(PROFIL_SCENE_NAME);
	}

	public void OpenWindowChangeAvatar()
	{
		#if UNITY_EDITOR
			string filePath = UnityEditor.EditorUtility.OpenFilePanel("Select image file", "", "png, jpg, jpeg");
		

			if (!string.IsNullOrEmpty(filePath))
			{
				// Stocke le chemin d'accès local du fichier sélectionné
				localImagePath = filePath;
				// Affiche l'image sélectionnée
				tmpAvatar = LoadAvatarFromUrl(localImagePath);
				DisplayUserInfos.SetUserInfos(tmpAvatar);
			}
		#endif
	}
	
	public void UpdateAvatar()
	{
		if (string.IsNullOrEmpty(localImagePath))
		{
			Debug.Log("No file selected");
			return;
		}

		// On update l'Utilisateur
		User.AvatarUrl = localImagePath;
		User.Avatar = tmpAvatar;
		
		PlayFabAPI.UpdateAvatar(User.AvatarUrl);
	}
	
	// ! ---- READ IMG AND TRANSFORM IT TO SPRITE ----
	public static Sprite LoadAvatarFromUrl(string avatarPath)
	{
		// Lecture des données de l'image à partir de l'URL
		byte[] imageData = File.ReadAllBytes(avatarPath);

		// Création de la texture à partir des données de l'image
		Texture2D texture = new Texture2D(1, 1);
		texture.LoadImage(imageData);

		// Création du sprite à partir de la texture
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
	}
}