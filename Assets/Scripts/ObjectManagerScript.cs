using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridManagerScript GridManager;
    DisplayManagerScript DisplayManager;

    [SerializeField] PlaceableObject Desk; //0
    [SerializeField] PlaceableObject BlackLaptop; //1
    [SerializeField] PlaceableObject WhiteLaptop; //2


    public void LoadObjectsFromAPI(List<APIObject> objects)
    {
        foreach (APIObject obj in objects)
        {
            switch(obj.Type)
            {
                case 0:
                    PlaceObjectFromAPI(Desk, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
                    break;
                case 1:
                    PlaceObjectFromAPI(BlackLaptop, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
                    break;
                case 2:
                    PlaceObjectFromAPI(WhiteLaptop, obj.Rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
                    break;
            }
        }
    }

    void PlaceObjectFromAPI(PlaceableObject prefab, string rotation, Vector3 Location)
    {
        var Object = Instantiate(prefab);
    }

    public void PlaceObject(PlaceableObject prefab)
    {
        var Object = Instantiate(prefab);
        Object.isDragging = true;
        Object.Rotation = "Front";
    }
    public void LoadGame()
    {
        
    }
    public void UnloadGame()
    {
        GridManager.DeleteGrid();
        var deleteableObjects = Object.FindObjectsByType<PlaceableObject>(FindObjectsSortMode.None);
        foreach (var deleteObject in deleteableObjects)
        {
            Destroy(deleteObject.gameObject);
        }
        DisplayManager.ToSaveList();
    }
}