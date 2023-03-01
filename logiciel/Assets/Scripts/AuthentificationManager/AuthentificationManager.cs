using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 

public class AuthentificationManager : MonoBehaviour
{   [Header("UI")]
    public Text messageText;
    public InputField usernameInput;
    public InputField emailInput;
    public InputField passwordInput;
    public GameObject login;
    public GameObject register;
  

    void Start(){
        login.SetActive(false);
        messageText.text=" ";
    }

    public void AlreadyMember(){
        register.SetActive(false);
        login.SetActive(true);
    }
    public void NotMemberYet(){
        register.SetActive(true);
        login.SetActive(false);
    }

    public void RegisterButton(){
        if(passwordInput.text.Length < 6){
            messageText.text="Mot de passe trop court";
            return;
        }
        var request = new RegisterPlayFabUserRequest {
            DisplayName = usernameInput.text,
            Username = usernameInput.text,
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    public void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text="Votre compte a été crée avec succès";
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void ResetPasswordButton()
    { 
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = "CD8AB"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result){
        messageText.text="Email de réinitialisation du mot de passe envoyé.";
    }
    void OnLoginSuccess(LoginResult result){
        messageText.text = "Connexion réussie.";
        Debug.Log("Connexion réussie.");
        SceneManager.LoadScene("Home");
    }


    void OnError(PlayFabError error){
        messageText.text=error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

}
