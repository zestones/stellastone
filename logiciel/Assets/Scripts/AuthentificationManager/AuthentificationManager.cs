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


    void Start()
    {
        login.SetActive(false);
        messageText.text = " ";
    }

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

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Mot de passe trop court";
            return;
        }

        // Enregistrement du joueur via la méthode de l'instance User
        User.RegisterUser(usernameInput.text, emailInput.text, passwordInput.text, OnRegisterSuccess, OnError);
    }

    public void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text="Votre compte a été crée avec succès";
    }
   

    public void LoginButton()
    {
        // Connexion du joueur via la méthode de l'instance User
        User.LoginUser(emailInput.text, passwordInput.text, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {   
        messageText.text = "Connexion réussie.";
        Debug.Log("Connexion réussie.");
        initUser();
        SceneManager.LoadScene("Home");
    }

    public void ResetPasswordButton()
    {
        // Réinitialisation du mot de passe via la méthode de l'instance User
        User.ResetPassword(emailInput.text, OnPasswordResetSuccess, OnError);
    }

    void OnPasswordResetSuccess(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Email de réinitialisation du mot de passe envoyé.";
    }
    

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void initUser()
    {   // Initialisation des propriétés de la classe statique User avant de passer  à la scène suivante
        if (PlayFabClientAPI.IsClientLoggedIn()){
        //récupération de l'id, username et email de l'utilisateur
        InitInfos();
        //récupération de la description de l'utilisateur
        InitDescription();
        //récupération de l'avatar de l'utilisateur de l'utilisateur
        InitAvatarUrl(User.id);
        }
    }
    private static void InitInfos(){
        //récupération de l'id, username et email de l'utilisateur
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            Debug.Log("GetAccountInfoSuccess");
            User.id = result.AccountInfo.PlayFabId;
            User.username = result.AccountInfo.Username;
            User.email = result.AccountInfo.PrivateInfo.Email;
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    private static void InitDescription(){
        //récupération de la description de l'utilisateur
         PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { "description" }
        }, result => {
             if (result.Data.TryGetValue("description", out PlayFab.ClientModels.UserDataRecord descriptionOut))
            {
                User.description = descriptionOut.Value;
                Debug.Log("Description: " + User.description);
            }
            else
            {   User.description = " ";
                Debug.Log("Description not found.");
            }
        }
        , error => Debug.LogError(error.GenerateErrorReport()));
    }
    private static void InitAvatarUrl(string playFabId)
    {   //récupération de l'avatar de l'utilisateur de l'utilisateur
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowAvatarUrl = true
            }
        }, result =>
        {
            if (result.PlayerProfile != null && !string.IsNullOrEmpty(result.PlayerProfile.AvatarUrl))
            {
                User.avatarUrl = result.PlayerProfile.AvatarUrl;
                Debug.Log("On a l'avatar URL !" + User.avatarUrl);
            }
            else
            {
               User.avatarUrl = " ";
            }
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

}
