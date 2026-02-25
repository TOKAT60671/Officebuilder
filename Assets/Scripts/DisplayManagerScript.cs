using UnityEngine;
using UnityEngine.UI;

public class DisplayManagerScript : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject GameUIPanel;
    public GameObject SaveListPanel;
    public void clearScreen()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        GameUIPanel.SetActive(false);
        SaveListPanel.SetActive(false);
    }
    public void ToLogin()
    {
        clearScreen();
        loginPanel.SetActive(true);
    }
    public void ToRegister()
    {
        clearScreen();
        registerPanel.SetActive(true);
    }
    public void EnableGameUI()
    {
        clearScreen();
        GameUIPanel.SetActive(true);
    }
    public void ToSaveList()
    {
        clearScreen();
        SaveListPanel.SetActive(true);
    }
}
