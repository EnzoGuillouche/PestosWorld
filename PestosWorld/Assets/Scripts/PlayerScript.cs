using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private GameObject PestoDestination;
    private GameObject playerArrowObject;
    private Vector2 mousePos;
    private InputAction click;
    private Vector3 targetPosition;
    private Vector2 targetDistance;
    private Vector2 moveDir;
    private bool moving = false;

    void Start()
    {
        playerArrowObject = GameObject.Find("PlayerArrow");
        click = InputSystem.actions.FindAction("Click");
        click.performed += ctx => OnMouseDown();
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid leaks
        click.performed -= ctx => OnMouseDown();
    }

    void Update()
    {
        if (moving)
        {
            MovePesto(moveDir);

            if (Vector2.Distance(PestoDestination.transform.position, targetPosition) < 0.005f)
            {
                moving = false;
            }
        }
    }

    public void MovePesto(Vector2 moveDir)
    {
        if (moving)
        {
            if (PestoDestination.GetComponent<PestoScript>().collision != true)
            {
                PestoDestination.GetComponent<PestoScript>().Move(moveDir);
            }
            else
            {
                moving = false;
            }
        }
    }

    private void OnMouseDown()
    {
        // get pesto
        System.Random rng = new System.Random();
        PestoDestination = GameObject.Find("Pesto " + rng.Next(1, 3));

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (PestoDestination == null)
            return;

        if (hit.collider != null)
        {
            playerArrowObject.GetComponent<PlayerArrowBehavior>().UpdateGameObject(hit.collider.gameObject);
            if (PestoDestination == hit.collider.gameObject)
                return;
        }

        mousePos = Mouse.current.position.ReadValue();

        targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        targetPosition.z = PestoDestination.transform.position.z;
        CalculateMovementVect(targetDistance, PestoDestination);
        moving = true;
    }

    private void CalculateMovementVect(Vector2 targetDistance, GameObject Pesto)
    {
        targetDistance.x = targetPosition.x - Pesto.transform.position.x;
        targetDistance.y = targetPosition.y - Pesto.transform.position.y;

        moveDir = targetDistance.normalized;
    }
}
