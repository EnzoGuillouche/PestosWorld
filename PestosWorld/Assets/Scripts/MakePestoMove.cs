using UnityEngine;
using UnityEngine.InputSystem;

public class MakePestoMove : MonoBehaviour
{
    private GameObject PestoDestination;
    private Vector2 mousePos;
    private InputAction click;
    private Vector3 targetPosition;
    private Vector2 targetDistance;
    private Vector2 moveDir;
    private bool moving = false;

    void Start()
    {
        PestoDestination = GameObject.Find("Pesto");

        click = InputSystem.actions.FindAction("Click");
        click.performed += ctx => OnClick();
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid leaks
        click.performed -= ctx => OnClick();
    }

    void Update()
    {
        if (moving)
        {
            MovePesto(moveDir);
            
            if (Vector2.Distance(PestoDestination.transform.position, targetPosition) < 0.5f)
            {
                moving = false;
            }
        }
    }

    public void MovePesto(Vector2 moveDir)
    {
        // then update it
        PestoDestination.GetComponent<PlayerScript>().Move(moveDir);
    }

    private void OnClick()
    {
        mousePos = Mouse.current.position.ReadValue();
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        targetPosition.z = PestoDestination.transform.position.z;
        CalculateMovementVect();
        moving = true;
    }

    private void CalculateMovementVect()
    {
        targetDistance.x = targetPosition.x - PestoDestination.transform.position.x;
        targetDistance.y = targetPosition.y - PestoDestination.transform.position.y;

        moveDir = targetDistance.normalized;
    }
}
