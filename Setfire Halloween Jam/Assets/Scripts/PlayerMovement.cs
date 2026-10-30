using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Speed & direction
    [SerializeField] private float speed = 3f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float baseSpeed = 3f;
    [SerializeField] private float maxSpeed = 6f;
    private Vector2 direction;
    private bool accelerating = false;
    private bool slowingDown = false;

    // Rotation
    private float adjacent;
    private float opposite;
    private float alpha;

    private void Update()
    {
        if (accelerating)
        {
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        else if (slowingDown)
        {
            if (speed > 0)
            {
                speed -= acceleration * Time.deltaTime;
            }
            else
            {
                direction = Vector2.zero;
                speed = 3;
                slowingDown = false;
            }
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            slowingDown = false;
            accelerating = true;
            speed = baseSpeed;
        }

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
            slowingDown = true;
            accelerating = false;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(direction.x, direction.y) * speed * Time.fixedDeltaTime);
    }
}
