using System;
using UnityEngine;

/*
* The GameObject also needs a collider otherwise OnMouseUpAsButton() can not be detected.
*/
public class Draggable: MonoBehaviour
{
    public Transform trans;
 
    private bool isDragging = false;

    public void StartDragging()
    {
        isDragging = true;
    }

    public void Update()
    {
        if (isDragging)
            trans.position = GetMousePosition();
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
