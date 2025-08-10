using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private GameObject pesto;
    private Animator animator;
    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 moveInput;
    private readonly float speed = 7f;
    private int direction = 0; // 0 = front, 1 = left, 2 = right, 3 = back

    void Start()
    {
        pesto = GameObject.Find("Pesto");
        rb = pesto.GetComponent<Rigidbody2D>();
        animator = pesto.GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x != 0)
        {
            animator.SetBool("isMovingX", true);
            if (moveInput.x < 0)
            {
                if (direction != 1)
                    Flip(-1, 1);
                direction = 1;
            }
            if (moveInput.x > 0)
            {
                if (direction != 2)
                    Flip(-1, 1);
                direction = 2;
            }
        }
        else
            animator.SetBool("isMovingX", false);
        // if (moveInput.y != 0)
        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
    }

    private void Flip(int x, int y)
    {
        pesto.transform.localScale *= new Vector2(x, y);
    }
}
