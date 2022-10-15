using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] int baseSpeed;
    [SerializeField] int jumpForce;
    [SerializeField] int layermask;

    bool isGrounded = false;

    float distanceToGround = 0f;

    void Start()
    {
        distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
        Debug.Log(distanceToGround);
    }

    void Update()
    {
        checkGround();

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(baseSpeed, playerRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-baseSpeed, playerRB.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.velocity += new Vector2(playerRB.velocity.x, jumpForce);
        }

    }

    bool checkGround()
    {
        layermask = 1 << layermask;

        if (Physics2D.Raycast(this.transform.position, Vector2.down, distanceToGround + 0.1f, layermask))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }
}
