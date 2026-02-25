using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement: MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        Vector2 moveInput = Keyboard.current.wKey.isPressed ? Vector2.up :
                            Keyboard.current.sKey.isPressed ? Vector2.down :
                            Keyboard.current.aKey.isPressed ? Vector2.left :
                            Keyboard.current.dKey.isPressed ? Vector2.right :
                            Vector2.zero;

        transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
    }
}
