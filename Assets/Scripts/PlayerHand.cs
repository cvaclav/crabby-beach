using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject player;

    public GameObject handPositionUp;

    public GameObject handPositionRight;

    public GameObject handPositionDown;

    public GameObject handPositionLeft;

    [HideInInspector]
    public GameObject handPosition;

    private GameObject carryingObject;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        handPosition = handPositionDown;
        carryingObject = null;
    }

    void Update()
    {
        if (playerMovement.faceDirection == MovementDirection.Up)
        {
            handPosition = handPositionUp;
        }
        else if (playerMovement.faceDirection == MovementDirection.Right)
        {
            handPosition = handPositionRight;
        }
        else if (playerMovement.faceDirection == MovementDirection.Down)
        {
            handPosition = handPositionDown;
        }
        else if (playerMovement.faceDirection == MovementDirection.Left)
        {
            handPosition = handPositionLeft;
        }
    }

    public bool PickUpItem(GameObject item)
    {
        if (carryingObject != null)
        {
            return false;
        }

        carryingObject = item;
        return true;
    }

    public void PutDownItem()
    {
        carryingObject = null;
    }

    public void StoleItem(GameObject thief)
    {
        if (carryingObject == null) {
            return;
        }

        carryingObject.GetComponent<CollectableItem>().Stole(thief);
        thief.GetComponent<EnemyMovement>().StoleItem(carryingObject);
        this.PutDownItem();
    }

    public bool isEmpty()
    {
        return carryingObject == null;
    }

    public bool isVisible()
    {
        return playerMovement.faceDirection == MovementDirection.Right || playerMovement.faceDirection == MovementDirection.Down;
    }
}
