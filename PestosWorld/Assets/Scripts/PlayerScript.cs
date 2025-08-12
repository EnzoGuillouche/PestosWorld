using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    // private InputAction moveAction;
    private Vector2 moveInput;
    private readonly float speed = 4f;
    private Vector2 direction; //  (0 = left, 1 = right), (0 = front, 1 = back)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
    }

    private void Flip(int x, int y)
    {
        transform.localScale *= new Vector2(x, y);
    }

    public void Move(Vector2 movement)
    {
        moveInput = movement;

        if (moveInput != Vector2.zero)
        {
            if (moveInput.x > 0.5f ^ moveInput.x < -0.5f)
            {
                animator.SetBool("isMovingX", true);
                animator.SetBool("isMovingFront", false);
                animator.SetBool("isMovingBack", false);

                if (moveInput.x < 0)
                {
                    if (direction.x != 1)
                        Flip(-1, 1);
                    direction.x = 1;
                }
                else
                {
                    if (direction.x != 0)
                        Flip(-1, 1);
                    direction.x = 0;
                }
            }
            else
            {
                if (moveInput.y != 0)
                {
                    animator.SetBool("isMovingX", false);

                    if (moveInput.y < 0)
                    {
                        direction.y = 1;
                        animator.SetBool("isMovingFront", true);
                        animator.SetBool("isMovingBack", false);
                    }
                    else
                    {
                        direction.y = 0;
                        animator.SetBool("isMovingFront", false);
                        animator.SetBool("isMovingBack", true);
                    }
                }
            }
        }
        else
        {
            animator.SetBool("isMovingX", false);
            animator.SetBool("isMovingFront", false);
            animator.SetBool("isMovingBack", false);
        }
    }
}
