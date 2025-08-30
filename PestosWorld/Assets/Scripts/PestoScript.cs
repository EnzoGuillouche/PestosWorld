using UnityEngine;
using System;
using System.Collections.Generic;

public class PestoScript : MonoBehaviour
{
    public GameObject PestoGameObject;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private readonly float speed = 4f;
    private Vector2 direction; //  (0 = left, 1 = right), (0 = front, 1 = back)

    public bool moving = false;
    public bool collision = false;
    public bool isInHouse = false;
    float timer = 0.0f;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        // initiate stats dictionary
        System.Random rng = new System.Random();

        stats.Add("Curiosity", 75);//rng.Next(20, 41));
        stats.Add("Creativity", 75);//rng.Next(20, 41));
        stats.Add("Comfort", rng.Next(20, 41));
        stats.Add("Socialization", 75); //rng.Next(20, 41));
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
        GetComponent<Renderer>().enabled = !isInHouse;

        ManageHapiness();
    }

    public void Flip(int x, int y)
    {
        transform.localScale *= new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == GameObject.Find("PlayerArrow").GetComponent<PlayerArrowBehavior>().pointedObject || other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            if (other.gameObject.CompareTag("Destination"))
            {
                Destroy(other.gameObject); // Remove hitbox once reached
            }
            else if (other.gameObject.CompareTag("Walls") || other.gameObject.CompareTag("House"))
            {
                Vector2 pushDir = other.contacts[0].normal;
                transform.position += (Vector3)pushDir * 0.05f; // small push out

                if (other.gameObject == GameObject.Find("PlayerArrow").GetComponent<PlayerArrowBehavior>().pointedObject && other.gameObject.CompareTag("House"))
                {
                    isInHouse = true;
                    GameObject.Find("Player").GetComponent<PlayerScript>().ShowPestoUI(gameObject, true);
                }
            }

            collision = true;
            moving = false;
            moveInput = Vector2.zero;
            animator.SetBool("isMovingX", false);
            animator.SetBool("isMovingFront", false);
            animator.SetBool("isMovingBack", false);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            collision = false;
        }
    }

    public void Move(Vector2 movement)
    {
        if (isInHouse == true)
            return;
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

    public void UpdateStat(string stat, int amount)
    {
        stats[stat] += amount;
    }

    private void ManageHapiness()
    {
        int statPercentage = (stats["Curiosity"] + stats["Creativity"] + stats["Comfort"] + stats["Socialization"]) / 4;

        if (isInHouse)
        {
            if (statPercentage >= 75)
            {
                GameObject.Find("Player").GetComponent<PlayerScript>().ShowPestoUI(null, false);
                stats["Comfort"] = 33;
                isInHouse = false;

                GameObject go = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
                go.GetComponent<PestoScript>().Flip(-1, 1);
            }

            timer += Time.deltaTime;
            int seconds = (int)(timer % 60);

            if (seconds >= 2)
            {
                stats["Comfort"] += stats["Comfort"] < 100 ? seconds : 0;
                timer = 0;
            }
        }
    }
}