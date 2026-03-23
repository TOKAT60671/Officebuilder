using System;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
public class LoginRegisterManagerScript : MonoBehaviour
{
    public APIClient APIClient;
    public TMP_InputField LoginEmail;
    public TMP_InputField LoginPassword;
    public TMP_InputField RegisterEmail;
    public TMP_InputField RegisterPassword;
    public DisplayManagerScript DisplayManager;
    public async void Login()
    {
        User user = new User {Email = LoginEmail.text, Password = LoginPassword.text };
        IWebRequestReponse webRequestResponse = await APIClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");
                DisplayManager.ToSaveList();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);
                if (errorMessage == "HTTP/1.1 401 Unauthorized")
                {
                    DisplayManager.OpenErrorMessage("Email of wachtwoord is incorrect");
                }
                else
                {
                    DisplayManager.OpenErrorMessage(errorMessage);
                }
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
    public async void Register()
    {
        User user = new User {Email = RegisterEmail.text, Password = RegisterPassword.text };
        IWebRequestReponse webRequestResponse = await APIClient.Register(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                DisplayManager.ToLogin();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                if (errorMessage == "HTTP/1.1 400 Bad Request")
                {
                    DisplayManager.OpenErrorMessage("Email of wachtwoord voldoet niet aan de eisen");
                }
                else
                {
                    DisplayManager.OpenErrorMessage(errorMessage);
                }
                Debug.Log("Register error: " + errorMessage);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
    public void Logout()
    {

        DisplayManager.ToLogin();
    }
}
