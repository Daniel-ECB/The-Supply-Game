using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed = 10; // How fast the player will move?
    public float jumpForce = 3;// How high can the player jump?
    public LayerMask groundLayer;//The ground layer for collision detection.
    public Transform legPosition;// Position of the leg for collision detection.
    public float collisionCircleRadius = 0.1f;
    public bool grounded = true;// Is the player on the ground?



    private Rigidbody2D body;
    private Animator anim;
    private bool isFacingRight = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Movement
        float x = Input.GetAxis("Horizontal"); //Get input for left and right arrow.
        Move(x);

        //Jump
        grounded = Physics2D.OverlapCircle(legPosition.position, collisionCircleRadius, groundLayer);

        if (Input.GetKey(KeyCode.Space) && grounded)// If pressed the spacebar and the player is on the ground
        {
            Jump();
        }

        //Animation
        anim.SetFloat("Speed", Mathf.Abs(x));//For run animation
        anim.SetFloat("YVelocity", body.velocity.y);//For jump animation
        anim.SetBool("Grounded", grounded);

        //Flip the player according to velocity

        if (x < 0 && isFacingRight)//If the player is facing towards the right and left arrow is pressed
        {
            FlipPlayer();
        }
        else if (x > 0 && !isFacingRight)//If the player is facing towards the left and right arrow is pressed
        {
            FlipPlayer();
        }
    }

    private void Move(float x)
    {
        Vector2 newVelocity = new Vector2(x * speed * Time.deltaTime, body.velocity.y); //Change the 'x' Velocity keeping the 'y' velocity the same.
        body.velocity = newVelocity;//Change the velocity.
    }

    private void Jump()
    {
        Vector2 newVelocity = new Vector2(body.velocity.x, jumpForce);//Keep 'x' same and change 'y'
        body.velocity = newVelocity;
    }

    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;//flip the x axis
        transform.localScale = scale;
    }
}
