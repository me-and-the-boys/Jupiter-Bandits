using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 


    public float speed;
    public float jumpForce;
    private float moveInput;
    public bool facingright;
    private Rigidbody2D rb;

    //double jump stuff
    private bool isgrounded;
    public Transform groundcheck;
    public float  checkradius;
    public LayerMask whatisground;

    private int extrajumps;
    public int extrajumpsvalue;

    //Dash vars
    public float dashdistanceF;
    private int dashRepeat;
    public int dashRepeatMax;
    private float dashdistanceB;

    // Start is called before the first frame update
    void Start()
    {
        dashdistanceB = dashdistanceF * -1;
        extrajumps = extrajumpsvalue;
        facingright = true;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this is to help check if grounded
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, checkradius, whatisground);
        

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    
        if(facingright == false && moveInput > 0){
            flip();

        }else if(facingright == true && moveInput < 0){
            flip();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) || dashRepeat < dashRepeatMax){
            Dash();
        }

        
     }
        
    
    
    void Update(){
         
         if(isgrounded == true){
             extrajumps = extrajumpsvalue;
         }
         if(Input.GetKeyDown(KeyCode.UpArrow) && extrajumps > 0){
             rb.velocity = Vector2.up * jumpForce;
             extrajumps--;
         }else if(Input.GetKeyDown(KeyCode.UpArrow) && extrajumps == 0 && isgrounded == true){
             rb.velocity = Vector2.up * jumpForce;


         }
        
    }
         

    void flip(){
        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void Dash(){
        //Vector2 mi = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if(dashRepeat > 0){
            dashRepeat--;
            Vector2 DashVector = new Vector2(dashdistanceF,(rb.position.y));
            if (facingright){
            rb.MovePosition(rb.position + (DashVector * Time.fixedDeltaTime));
            }else{
            rb.MovePosition(rb.position + (DashVector* -1 * Time.fixedDeltaTime));
            }
        
        }else{
            dashRepeat = dashRepeatMax;
        }
        
    }
    
    
}
