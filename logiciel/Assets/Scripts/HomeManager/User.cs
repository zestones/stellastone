using PlayFab;
using PlayFab.ClientModels;
using System;

public static class User
{
    private static string username;
    private static string email;
    private static string password;

    static User()
    {
        // Initialisation des propriétés
        username = string.Empty;
        email = string.Empty;
        password = string.Empty;
    }

    public static string GetUsername()
    {
        return username;
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
            User.password = password;

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
        PlayFabClientAPI.SendAccountRecoveryEmail(request, onSuccess, onError);
    }
}
