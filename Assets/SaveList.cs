using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveList : MonoBehaviour
{
    public APIClient APIClient;


    public SaveSlot SaveSlot1;
    public SaveSlot SaveSlot2;
    public SaveSlot SaveSlot3;
    public SaveSlot SaveSlot4;
    public SaveSlot SaveSlot5;

    DisplayManagerScript DisplayManager;

    public async void UpdateSaveList()
    {
        IWebRequestReponse webRequestResponse = await APIClient.GetWorlds();

        switch (webRequestResponse)
        {
            case WebRequestData<List<APIWorld>> dataResponse:
                List<APIWorld> worlds = dataResponse.Data;
                foreach (APIWorld world in worlds)
                {
                    switch(world.SaveSlotIndex)
                    {
                        case 1:
                            SaveSlot1.SetCurrentWorld(world);
                            Debug.Log("Added world with id: " + world.Id + " to save slot 1");
                            break;
                        case 2:
                            SaveSlot2.SetCurrentWorld(world);
                            Debug.Log("Added world with id: " + world.Id + " to save slot 2");
                            break;
                        case 3:
                            SaveSlot3.SetCurrentWorld(world);
                            Debug.Log("Added world with id: " + world.Id + " to save slot 3");
                            break;
                        case 4:
                            SaveSlot4.SetCurrentWorld(world);
                            Debug.Log("Added world with id: " + world.Id + " to save slot 4");
                            break;
                        case 5:
                            SaveSlot5.SetCurrentWorld(world);
                            Debug.Log("Added world with id: " + world.Id + " to save slot 5");
                            break;
                        default:
                            Debug.Log("Invalid save slot index: " + world.SaveSlotIndex + " for world with id: " + world.Id);
                            break;
                    }
                }
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read Worlds error: " + errorMessage);
                DisplayManager.OpenErrorMessage(errorMessage);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

}
