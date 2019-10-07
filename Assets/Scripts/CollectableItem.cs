using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public Rigidbody2D rb;

    private GameObject player;

    private PlayerHand playerHand;

    private PlayerMovement playerMovement;

    private bool isPickedUp;

    private bool isPickUpAllowed;

    public SpriteRenderer sr;

    private bool isStolen;

    private Transform thiefCarryPoint;

    public BuildableItem buildableItem;

    private bool canBeUsed;

    private bool isUsed;

    private float usageTimeCountdown = 0.4f;

    private bool isAddedToCastle;

    public AudioSource dropSound;

    public AudioSource pickSound;

    void Start()
    {
        isPickedUp = false;
        isPickUpAllowed = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHand = player.GetComponent<PlayerHand>();
        playerMovement = player.GetComponent<PlayerMovement>();
        isStolen = false;
        sr.sprite = buildableItem.sprite;
        canBeUsed = false;
        isUsed = false;
        isAddedToCastle = false;
    }

    void Update()
    {
        if (isUsed)
        {
            if (!isAddedToCastle)
            {
                if (usageTimeCountdown <= 0)
                {
                    addToCastle();
                }
                else
                {
                    usageTimeCountdown -= Time.deltaTime;
                }
            }
        }

        if (isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            isPickedUp = false;
            playerHand.PutDownItem();
            sr.sortingLayerName = "Default";
            sr.sortingOrder = 50;
            dropSound.Play();

            if (canBeUsed)
            {
                isUsed = true;
            }
        }

        if (!isPickedUp && isPickUpAllowed && Input.GetKeyDown(KeyCode.E) && playerMovement.isAbleToMove())
        {
            bool pickUp = playerHand.PickUpItem(gameObject);

            if (pickUp)
            {
                isPickedUp = true;
                isPickUpAllowed = false;
                sr.sortingLayerName = "Player";
                pickSound.Play();
            }
        }

        if (isPickedUp)
        {
            if (playerHand.isVisible())
            {
                sr.sortingOrder = 1;
            }
            else
            {
                sr.sortingOrder = -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPickedUp)
        {
            rb.MovePosition(playerHand.handPosition.transform.position);
        }

        if (isStolen)
        {
            rb.MovePosition(thiefCarryPoint.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player") && playerHand.isEmpty() && !isPickedUp && !isUsed)
        {
            isPickUpAllowed = true;
        }


        if (target.CompareTag("DropSpot"))
        {
            canBeUsed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            isPickUpAllowed = false;
        }
    }

    public void Stole(GameObject thief)
    {
        isPickedUp = false;
        isStolen = true;
        this.thiefCarryPoint = thief.transform.Find("CarryPoint");
    }

    public void Drop()
    {
        isPickedUp = false;
        isStolen = false;
        this.thiefCarryPoint = null;
        sr.sortingLayerName = "Default";
        sr.sortingOrder = 50;
    }
    private void addToCastle()
    {
        isAddedToCastle = true;

        GameObject castle = GameObject.FindGameObjectWithTag("Castle");
        castle.GetComponent<Castle>().addCollectable(buildableItem);

        GameObject generator = GameObject.Find("CollectablesGenerator");
        generator.GetComponent<CollectablesGenerator>().removeItem(buildableItem);

        Destroy(gameObject);
    }

}
