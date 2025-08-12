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
        // get pesto
        System.Random rng = new System.Random();
        PestoDestination = GameObject.Find("Pesto " + rng.Next(1, 3));

        if (PestoDestination == null)
            return;

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
