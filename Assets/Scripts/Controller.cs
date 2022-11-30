using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Controller : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float stoppingDistance;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private float gravityScale;

    public GameOver gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (MovementSpeed > maxSpeed)
        {
            MovementSpeed = maxSpeed;
        }
        Walking();
        Jumping();
    }

    private void Walking()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            MovementSpeed = 10;
        }
        if (Input.GetKey(KeyCode.D)) // detect while walking is the player input
        {
            if (MovementSpeed <= maxSpeed)
            {
               MovementSpeed *= acceleration;
            }
            transform.position += transform.right * Time.deltaTime * MovementSpeed; // Time.deltaTime, it does not depend on the performance of your computer
            transform.rotation = Quaternion.Euler(0, 0, 0); // set the rotation of game object
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            var numberOfLoops = 0;
            float timepast = 0;
            if (MovementSpeed <= maxSpeed)
            {
                MovementSpeed *= acceleration;
            }

            transform.position += transform.right * Time.deltaTime * MovementSpeed;
            transform.rotation = Quaternion.Euler(0, 180, 0); //change y rotation to 180
        }
    }

    private void Jumping()
    {
        var isGrounded = CheckIfGrounded();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpVelocity, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        rb.gravityScale = gravityScale;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider) return true;


        else return false;
    }

    private bool CheckIfPlayerShouldStop(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stoppingDistance, wallLayer);

        if (hit.collider) return true;
        else return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOver")
        {
            SceneManager.LoadScene("End");
        }
    }
}
