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
    private bool nowCardinal = false;
    private bool wasDiagonal = false;
    private bool returningStick = false;
    private float lastMagnitude = 0f;

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
                GetComponent<Animator>().SetBool("walking", false);
            }
        }
    }

    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.started)
        {
            speed = baseSpeed;
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (!context.canceled && input != Vector2.zero)
        {
            slowingDown = false;
            accelerating = true;
            wasDiagonal = direction.x != 0 && direction.y != 0;
            nowCardinal = (input.x == 0) != (input.y == 0);
            returningStick = input.magnitude < lastMagnitude - 0.01f;
            lastMagnitude = input.magnitude;

            if ((wasDiagonal && nowCardinal) || returningStick)
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
            lastMagnitude = 0f;
            isChanging = false;
            slowingDown = true;
            accelerating = false;
        }
    }

    private void ChangeDirection(Vector2 newDirection)
    {
        direction = newDirection;
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            GetComponent<Animator>().SetBool("back", false);
            GetComponent<Animator>().SetBool("side", true);
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        }
        else
        {
            if (direction.y >= 0)
            {
                GetComponent<Animator>().SetBool("back", true);
                GetComponent<Animator>().SetBool("side", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("back", false);
                GetComponent<Animator>().SetBool("side", false);
            }
        }
    }
}