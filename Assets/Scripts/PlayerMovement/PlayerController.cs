using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGhost = false;
    public int jumpCounter = 0;

    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] int baseSpeed;
    [SerializeField] int jumpForce;
    [SerializeField] int layermask;

    Animator animator;

    public bool isGrounded = false;

    float distanceToGround = 0f;

    private void Awake()
    {
        playerRB = this.gameObject.GetComponentInParent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
        layermask = 1 << layermask;
    }

    void Update()
    {
        checkGround();

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(baseSpeed, playerRB.velocity.y);
            animator.SetFloat("speed", 1);

            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-baseSpeed, playerRB.velocity.y);
            animator.SetFloat("speed", 1);

            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetFloat("speed", 0);
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGhost)
            {
                //Addtl jumping logic here
                if (jumpCounter < 5)
                {
                    playerRB.velocity += new Vector2(playerRB.velocity.x, -(jumpForce-(jumpCounter*0.1f)));
                }
                jumpCounter++;
                animator.SetTrigger("jump");

            }
            else if (isGrounded)
            {
               playerRB.velocity += new Vector2(playerRB.velocity.x, jumpForce);
                animator.SetTrigger("jump");
            }
        }

    }

    bool checkGround()
    {
        if ((!isGhost && Physics2D.Raycast(this.transform.position, Vector2.down, distanceToGround + 0.1f, layermask)) || (isGhost && Physics2D.Raycast(this.transform.position, Vector2.up, distanceToGround + 0.1f, layermask)))
        {
            jumpCounter = 0; //Reset jumps
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
