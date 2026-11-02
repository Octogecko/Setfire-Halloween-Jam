using UnityEngine;
using UnityEngine.InputSystem;

public class DontDisplayMouse : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
}
