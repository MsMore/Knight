using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{

    private Rigidbody2D rb;
    public PlayerController pc;
    public float horseSpeed;
    private Animator anim;
    private float moveInput;

    private GameObject pin;

    private bool facingRight = false;
    private GameObject player;
    
    [SerializeField]private bool canRideHorse;

    private GameObject mountedHorse;
    private GameObject dismountedHorse;
    

    void Start()
    {
        pin = GameObject.Find("pin");
        pin.transform.position = pin.transform.parent.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("playerHolder").transform.GetChild(0).gameObject;
        pc.isMounted = false;
        mountedHorse = GameObject.Find("Horse").gameObject.transform.GetChild(2).gameObject;
        dismountedHorse = GameObject.Find("Horse").gameObject.transform.GetChild(3).gameObject;
        anim.SetBool("isMounted", false);
        
    }

    void Update()
    {
        pin.transform.position = pin.transform.parent.position + new Vector3(0, 3.5f, 0);
        if (pc.canMount == true && pc.isMounted == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("mounted");
            player.SetActive(false);
            anim.SetBool("isMounted", true);
            pc.isMounted = true;
            pc.canMount = false;
            canRideHorse = true;
            mountedHorse.SetActive(true);
            pin.transform.parent = mountedHorse.transform;
            dismountedHorse.SetActive(false);
            
        }
        if (canRideHorse)
        {
            pc.canMount = false;
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * horseSpeed, rb.velocity.y);
            if (moveInput != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
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
        if (pc.isMounted && Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            anim.SetBool("isMounted", false);
            player.SetActive(true);
            player.transform.position = transform.position;
            pc.isMounted = false;
            mountedHorse.SetActive(false);
            dismountedHorse.SetActive(true);
            canRideHorse = false;
            pin.transform.parent = player.transform;
           

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
