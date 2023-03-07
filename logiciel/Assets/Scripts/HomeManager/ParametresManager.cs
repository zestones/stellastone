using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab.CloudScriptModels;
using System.IO;

public class ParametresManager : MonoBehaviour
{
    public GameObject motdepasse;
    public GameObject donnees;
    public GameObject infosUtilisateur;
    public Text messageText;

    public void ClickInfos()
    {
        motdepasse.SetActive(false);
        donnees.SetActive(false);
        infosUtilisateur.SetActive(true);
    }
    public void ClickMdp()
    {
        motdepasse.SetActive(true);
        donnees.SetActive(false);
        infosUtilisateur.SetActive(false);
    }

    public void ClickDonnees()
    {
        motdepasse.SetActive(false);
        donnees.SetActive(true);
        infosUtilisateur.SetActive(false);
    }

   
    public void ResetPassword(){
        string email=User.GetEmail();
        User.ResetPassword(email,OnPasswordResetSuccess,OnError);
    }

    void OnPasswordResetSuccess(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Email de réinitialisation du mot de passe envoyé.";
    }

    private void OnError(PlayFabError error){
        messageText.text = error.GenerateErrorReport();
    }

}
