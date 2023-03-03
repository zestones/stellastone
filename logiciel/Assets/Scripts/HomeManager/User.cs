using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public static class User
{
    public static string id;
    public static string username;
    public static string email;
    public static string description;
    public static string avatarUrl;

    public static string GetId()
    {
        return id;
    }

    public static string GetUsername()
    {
        return username;
    }

    public static string GetEmail()
    {
        return email;
    }


    public static string GetDescription()
    {
        return description;
    }

    public static string GetAvatarUrl()
    {
        return avatarUrl;
    }

    // Enregistrement du joueur
    public static void RegisterUser(string username, string email, string password, Action<RegisterPlayFabUserResult> onSuccess, Action<PlayFabError> onError)
    {
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = username,
            Username = username,
            Email = email,
            Password = password
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, onSuccess, onError);
    }

    // Connexion du joueur
    public static void LoginUser(string email, string password, Action<LoginResult> onSuccess, Action<PlayFabError> onError)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, result =>
        {
            // Stockage des informations du joueur
            User.email = email;
            onSuccess(result);
        }, onError);
    }

    public static void ResetPassword(string email, Action<SendAccountRecoveryEmailResult> onSuccess, Action<PlayFabError> onError)
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = "CD8AB"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnResetPasswordSuccess, OnError);
    }

     public static void UpdateUsername(string newUsername)
    {   username = newUsername;
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = newUsername };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUsernameSuccess, OnError);
    }
    public static void UpdateDescription(string newDescription)
    {   description = newDescription;
        // Met à jour les données utilisateur avec une nouvelle description
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "description", newDescription }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
    }
    private static void OnUpdateUsernameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated description successfully");
    }
    private static void OnResetPasswordSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Password recovery mail sent successfully");
    }
    private static void OnUpdateSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Updated description successfully");
    }
    private static void OnUpdateFailure(PlayFabError error)
    {
        Debug.LogError("Failed to update : " + error.ErrorMessage);
    }
    static void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
