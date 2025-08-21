using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    private GameObject PestoDestination;
    private GameObject playerArrowObject;
    private Vector2 mousePos;
    private Vector3 targetPosition;
    private Vector2 targetDistance;
    public GameObject destinationHitboxPrefab;
    private GameObject currentHitbox;
    public GameObject pestoSelectedUiPrefab;
    private GameObject currentPestoUi;
    private Vector2 pestoSelectedUiPrefabPosition = new Vector2(4.3f, -0.1f);
    private Vector2 moveDir;
    public Text uiText;

    void Start()
    {
        playerArrowObject = GameObject.Find("PlayerArrow");
    }

    void Update()
    {
        displayStats();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Clicked();
        }

        if (PestoDestination != null)
        {
            MovePesto(moveDir);
        }
    }

    private void displayStats()
    {
        if (currentPestoUi == null)
            return;

        foreach (var stat in PestoDestination.GetComponent<PestoScript>().stats)
        {
            // Find the text element in the hierarchy
            Text uiText = currentPestoUi.transform.Find("Stats")?.Find(stat.Key + "Text")?.Find("Text (Legacy)").GetComponent<Text>();

            uiText.text = stat.Key + ": " + stat.Value + "%";
        }
    }

    private void SetDestination(Vector2 targetPosition)
    {
        // Destroy any old hitboxes
        if (currentHitbox != null)
        {
            Destroy(currentHitbox);
        }

        // Spawn a new invisible hitbox at clicked position
        currentHitbox = Instantiate(destinationHitboxPrefab, targetPosition, Quaternion.identity);

        // Calculate move direction
        moveDir = (targetPosition - (Vector2)transform.position).normalized;
        PestoDestination.GetComponent<PestoScript>().moving = true;
    }

    public void MovePesto(Vector2 moveDir)
    {
        if (PestoDestination.GetComponent<PestoScript>().moving)
        {
            if (PestoDestination.GetComponent<PestoScript>().collision != true)
            {
                PestoDestination.GetComponent<PestoScript>().Move(moveDir);
            }
            else
            {
                PestoDestination.GetComponent<PestoScript>().moving = false;
            }
        }
    }

    private void Clicked()
    {
        // get pesto
        System.Random rng = new System.Random();
        // PestoDestination = GameObject.Find("Pesto " + rng.Next(1, 3));
        PestoDestination = GameObject.Find("Pesto 1");

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (PestoDestination == null)
            return;

        mousePos = Mouse.current.position.ReadValue();
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        targetPosition.z = PestoDestination.transform.position.z;

        if (hit.collider != null)
        {
            playerArrowObject.GetComponent<PlayerArrowBehavior>().UpdateGameObject(hit.collider.gameObject);

            if (PestoDestination == hit.collider.gameObject)
            {
                if (currentPestoUi == null)
                {
                    currentPestoUi = Instantiate(pestoSelectedUiPrefab, pestoSelectedUiPrefabPosition, Quaternion.identity);

                }
                else
                {
                    Destroy(currentPestoUi);
                }
                return;
            }
        }
        else
        {
            SetDestination(targetPosition);
            playerArrowObject.GetComponent<PlayerArrowBehavior>().UpdateGameObject(currentHitbox);
        }

        CalculateMovementVect(targetDistance, PestoDestination);
        PestoDestination.GetComponent<PestoScript>().moving = true;
    }

    private void CalculateMovementVect(Vector2 targetDistance, GameObject Pesto)
    {
        targetDistance.x = targetPosition.x - Pesto.transform.position.x;
        targetDistance.y = targetPosition.y - Pesto.transform.position.y;

        moveDir = targetDistance.normalized;
    }
}
