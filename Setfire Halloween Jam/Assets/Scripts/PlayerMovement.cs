using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Speed & direction
    [SerializeField] private float speed = 3f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 1f;
    private float baseSpeed = 3f;
    private float maxSpeed = 8f;
    private Vector2 direction;

    // Rotation
    private float adjacent;
    private float opposite;
    private float alpha;

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
            direction = context.ReadValue<Vector2>();

            adjacent = transform.position.x + direction.x - transform.position.x;
            opposite = transform.position.y + direction.y - transform.position.y;
            alpha = Mathf.Atan2(opposite, adjacent) * (180 / Mathf.PI);
            transform.rotation = Quaternion.Euler(0f, 0f, alpha);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(direction.x, direction.y) * speed * Time.fixedDeltaTime);
    }
}
