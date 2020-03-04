using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2DBackup : MonoBehaviour
{

    public enum PlayerSelector { PlayerOne, PlayerTwo};
    public PlayerSelector playerSelector;

    private Dictionary<string, KeyCode> playerKeys = new Dictionary<string, KeyCode>();

    private float runSpeed = 4f;
    private float jumpForce = 15f;
    private float gravityForce = 35f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    private int finalMask;

    public Transform groundCheck;
    public float groundCheckRadius;
    public bool grounded;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 screenBoundaries;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

        //merging layer masks
        finalMask = whatIsGround | whatIsPlayer;

        //setting the controls if player 1 or 2

        if (playerSelector == PlayerSelector.PlayerOne)
        {
            playerKeys.Add("up", KeyCode.W);
            playerKeys.Add("down", KeyCode.S);
            playerKeys.Add("left", KeyCode.A);
            playerKeys.Add("right", KeyCode.D);
        }
        else if (playerSelector == PlayerSelector.PlayerTwo)
        {
            playerKeys.Add("up", KeyCode.UpArrow);
            playerKeys.Add("down", KeyCode.DownArrow);
            playerKeys.Add("left", KeyCode.LeftArrow);
            playerKeys.Add("right", KeyCode.RightArrow);
        }

        //finding screen boundaries
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.y > 0.01)
        {
            animator.SetBool("isJumping", true);
        }
        else if (rb.velocity.y < 0.01)
        {
            animator.SetBool("isJumping", false);
        }
       
        if (transform.position.y < screenBoundaries.y*-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, finalMask);

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

        if (!grounded)
        {
            rb.AddForce(Vector2.down * gravityForce);
        }

        if (Input.GetKeyDown(playerKeys["up"]))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
            }
        }

        if (Input.GetKey(playerKeys["right"]) && (transform.position.x < (screenBoundaries.x - GetComponent<SpriteRenderer>().bounds.size.x)))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(playerKeys["left"]) && (transform.position.x > (screenBoundaries.x*-1 + GetComponent<SpriteRenderer>().bounds.size.x)))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            rb.velocity = new Vector2(runSpeed * -1, rb.velocity.y);
        }
    }
}
