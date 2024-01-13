using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;
    private int jumpCounter;
    private float coyoteCounter;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    //private bool grounded;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         horizontalInput = Input.GetAxis("Horizontal");

        


         if (horizontalInput > 0.01f){

            transform.localScale = Vector2.one; 

        }
        else if(horizontalInput < -0.01f){

            transform.localScale = new Vector2(-1,1);
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else{
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else 
                coyoteCounter -= Time.deltaTime;
        }
        
        

        //print(onWall());

        // if(wallJumpCooldown<0.2f){

            
        //     body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //         if (onWall() && !isGrounded())
        //         {
        //             body.gravityScale = 0;
        //             body.velocity = Vector2.zero;
        //         }
        //          else
        //          {
        //             body.gravityScale = 7.0f;
        //          }

        //          if(Input.GetKey(KeyCode.Space) )
        //          {
        //             Jump();
        //             if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        //             {
        //                     SoundManager.instance.PlaySound(jumpSound);
        //             }

        //          }
        // }
        
        /*else
        {
            wallJumpCooldown != Time.deltaTime;
        }*/

        
        

    }

    private void Jump(){
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);

    if (onWall())
        WallJump();
    else
    {
        if (isGrounded())
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        else
        {
            if (coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else{
                if(jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter --;
                }
            }
        }

        coyoteCounter = 0;
    }
        
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }


}

