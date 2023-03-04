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
    private bool isLoading = false;
    public Canvas loader;
    public Slider slider;

    void Start()
    {
        login.SetActive(false);
        loader.gameObject.SetActive(false);
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
        loader.gameObject.SetActive(true);
        initUser();
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
    {   
        StartCoroutine(GetUserData());
    }
    IEnumerator GetUserData()
    {   // Initialisation des propriétés de la classe statique User avant de passer  à la scène suivante
        //récupération de l'id, username et email de l'utilisateur
        isLoading = true;
        InitInfos();
        yield return new WaitUntil(() => slider.value >= 0.33f );
        //récupération de la description de l'utilisateur
        InitDescription();
        yield return new WaitUntil(() => slider.value >= 0.67f );
        //récupération de l'avatar de l'utilisateur de l'utilisateur
        InitAvatarUrl(User.id);
        // Charger la scène Home
        yield return new WaitUntil(() => isLoading == false);
        SceneManager.LoadSceneAsync("Home");
    }
    private void InitInfos(){
        //récupération de l'id, username et email de l'utilisateur
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            Debug.Log("GetAccountInfoSuccess");
            User.id = result.AccountInfo.PlayFabId;
            User.username = result.AccountInfo.Username;
            User.email = result.AccountInfo.PrivateInfo.Email;
        }, error => Debug.LogError(error.GenerateErrorReport()));
        slider.value = 0.33f;
    }
    private void InitDescription(){
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
        slider.value = 0.67f;
    }
    private void InitAvatarUrl(string playFabId)
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
            isLoading = false;
        }, error => Debug.LogError(error.GenerateErrorReport()));
        slider.value = 1.0f;
    }

}
