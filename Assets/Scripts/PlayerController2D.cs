using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour
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

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 screenBoundaries;
    private BoxCollider2D boxCollider2d;

    private void Awake()
    {
        //avoids raycast on GO's own collider
        Physics2D.queriesStartInColliders = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        //merging layer masks
        finalMask = whatIsGround | whatIsPlayer;
        //finalMask = whatIsGround;

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
            Debug.Log("died");
            Die();
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    //Gizmos.DrawCube(new Vector3(0, screenBoundaries.y, 0), new Vector3(1, 1, 1));
    //}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

        if (!IsGrounded())
        {
            rb.AddForce(Vector2.down * gravityForce);
        }

        if (Input.GetKeyDown(playerKeys["up"]))
        {
            if (IsGrounded())
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

    private bool IsGrounded()
    {
        float extraHeightTest = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightTest, finalMask);

        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, extraHeightTest), Vector2.right * (boxCollider2d.bounds.extents.x*2), rayColor);

        return raycastHit.collider != null;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
