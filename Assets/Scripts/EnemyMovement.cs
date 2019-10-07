using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;

    private float speed;

    private GameObject player;

    public Rigidbody2D rb;

    private Vector2 moveSpot;

    private float waitingTimeCountdown;

    public float spotWaitTime;

    public float minX;

    public float maxX;

    public float minY;

    public float maxY;

    private PlayerMovement playerMovement;

    private GameObject stolenItem;

    private PlayerHand playerHand;

    public float followTrigger;

    public float fullHandFolloweTrigger;

    public Animator animator;

    void Start()
    {
        moveSpot = this.getRandomMovePosition();
        waitingTimeCountdown = spotWaitTime;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHand = player.GetComponent<PlayerHand>();
        speed = moveSpeed;
    }

    private void FixedUpdate()
    {
        float followTolerance = followTrigger;

        if (!playerHand.isEmpty())
        {
            followTolerance = fullHandFolloweTrigger;
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= followTolerance && playerMovement.isAbleToMove())
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime));
            animator.SetBool("Walk", true);
        }
        else
        {
            if (Vector2.Distance(transform.position, moveSpot) <= 0.01f)
            {
                if (waitingTimeCountdown <= 0)
                {
                    moveSpot = this.getRandomMovePosition();
                    waitingTimeCountdown = spotWaitTime;

                    if (stolenItem != null)
                    {
                        stolenItem.GetComponent<CollectableItem>().Drop();
                        stolenItem = null;
                    }
                }
                else
                {
                    waitingTimeCountdown -= Time.deltaTime;
                }

                animator.SetBool("Walk", false);
            } 
            else
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, moveSpot, speed * Time.fixedDeltaTime));
                animator.SetBool("Walk", true);
            }
        }
    }

    private Vector2 getRandomMovePosition()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player") && playerMovement.isAbleToMove())
        {
            moveSpot = this.getRandomMovePosition();
            waitingTimeCountdown = spotWaitTime;
        }

        if (target.CompareTag("Garbage"))
        {
            GarbageObstacle obstacle = target.gameObject.GetComponent<GarbageObstacle>();
            speed += obstacle.garbage.crabSpeedModificator;
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Garbage"))
        {
            speed = moveSpeed;
        }
    }

    public void StoleItem(GameObject item)
    {
        if (!this.CanStoleItem())
        {
            return;
        }

        this.stolenItem = item;
    }

    public bool CanStoleItem()
    {
        return this.stolenItem == null;
    }
}
