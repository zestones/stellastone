using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public static class PlayFabAPI
{
	public static async Task<bool> RegisterUser(string username, string email, string password, Text messageText)
	{
		var registerSuccess = new TaskCompletionSource<bool>();
		var request = new RegisterPlayFabUserRequest
		{
			DisplayName = username,
			Username = username,
			Email = email,
			Password = password
		};
		PlayFabClientAPI.RegisterPlayFabUser(request, 
		result => { OnRegisterSuccess(result, messageText); registerSuccess.SetResult(true); },
		error => { OnError(error); registerSuccess.SetResult(false); });
		
		return await registerSuccess.Task;
	}

	public static async Task<bool> LoginUser(string email, string password, Text messageText)
	{
		var loginSuccess = new TaskCompletionSource<bool>();
		var request = new LoginWithEmailAddressRequest
		{
			Email = email,
			Password = password
		};
		PlayFabClientAPI.LoginWithEmailAddress(request, 
		result => { OnLoginSuccess(result, messageText, email); loginSuccess.SetResult(true); },
		error => { OnError(error); loginSuccess.SetResult(false); });
		
		return await loginSuccess.Task;
	}


	public static void ResetPassword(string email, Text messageText)
	{
		var request = new SendAccountRecoveryEmailRequest
		{
			Email = email,
			TitleId = "CD8AB"
		};
		PlayFabClientAPI.SendAccountRecoveryEmail(request,
			result =>{ OnResetPasswordSuccess(result, messageText); },
			error => { OnError(error);}
		);
	}

	public static void UpdateUsername(string newUsername)
	{   
		User.Username = newUsername;
		var request = new UpdateUserTitleDisplayNameRequest { DisplayName = newUsername };
		PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUsernameSuccess, OnError);
	}

	public static void UpdateDescription(string newDescription)
	{   
		User.Description = newDescription;
		// Met à jour les données utilisateur avec une nouvelle description
		var request = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{ "description", newDescription }
			}
		};
		PlayFabClientAPI.UpdateUserData(request, OnUpdateDescriptionSuccess, OnUpdateFailure);
	}
	
	public static void UpdateAvatar(string imgPath) 
	{
		// Envoie l'image sélectionnée à PlayFab
		PlayFabClientAPI.UpdateAvatarUrl(new UpdateAvatarUrlRequest
		{
			ImageUrl = imgPath
		}, OnUpdateAvatarUrlSuccess, OnUpdateAvatarUrlFailure);
	}
	
	
	// ======= GET USER DATA ============
	
	public static void GetUserData(Action onUserDataRetrieved)
	{
		GetUserAvatarAndUsername(User.Id, () =>
		{
			GetUserDescription(() => { onUserDataRetrieved?.Invoke(); });
		});
	}

	private static void GetUserAvatarAndUsername(string userId, Action onAvatarAndUsernameRetrieved)
	{   //récupération de l'avatar de l'utilisateur de l'utilisateur
		PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
		{
			PlayFabId = userId,
			ProfileConstraints = new PlayerProfileViewConstraints
			{
				ShowAvatarUrl = true,
				ShowDisplayName = true
			}
		}, 
			result => { OnGetAvatarAndUsernameSuccess(result); onAvatarAndUsernameRetrieved?.Invoke(); }, 
			error => { OnError(error); onAvatarAndUsernameRetrieved?.Invoke(); }
		);
	}

	private static void GetUserDescription(Action onDescriptionRetrieved){
		PlayFabClientAPI.GetUserData(new GetUserDataRequest()
		{
			Keys = new List<string>() { "description" }
		}, 
		result => { OnGetDescriptionSuccess(result); onDescriptionRetrieved?.Invoke(); },
		error => { OnError(error); onDescriptionRetrieved?.Invoke(); });
	}	
			
	// ------------------
	
	private static void OnGetAvatarAndUsernameSuccess(GetPlayerProfileResult result) 
	{
		string avatarPath = result.PlayerProfile.AvatarUrl;
		if (result.PlayerProfile != null && !string.IsNullOrEmpty(avatarPath))
		{
			User.AvatarUrl = avatarPath;
			User.Avatar = UserInfosController.LoadAvatarFromUrl(avatarPath);
			User.Username = result.PlayerProfile.DisplayName;
		}
		
		else User.Username = result.PlayerProfile.DisplayName;
	}
	

	private static void OnGetDescriptionSuccess(GetUserDataResult result) 
	{
		if (result.Data.TryGetValue("description", out PlayFab.ClientModels.UserDataRecord descriptionOut))
		{
			User.Description = descriptionOut.Value;
		}
		else User.Description = " ";
	}
	
	// ===============================
		
	private static void OnLoginSuccess(LoginResult result, Text messageText, string email)
	{   
		messageText.text = "Connexion réussie.";
		User.Id = result.PlayFabId;
		User.Email = email;
	}
	
	// ------------------
	
	private static void OnRegisterSuccess(RegisterPlayFabUserResult result, Text messageText)
	{
		messageText.text = "Votre compte a été crée avec succès";
	}
			
	private static void OnResetPasswordSuccess(SendAccountRecoveryEmailResult result, Text messageText)
	{
		Debug.Log("Password recovery mail sent successfully");
		messageText.text = "Password recovery mail sent successfully";
	}
	
	private static void OnUpdateUsernameSuccess(UpdateUserTitleDisplayNameResult result)
	{
		Debug.Log("Updated username successfully");
	}
	
	private static void OnUpdateDescriptionSuccess(UpdateUserDataResult result)
	{
		Debug.Log("Updated description successfully");
	}
	
	private static void OnUpdateFailure(PlayFabError error)
	{
		Debug.LogError("Failed to update : " + error.ErrorMessage);
	}
	
	private static void OnUpdateAvatarUrlSuccess(EmptyResponse response)
	{   
		Debug.Log("Avatar URL updated successfully");   
	}   

	private static void OnUpdateAvatarUrlFailure(PlayFabError error)
	{
		Debug.LogError("Failed to update avatar URL: " + error.ErrorMessage);
	}
	
	static void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	} 
}
