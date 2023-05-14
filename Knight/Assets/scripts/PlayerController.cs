using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HorseController horseController;

    public CastleBuilder castleBuilder;
    // Start is called before the first frame update
    public float speed;
    public float jumpForce;
    public float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = false;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    private Animator anim;
    private bool isJumping;


    public bool isMounted;
    public bool canMount;
    private GameObject MountArrow;
    private void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //horseController = GameObject.FindObjectOfType<HorseController>();
        MountArrow = GameObject.Find("Horse").transform.GetChild(4).gameObject;
        MountArrow.SetActive(false);
        isMounted = false;

    }

    private void FixedUpdate()
    {
        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        


        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (!isMounted)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(moveInput * speed * 2, rb.velocity.y);
            }

            if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
            else if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
        }
        
    }

    private void Update()
    {
        
        anim.SetBool("isFighting", false);
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            anim.SetTrigger("takeOff");
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true) {

            extraJumps = extraJumpValue;
            anim.SetTrigger("takeOff");
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && castleBuilder.buildModeOn == false)
        {
            anim.SetBool("isFighting", true);
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "trigger")
        {
            MountArrow.SetActive(true);
            canMount = true;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "trigger")
        {
            MountArrow.SetActive(false);
            canMount = false;
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
