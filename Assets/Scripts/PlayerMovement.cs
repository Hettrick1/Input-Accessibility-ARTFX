using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Settings")]
    [SerializeField] private float walkspeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airControl;

    [Header("References")]
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;

    private Rigidbody rb;
    private Vector2 input;
    private float speed;
    private bool isGrounded, isSprinting, isJumping, isPaused, canPlaySound = true;
    private Vector3 initialLocalPos;


    public void PlayerCanMove(bool canMove)
    {
        isPaused = !canMove;
    }
    public bool GetPause()
    {
        return isPaused;
    }
    private void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        speed = walkspeed;
    }

    public void playerMovement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void jumpInput(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            if (isGrounded && context.ReadValueAsButton() && !isJumping)
            {
                rb.AddForce(new Vector3(0, jumpForce * 25, 0));

                Invoke(nameof(setIsJumping), 0.2f);
            }
        }
    }
    private void setIsJumping()
    {
        isJumping = true;
    }

    public void runMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, ground);
            if (isGrounded && isJumping)
            {
                isJumping = false;
            }

            else if (!isSprinting)
            {
                speed = walkspeed;
            }

            else
            {
                speed = runSpeed;
            }
        }
    }


    private void FixedUpdate()
    {
        if (!isPaused)
        {
            float friction;
            if (isGrounded && !isJumping)
            {
                friction = 1;
                rb.drag = 8;
            }
            else
            {
                friction = 0.2f;
                if (speed == runSpeed)
                {
                    rb.drag = 0.01f * airControl;
                }
                else
                {
                    rb.drag = 0.05f * airControl;
                }
                if (rb.velocity.y < 0.5f)
                {
                    rb.AddForce(new Vector3(0, -250 * Time.fixedDeltaTime, 0));
                }
                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -10, 10), Mathf.Clamp(rb.velocity.y, -10, 10), Mathf.Clamp(rb.velocity.z, -10, 10));
            }

            Vector3 movement = new Vector3((transform.forward.x * input.y) + (transform.right.x * input.x), 0, (transform.forward.z * input.y) + (transform.right.z * input.x));
            rb.AddForce(movement.normalized * speed * Time.fixedDeltaTime * 500 * friction, ForceMode.Force);
        }
    }

    public Vector2 getInput()
    {
        return input;
    }

    public void SetIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
    public bool GetIsPaused()
    {
        return isPaused;
    }
}

