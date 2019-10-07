using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private PlayerMovement movement;

    private PlayerHand hand;

    public AudioSource stunSound;

    void Start()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
        hand = gameObject.GetComponent<PlayerHand>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            movement.stun();
            hand.StoleItem(target.gameObject);
            stunSound.Play();
        }
    }
}
