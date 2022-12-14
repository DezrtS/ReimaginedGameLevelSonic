using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Controller : MonoBehaviour
{
    public Animator animator;

    public static Controller instance;

    [SerializeField] public float MovementSpeed;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float stoppingDistance;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask gameOverLayer;

    [SerializeField] private Sprite regularSonic;
    [SerializeField] private Sprite runningSonic;
    [SerializeField] private Sprite jumpingSonic;

    private bool running;
    private bool jumping;

    private Rigidbody2D rb;
    private float gravityScale;

    public GameOver gameOverScreen;

    private bool isGrounded = false;
    private bool canWalk = true;
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

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
        if (canWalk)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(Input.GetAxis("Horizontal")) + 1);
            Walking();
        } else
        {
            animator.SetBool("isRunning", false);
        }
        if (canJump)
        {
            Jumping();
        }
        if (PlayerDash.instance.IsDashing())
        {
            animator.SetBool("isDashing", true);
        }
        else
        {
            animator.SetBool("isDashing", false);
        }
    }

    private void Walking()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            MovementSpeed = 10;
        }

        if (Input.GetKey(KeyCode.D)) // detect while walking is the player input
        {
            animator.SetBool("isRunning", true);
            if (MovementSpeed <= maxSpeed)
            {
               MovementSpeed *= acceleration;
            }
            transform.position += transform.right * Time.deltaTime * MovementSpeed; // Time.deltaTime, it does not depend on the performance of your computer
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z); // set the rotation of game object
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            var numberOfLoops = 0;
            float timepast = 0;
            if (MovementSpeed <= maxSpeed)
            {
                MovementSpeed *= acceleration;
            }

            transform.position += transform.right * Time.deltaTime * MovementSpeed;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z); //change y rotation to 180
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", false);
        }

        //
        // Alternate Form of Movement
        //
        /*if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = Vector2.zero;
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (Mathf.Abs(rb.velocity.x + acceleration) >= maxSpeed)
            {
                rb.AddForce(new Vector2(maxSpeed - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(acceleration, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = Vector2.zero;
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (Mathf.Abs(rb.velocity.x - acceleration) >= maxSpeed)
            {
                rb.AddForce(new Vector2(-maxSpeed - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-acceleration, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector2.zero;
        }*/
    }

    private void Jumping()
    {
        CheckIfGrounded();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlaySound("Jump");
            rb.AddForce(transform.up * jumpVelocity, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        rb.gravityScale = gravityScale;
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private bool CheckIfPlayerShouldStop(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stoppingDistance, wallLayer);

        if (hit.collider) return true;
        else return false;
    }

    public void DisableMovement(bool walking, bool jumping)
    {
        canWalk = walking;
        canJump = jumping;
        if (!walking && !jumping)
        {
            animator.SetBool("isRolling", true);
        } else
        {
            animator.SetBool("isRolling", false);
        }
    }

    public void ResetMovementSpeed()
    {
        MovementSpeed = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene("End");
        } else if (collision.gameObject.layer == 10)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
