using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectManagerScript : MonoBehaviour
{
    public bool IsPlaying = true;
    public bool PlacementMode = false;
    [SerializeField] PlaceableObject Desk; //0
    [SerializeField] PlaceableObject BlackLaptop; //1
    [SerializeField] PlaceableObject WhiteLaptop; //2


    public void LoadObjectsFromAPI(List<APIObject> objects)
    {
        foreach (APIObject obj in objects)
        {
            switch(obj.type)
            {
                case 0:
                    PlaceObjectFromAPI(Desk, obj.rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
                    break;
                case 1:
                    PlaceObjectFromAPI(BlackLaptop, obj.rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
                    break;
                case 2:
                    PlaceObjectFromAPI(WhiteLaptop, obj.rotation, new Vector3((float)obj.LocationX, (float)obj.LocationY));
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
        PlacementMode = true;
    }
}
