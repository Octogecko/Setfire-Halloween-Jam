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
    private float alpha;

    // Time
    [SerializeField] private float timeBeforeDirectionChange = 0.08f;
    private float changeTimer;
    private bool isChanging = false;
    private Vector2 changingDirection;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isChanging)
        {
            changeTimer -= Time.deltaTime;

            if (changeTimer <= 0)
            {
                ChangeDirection(changingDirection);
                isChanging = false;
            }
        }

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
                speed -= deceleration * Time.deltaTime;
            }
            else
            {
                direction = Vector2.zero;
                speed = baseSpeed;
                slowingDown = false;
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + new Vector3(direction.x, direction.y).normalized * speed * Time.fixedDeltaTime);
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
            Vector2 input = context.ReadValue<Vector2>();

            bool wasDiagonal = direction.x != 0 && direction.y != 0;
            bool nowCardinal = (input.x == 0) != (input.y == 0);

            if (accelerating && wasDiagonal && nowCardinal)
            {
                changingDirection = input;
                changeTimer = timeBeforeDirectionChange;
                isChanging = true;
            }
            else
            {
                isChanging = false;
                ChangeDirection(input);
            }
        }
        else
        {
            isChanging = false;
            slowingDown = true;
            accelerating = false;
        }
    }

    private void ChangeDirection(Vector2 newDirection)
    {
        direction = newDirection;
        alpha = Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0f, 0f, alpha);
    }
}