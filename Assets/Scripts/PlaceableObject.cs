using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private Sprite _frontSpr, _backSpr, _leftSpr, _rightSpr;
    public Transform trans;
    public string Rotation;

    private bool isDragging = false;

    public void StartDragging()
    {
        isDragging = true;
    }

    public void Update()
    {
        if (isDragging)
            trans.position = GetMousePosition();
        if (Rotation == "Front")
        {
            GetComponent<SpriteRenderer>().sprite = _frontSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame)
                Rotation = "Left";
            if (Keyboard.current.eKey.wasReleasedThisFrame)
                Rotation = "Right";
        }
        else if (Rotation == "Back")
        {
            GetComponent<SpriteRenderer>().sprite = _backSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame)
                Rotation = "Right";
            if (Keyboard.current.eKey.wasReleasedThisFrame)
                Rotation = "Left";
        }
        else if (Rotation == "Left")
        {
            GetComponent<SpriteRenderer>().sprite = _leftSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame)
                Rotation = "Back";
            if (Keyboard.current.eKey.wasReleasedThisFrame)
                Rotation = "Front";
        }
        else if (Rotation == "Right")
        {
            GetComponent<SpriteRenderer>().sprite = _rightSpr;
            if (Keyboard.current.qKey.wasReleasedThisFrame)
                Rotation = "Front";
            if (Keyboard.current.eKey.wasReleasedThisFrame)
                Rotation = "Back";
        }
    }

    private void OnMouseUpAsButton()
    {
        isDragging = !isDragging;

        if (!isDragging)
        {
            Debug.Log($"Stopped dragging at {GetMousePosition()}");
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;
        return positionInWorld;
    }
}