using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] int baseSpeed;
    [SerializeField] int jumpForce;

    private void Awake()
    {
        playerRB = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    void Update(){

        Debug.Log(playerRB.velocity);
        if(Input.GetKey(KeyCode.D)){
            playerRB.velocity = new Vector2(baseSpeed, playerRB.velocity.y);
        }
        else if(Input.GetKey(KeyCode.A)){
            playerRB.velocity = new Vector2(-baseSpeed, playerRB.velocity.y);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            playerRB.velocity += new Vector2(playerRB.velocity.x, -jumpForce);
        }


    }
}
