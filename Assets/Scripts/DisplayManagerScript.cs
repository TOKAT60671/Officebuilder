using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayManagerScript : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject GameUIPanel;
    public GameObject SaveListPanel;
    public GameObject PauseMenu;
    public GameObject ErrorPanel;
    [SerializeField] private TMP_Text ErrorPanelText;

    public SaveList SaveList;

    public void clearScreen()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        GameUIPanel.SetActive(false);
        SaveListPanel.SetActive(false);
        PauseMenu.SetActive(false);
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
    public void EnablePauseMenu()
    {
        clearScreen();
        PauseMenu.SetActive(true);

    }
    public void ToSaveList()
    {
        clearScreen();
        SaveListPanel.SetActive(true);
        SaveList.UpdateSaveList();
    }
    public void OpenErrorMessage(string Message)
    {
        ErrorPanel.SetActive(true);
        ErrorPanelText.text = Message;
    }
    public void CloseErrorMessage()
    {
        ErrorPanel.SetActive(false);
        ErrorPanelText.text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi cursus.";
    }
}
