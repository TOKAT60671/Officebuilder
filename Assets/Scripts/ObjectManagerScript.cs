using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.LightTransport;


public class ObjectManager : MonoBehaviour
{
    public GridManagerScript GridManager;
    public DisplayManagerScript DisplayManager;
    public APIClient APIClient;


    [SerializeField] PlaceableObject Desk; //0
    [SerializeField] PlaceableObject BlackLaptop; //1
    [SerializeField] PlaceableObject WhiteLaptop; //2

    public APIWorld currentWorld;

    public void LoadObjectsFromAPI(List<APIObject> objects)
    {
        foreach (APIObject obj in objects)
        {
            switch(obj.Type)
            {
                case 0:
                    PlaceObjectFromAPI(Desk, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY, 0));
                    break;
                case 1:
                    PlaceObjectFromAPI(BlackLaptop, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY, 0));
                    break;
                case 2:
                    PlaceObjectFromAPI(WhiteLaptop, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY, 0));
                    break;
            }
        }
    }
    void PlaceObjectFromAPI(PlaceableObject prefab, string rotation, Vector3 Location)
    {
        // Instantiate the prefab at the given location with no extra rotation
        PlaceableObject obj = Instantiate(prefab, Location, Quaternion.identity);
        // Ensure the internal transform reference is set (used while dragging)
        if (obj.trans == null)
            obj.trans = obj.transform;
        // Place at exact location from API (snap to z=0)
        obj.transform.position = new Vector3(Location.x, Location.y, 0);
        obj.trans.position = obj.transform.position;
        // Apply rotation/state from API
        obj.Rotation = rotation;
        obj.isDragging = false;
    }
    public void PlaceObject(PlaceableObject prefab)
    {
        var Object = Instantiate(prefab);
        Object.isDragging = true;
        Object.Rotation = "Front";
    }
    List<APIObject> GatherObjectsInList()
    {
        List<APIObject> objects = new List<APIObject>();
        var placeableObjects = UnityEngine.Object.FindObjectsByType<PlaceableObject>(FindObjectsSortMode.None);
        foreach (var placeableObject in placeableObjects)
        {
            APIObject obj = new APIObject();
            obj.Id = Guid.NewGuid();
            obj.LocationX = placeableObject.transform.position.x;
            obj.LocationY = placeableObject.transform.position.y;
            obj.Rotation = placeableObject.Rotation;
            obj.WorldId = currentWorld.Id;
            obj.Type = placeableObject.ObjectType;
            objects.Add(obj);
        }
        return objects;
    }
    public async Task LoadGame(APIWorld world)
    {
        IWebRequestReponse webRequestResponse = await APIClient.LoadObjects(world.Id);
        switch (webRequestResponse) {
            case WebRequestData<List<APIObject>> dataResponse:
                List<APIObject> objects = dataResponse.Data;
                Debug.Log($"Loaded {objects.Count} Objects SuccesFully");
            GridManager.GenerateGrid(world.Width, world.Height);
            LoadObjectsFromAPI(objects);
                DisplayManager.EnableGameUI();
                break;
        case WebRequestError errorResponse:
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Object Loading error: " + errorMessage);
            DisplayManager.OpenErrorMessage(errorMessage);
            break;
        default:
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
    async Task SaveGame(APIWorld world)
    {
        List<APIObject> objects = GatherObjectsInList();
        IWebRequestReponse webRequestResponse = await APIClient.SaveObjects(currentWorld.Id, objects);
        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log($"Saved {objects.Count} objects SuccesFully");
                GridManager.GenerateGrid(world.Width, world.Height);
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Object Loading error: " + errorMessage);
                DisplayManager.OpenErrorMessage(errorMessage);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
    public async void SaveGame()
    {
          await SaveGame(currentWorld);
    }
    public async void SaveAndQuit()
    {
            await SaveGame(currentWorld);
            UnloadGame();
    }
    public void UnloadGame()
    {
        GridManager.DeleteGrid();
        var deleteableObjects = UnityEngine.Object.FindObjectsByType<PlaceableObject>(FindObjectsSortMode.None);
        foreach (var deleteObject in deleteableObjects)
        {
            Destroy(deleteObject.gameObject);
        }
        DisplayManager.ToSaveList();
    }
}