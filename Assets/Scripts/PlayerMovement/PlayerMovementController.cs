using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] int baseSpeed;
    [SerializeField] int jumpForce;

    void Update(){

        Debug.Log(playerRB.velocity);
        if(Input.GetKeyDown(KeyCode.D)){
            playerRB.velocity = new Vector2(baseSpeed, playerRB.velocity.y);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            playerRB.velocity = new Vector2(-baseSpeed, playerRB.velocity.y);
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            playerRB.velocity += new Vector2(playerRB.velocity.x, jumpForce);
        }


    }
}
