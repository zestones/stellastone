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
    public Text messageText;
   
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
