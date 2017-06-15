using System.Collections;           //The using__ imports a namespace, these are a collection of classes and other data types that are used to categorize the library. The System.Collections is all the classes in the .net related to holding groups of data.
using System.Collections.Generic;   //The using__ imports a namespace, these are a collection of classes and other data types that are used to categorize the library. The ystem.Collections.Generic is used to hold a different class of object that are not directly identified in original library.
using UnityEngine;                  //The using__ imports a namespace, these are a collection of classes and other data types that are used to categorize the library. UnityEngine in this case is a collection of all the classes related to unity.

public class playerController : MonoBehaviour { //The Monobehaviour is used to tell Unity what class your class is derived from.
 
    public float topSpeed = 10f;    //This is used as a variable to determine the speed the player move at.
    bool facingRight = true;        //This Boolean will act as a trigger to understand if the player is facing right, which is recognized as true, if they go right and false if they are facing left.
    Animator anim;                  //This is used to assign animation clips to animation components and control the playback based on variables and parameters.

    bool grounded = false;	    //
    public Transform groundCheck;   //
    float groundRadius = 0.2f;      //
    public LayerMask whatIsGround;  //
    public float jumpForce = 250f;  //This float is a variable that determines the force that the player character jumps at, this is set to 250.

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Grounded", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
     }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");
        Debug.Log(move);

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
      }

    void Flip()
     {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
       transform.localScale = theScale;
    }
}
