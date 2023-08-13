using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoment : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float playerJumpHeight;
    Vector3 dir;
    Rigidbody2D rb;
    float xInput;
    [HideInInspector]
    public bool canMove=true;
    // static bool isJumping = false;
   

    public Transform WallCheck;
    private bool isSliding;
    public float slidingSpeed;
    bool ShouldSlide;

    private bool walljump;
    public float wallJumpDurationTime;
    public Vector2 wallJumpSpeed;

    private PlayerAnimController animatorRefPlayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorRefPlayer=transform.GetChild(0).GetComponent<PlayerAnimController>();
    }

    RaycastHit2D hitGround;//for jumping

    private void FixedUpdate()
    {
        if(canMove)
        {
            movement(); 
        }
       
        wallJumpPlayer();
        isSliding = Physics2D.OverlapBox(WallCheck.position, new Vector2(0.1f, 1f), 0, LayerMask.GetMask("HideObjects"));
        hitGround = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));//Ray cas tto check the player is eligible to jump

        //  Debug.DrawLine(transform.position, Vector2.down, Color.blue);
        //permisio to Jump



       


       
        if (PlayeIsSlidindCheck())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
            animatorRefPlayer.PlayerSlide(true);
            
        }
        else
        {
            animatorRefPlayer.PlayerSlide(false);
        }
        if (walljump)
        {
            rb.velocity=new Vector2(-xInput*wallJumpSpeed.x,wallJumpSpeed.y);

        }
       
        
        if(!isSliding && hitGround.collider == null)
        {
            animatorRefPlayer.PlayerFall((int)rb.velocity.y);
            animatorRefPlayer.PlayerIdle(false);
        }
        else
        {
            animatorRefPlayer.PlayerFall(1);
        }
        
    }
   
    void movement()//Moment for The Player By both phone and PC
    {
        xInput = Input.GetAxis("Horizontal");
        dir.x = xInput * playerSpeed;
        //animations
        if(xInput!=0 && hitGround.collider!=null)
        {
            animatorRefPlayer.PlayerRun(true);
            animatorRefPlayer.PlayerIdle(false);
        }
        else
        {
            animatorRefPlayer.PlayerIdle(true);
            animatorRefPlayer.PlayerRun(false);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            jumpPlayer();
            animatorRefPlayer.PlayerJump((int)rb.velocity.y);
        }
        else
        {
            animatorRefPlayer.PlayerJump(0);
        }
      
        rb.velocity = dir;//moment * the button pressed calculation 
        //flip code
        if(xInput < 0 && isFacingRight )
        {
            flip();
        }
        if (xInput > 0 && !isFacingRight)
        {
            flip();
        }

        
    }
    bool isFacingRight=true;
    void flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180,0);
    }
  
    bool PlayeIsSlidindCheck()
    {
        if (isSliding && hitGround.collider == null && xInput != 0)
        {
            ShouldSlide = true;
        }
        else
        {
            ShouldSlide = false;
        }
        return ShouldSlide;
    }
    
    void jumpPlayer()//jumping code
    {
        if ( hitGround.collider!=null)//Space Button For PC
        {
            rb.position += Vector2.Lerp(transform.position, Vector2.up * playerJumpHeight, 1);
            
        }

    }
       
    void wallJumpPlayer()
    {
       if(Input.GetKey(KeyCode.Space ) && ShouldSlide)
        {
            walljump = true;
            Invoke("DurationOfWallJump", wallJumpDurationTime);  
        } 
    }
    void DurationOfWallJump()
    {
        walljump = false;
       
    }
    
    
        
    
}

