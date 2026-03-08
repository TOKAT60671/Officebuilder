using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] GameObject MakeWorldPanel;
    [SerializeField] GameObject LoadWoldPanel;
    [SerializeField] GameObject NewWorldButton;
    [SerializeField] GameObject SizeWorldPanel;
    [SerializeField] int SlotIndex; // 1 to 5
    [SerializeField] TMP_InputField WorldNameField;
    [SerializeField] TMP_Text WorldNameText;
    [SerializeField] TMP_InputField WorldWidthField;
    [SerializeField] TMP_InputField WorldHeightField;

    public APIWorld currentWorld;

    public DisplayManagerScript DisplayManager;
    public ObjectManager ObjectManager;
    public APIClient APIClient;
    public SaveList SaveList;


    public void NewWorld()
    {
        NewWorldButton.SetActive(false);
        MakeWorldPanel.SetActive(true);
    }
    public async void ConfirmWorld()
    {
        //H=10-100 W=20-200
        if (Regex.IsMatch(WorldWidthField.text, @"^([2-9][0-9]|1[0-9]{2}|200)$"))
        {
            if (Regex.IsMatch(WorldHeightField.text, @"^([1-9][0-9]|100)$"))
            {
                APIWorld world = new APIWorld{Id = Guid.NewGuid(), Name = WorldNameField.text, Width = int.Parse(WorldWidthField.text), Height = int.Parse(WorldHeightField.text), SaveSlotIndex = SlotIndex, UserId = ""};
                IWebRequestReponse webRequestResponse = await APIClient.CreateWorld(world);
                switch (webRequestResponse)
                {
                    case WebRequestData<string> dataResponse:
                        Debug.Log("World Created SuccesFully");
                        SizeWorldPanel.SetActive(false);
                        LoadWoldPanel.SetActive(true);
                        SaveList.UpdateSaveList();
                        break;
                    case WebRequestError errorResponse:
                        string errorMessage = errorResponse.ErrorMessage;
                        Debug.Log("Save creation error: " + errorMessage);
                        DisplayManager.OpenErrorMessage(errorMessage);
                        break;
                    default:
                        throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
                }
            }
            else
            {
                DisplayManager.OpenErrorMessage("World height must be between 10 and 100");
            }
        }
        else
        {
            DisplayManager.OpenErrorMessage("World width must be between 20 and 200");
        }


    }
    public void ConfirmName()
    {
        if (WorldNameField.text.Count() > 25)
        {
            DisplayManager.OpenErrorMessage("World name must be less than 25 characters");
        }
        else if (WorldNameField.text.Count() < 1)
        {
            DisplayManager.OpenErrorMessage("world name must be more than 1 characters");
        }
        else
        {
            WorldNameText.text = WorldNameField.text;
            MakeWorldPanel.SetActive(false);
            SizeWorldPanel.SetActive(true);
        }
    }
    public void BackToNaming()
    {
        SizeWorldPanel.SetActive(false);
        MakeWorldPanel.SetActive(true);

    }
    public void Cancel()
    {
        MakeWorldPanel.SetActive(false);
        NewWorldButton.SetActive(true);
    }
    public async void DeleteWorld()
    {
        IWebRequestReponse webRequestResponse = await APIClient.DeleteWorld(currentWorld.Id);
        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("World Deleted SuccesFully");
                LoadWoldPanel.SetActive(false);
                NewWorldButton.SetActive(true);
                SaveList.UpdateSaveList();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Save deletion error: " + errorMessage);
                DisplayManager.OpenErrorMessage(errorMessage);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }

    }
    public async void LoadWorld()
    {
        ObjectManager.currentWorld = currentWorld;
        await ObjectManager.LoadGame(currentWorld);
    }
    public void SetCurrentWorld(APIWorld world)
    {
        currentWorld = world;
        WorldNameText.text = world.Name;
        LoadWoldPanel.SetActive(true);
        NewWorldButton.SetActive(false);
    }
}