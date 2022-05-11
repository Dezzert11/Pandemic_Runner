using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerbody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
   
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        //Grab components for rigidbody and animator
        playerbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        //Whole thing is for flipping player right or left
        //Means player is moving right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //Anim parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall Jump logic
        if (wallJumpCooldown > 0.2f)
        {
            
            //change the velocity and what direction the player is going in
            playerbody.velocity = new Vector2(horizontalInput * speed, playerbody.velocity.y);

            //Checks if player is on the wall and not grounded
            if (onWall() && !isGrounded())
            {
                playerbody.gravityScale = 0;
                playerbody.velocity = Vector2.zero;
                //Gets stuck to the wall and not able to fall down
            }
            else
                playerbody.gravityScale = 1.5f;


            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                //only jump if grounded
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }

        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            playerbody.velocity = new Vector2(playerbody.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            Debug.Log("jump");
        }
        else if (onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            playerbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1, 3);
            //Finds the direction in which the player is in and let's it wall climb
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
        //if there is nothing beneath the player it is equal to null
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    
    }
}
