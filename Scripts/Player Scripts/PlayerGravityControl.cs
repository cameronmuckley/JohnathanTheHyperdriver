using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityControl : MonoBehaviour
{

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    new Rigidbody2D rigidbody;
    private PlayerController player;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (player.inHyperSpeed && player.onGround)
        {
            player.fallScale = 0.0f;

        } else if (player.inHyperSpeed) {
            player.fallScale = 0.8f;
            
        // if the player is falling then set the gravity to something higher to make the fall snappier 
        } else if (rigidbody.velocity.y < 0)
        {
            player.fallScale = fallMultiplier;
            
        // if the player is rising then and has let go of the button, then set
        // the gravity to something slightly higher to start falling earlier and
        // make the fall snappier 
        } else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.fallScale = lowJumpMultiplier;
        // if the player is holding the button and rising up, set the gravity to normal
        } else
        {
            player.fallScale = 1.0f;
        }
        // fall in the player's down direction. Only does anything if the player's gravity is flipped
//        player.fallScale *= player.transform.localScale.y;
    }
}
