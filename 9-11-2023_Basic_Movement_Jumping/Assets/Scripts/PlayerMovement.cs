using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float movementSpeed;
    private float inputHorizontal;
    public float jumpforce;
 
    private int numJumps;
    private int maxnumJumps;
    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Start");
        playerRigidBody = GetComponent<Rigidbody2D>();//only use get component when script is tied to the object you want
        numJumps = 1;
        maxnumJumps = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        //Basic Movement not ideal
        //moves an object but will ignore collisions 
        //transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        movePLayerLateral();
        jump();
    }
    private void movePLayerLateral()
    {
        //Value rturned will be 0, 1, or -1 depending on what button is pressed
        // no button pressed 0, righ arrow 1, left arrow -1
        // Horizontal is defined in the input section of project settings
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);
        flipPlayer();
    }

    private void flipPlayer()
    {
        if(inputHorizontal > 0) 
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (inputHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && numJumps <= maxnumJumps) 
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpforce);
            numJumps++;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OB"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (collision.gameObject.CompareTag("Grounded"))
        {
            numJumps = 1;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            maxnumJumps = 2;
            Destroy(collision.gameObject);
        }

    }
}
