using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private Sprite _frontSpr, _backSpr, _leftSpr, _rightSpr;
    public Transform trans;
    public string Rotation;

    // Set this in the prefab inspector to identify which prefab type this instance came from
    [Tooltip("Set the numeric type for this prefab (0=Desk, 1=BlackLaptop, 2=WhiteLaptop)")]
    public int ObjectType;

    public bool isDragging = false;

    public void Update()
    {
        if (isDragging)
            trans.position = new Vector3(Mathf.Round(GetMousePosition().x), Mathf.Round(GetMousePosition().y), 0);
       
        if (Rotation == "Front")
        {
            GetComponent<SpriteRenderer>().sprite = _frontSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame && isDragging)
                Rotation = "Left";
            if (Keyboard.current.eKey.wasReleasedThisFrame && isDragging)
                Rotation = "Right";
        }
        else if (Rotation == "Back")
        {
            GetComponent<SpriteRenderer>().sprite = _backSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame && isDragging)
                Rotation = "Right";
            if (Keyboard.current.eKey.wasReleasedThisFrame && isDragging)
                Rotation = "Left";
        }
        else if (Rotation == "Left")
        {
            GetComponent<SpriteRenderer>().sprite = _leftSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame && isDragging)
                Rotation = "Back";
            if (Keyboard.current.eKey.wasReleasedThisFrame && isDragging)
                Rotation = "Front";
        }
        else if (Rotation == "Right")
        {
            GetComponent<SpriteRenderer>().sprite = _rightSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame && isDragging)
                Rotation = "Front";
            if (Keyboard.current.eKey.wasReleasedThisFrame && isDragging)
                Rotation = "Back";
        }
    }

    private void OnMouseUpAsButton()
    {
        isDragging = !isDragging;

        if (!isDragging)
        {
            Debug.Log("stopped dragging");  
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;
        return positionInWorld;
    }
}