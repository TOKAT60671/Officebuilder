using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    public GameObject MakeWorldPanel;
    public GameObject LoadWoldPanel;
    public GameObject NewWorldButton;
    public int SlotIndex; // 1 to 5
    TMP_InputField WorldNameField;
    TMP_Text WorldNameText;


    public void NewWorld()
    {
        NewWorldButton.SetActive(false);
        MakeWorldPanel.SetActive(true);
    }
    public void ConfirmWorld()
    {
        MakeWorldPanel.SetActive(false);
        LoadWoldPanel.SetActive(true);
        WorldNameText.text = WorldNameField.text;
        
    }
    public void Cancel()
    {
        MakeWorldPanel.SetActive(false);
        NewWorldButton.SetActive(true);
    }
    public void DeleteWorld()
    {
        LoadWoldPanel.SetActive(false);
        NewWorldButton.SetActive(true);
    }
    public void LoadWorld()
    {
        
    }
}
