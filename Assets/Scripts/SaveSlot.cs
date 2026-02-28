using Newtonsoft.Json;
using System;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public GameObject MakeWorldPanel;
    public GameObject LoadWoldPanel;
    public GameObject NewWorldButton;
    public GameObject SizeWorldPanel;
    public int SlotIndex; // 1 to 5
    TMP_InputField WorldNameField;
    TMP_Text WorldNameText;
    TMP_InputField WorldWidthField;
    TMP_InputField WorldHeightField;

    DisplayManagerScript DisplayManager;
    WebClient webClient;


    public void NewWorld()
    {
        NewWorldButton.SetActive(false);
        MakeWorldPanel.SetActive(true);
    }
    public void ConfirmWorld()
    {
        //H=10-100 W=20-200
        if (Regex.IsMatch(WorldWidthField.text, @"^([2-9][0-9]|1[0-9]{2}|200)$") && Regex.IsMatch(WorldHeightField.text, @"^([1-9][0-9]|100)$"))
        {
            new APIWorld(WorldNameField.text, int.Parse(WorldWidthField.text), int.Parse(WorldHeightField.text ));
        }
        

    }
    public void ConfirmName()
    {
        if (WorldNameField.text.Count() > 25)
        {
            DisplayManager.OpenErrorMessage("World name must be less than 20 characters");
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
    public void DeleteWorld()
    {
        LoadWoldPanel.SetActive(false);
        NewWorldButton.SetActive(true);
    }
    public void LoadWorld()
    {
        
    }

    // API part
    public async Awaitable<IWebRequestReponse> CreateEnvironment(APIWorld world)
    {
        string route = "/environments";
        string data = JsonConvert.SerializeObject(environment, JsonHelper.CamelCaseSettings);

        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
        return ParseEnvironment2DResponse(webRequestResponse);
    }
}
