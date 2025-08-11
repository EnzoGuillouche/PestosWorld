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
    private Vector2 direction; //  (0 = left, 1 = right), (0 = front, 1 = back)

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
            animator.SetBool("isMovingX", false);
        
        if (moveInput.y != 0)
        {
            animator.SetBool("isMovingY", true);
            if (moveInput.y < 0)
            {
                direction.y = 1;
            }
            else 
                direction.y = 0;
        }
        else
            animator.SetBool("isMovingY", false);

        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
    }

    private void Flip(int x, int y)
    {
        pesto.transform.localScale *= new Vector2(x, y);
    }
}
