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
}
