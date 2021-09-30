using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float speed = 5f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingCheckRadius = 0.05f;
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private BoxCollider2D headCollider;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    
    private Rigidbody2D playerRB;
    private bool canJump;
    private bool canStand;
    private float directionX = 0f;
    private float directionY = 0f;



    // Start is called before the first frame update
    void Start()
    {
        Player _player = gameObject.GetComponent<Player>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal");
        directionY = Input.GetAxis("Jump");
        playerRB.velocity = new Vector2(directionX * speed,playerRB.velocity.y) ;

        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
        canStand = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius ,whatIsGround);
        
        _animator.SetFloat("airSpeed",Mathf.Abs(directionY));
        _animator.SetFloat("speed",Mathf.Abs(directionX));
        _animator.SetBool("isGrounded",canJump);

        if (directionX < 0)
        {
            playerSprite.flipX = true;
        }
        else if (directionX > 0)
        {
            playerSprite.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRB.AddForce(Vector2.up * jumpForce);
        }
        
        if (Input.GetKeyDown(KeyCode.C) && !canStand)
        {
            _animator.SetBool("isCrouching",headCollider.enabled);
            headCollider.enabled = !headCollider.enabled;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _player.Stamina >= 10)
        {
            Debug.Log("*Dash Motion*");
            _player.Stamina -= 20;
        }

    }
}
