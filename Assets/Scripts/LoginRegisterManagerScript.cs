using UnityEngine;
public class LoginRegisterManagerScript : MonoBehaviour
{
    public async void Login()
    {

        FindFirstObjectByType<DisplayManagerScript>().ToLogin();
    }

    public async void Register()
    {

        FindFirstObjectByType<DisplayManagerScript>().ToRegister();
    }
    public async void Logout()
    {

        FindFirstObjectByType<DisplayManagerScript>().ToLogin();
    }
}
/*
{
  "email": "Test@Email.com",
  "password": "AbsoluteTesting123!"
}
*/
