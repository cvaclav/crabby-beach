using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection { Left, Right, Up, Down };

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private float speed;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    [HideInInspector]
    public MovementDirection faceDirection;

    private bool stunned;

    public float stunTime;

    private float stunningTimeCountdown;

    public void Start()
    {
        faceDirection = MovementDirection.Down;
        stunned = false;
        stunningTimeCountdown = stunTime;
        speed = moveSpeed;
    }

    void Update()
    {
        if (stunned)
        {
            if (stunningTimeCountdown > 0)
            {
                stunningTimeCountdown -= Time.deltaTime;
            }
            else
            {
                stunned = false;
                animator.SetBool("Stunned", false);
            }

            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x == 0)
        {
            if (movement.y == 1)
            {
                faceDirection = MovementDirection.Up;
            } 
            else if (movement.y == -1)
            {
                faceDirection = MovementDirection.Down;
            }
        } 
        else
        {
            if (movement.x == 1)
            {
                faceDirection = MovementDirection.Right;
            }
            else if (movement.x == -1)
            {
                faceDirection = MovementDirection.Left;
            }
        }

        if (movement.x == 0 && movement.y == 0)
        {
            faceDirection = MovementDirection.Down;
        }
    }

    void FixedUpdate()
    {
        if (!stunned)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    public void stun()
    {
        stunned = true;
        stunningTimeCountdown = stunTime;
        animator.SetFloat("Speed", 0);
        animator.SetBool("Stunned", true);
    }

    public bool isAbleToMove()
    {
        return stunned != true;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Garbage"))
        {
            GarbageObstacle obstacle = target.gameObject.GetComponent<GarbageObstacle>();
            speed += obstacle.garbage.playerSpeedModificator;
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Garbage"))
        {
            speed = moveSpeed;
        }
    }
}
