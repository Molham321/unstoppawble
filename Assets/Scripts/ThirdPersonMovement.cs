using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [Header("Movement")]
    public float midAirSpeed = 35f;
    public float normalMidAirSpeed = 35f;
    public float normalMaxSpeed = 30f; 
    public float maxSpeed = 30f;
    public float normalSpeed = 45;  //needs to be the same as "speed"
    [SerializeField] private float speed = 45f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTimeConter;
    [SerializeField] private bool isJumping;
    [SerializeField] public float jumpTime;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask slopeMask;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isOnSlope;

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private AudioSource runAudio;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    public Vector3 playerMovementInput;

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;

        playerAudio = GetComponent<AudioSource>();
        runAudio = GetComponent<AudioSource>();
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

 

        if (!isGrounded)
        {
            speed = midAirSpeed;
        }

        if (isGrounded)
        {
            speed = normalSpeed;
        }

        MovePlayer();
        JumpPlayer();
        FellDown();
    }


    private void FixedUpdate()
    {
        if(playerRb.velocity.magnitude >= maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }
    }

    private void MovePlayer()
    {
        //----------------Movement----------------------------------
        if (playerMovementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(playerMovementInput.x, playerMovementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (isGrounded)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 MoveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            playerRb.AddForce(MoveVector.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
         
        }

        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        //----------------Falling----------------------------------
        if (playerRb.velocity.y < -0.5f)
        {
            if(!isGrounded && playerMovementInput.magnitude >= 0.1f)
            {
                animator.SetBool("isRunning", true);
            }

            animator.SetBool("isFalling", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isFalling", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Slope")
        {
            playerRb.drag = 0;
            playerRb.angularDrag = 0;
            maxSpeed = 40;
            midAirSpeed = 40;

            animator.SetBool("isSliding", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }


        if (collision.gameObject.tag == "Checkpoint")
        {
            playerRb.drag = 0.85f;
            playerRb.angularDrag = 0.05f;
            normalMaxSpeed = maxSpeed;
            normalMidAirSpeed = midAirSpeed;

            animator.SetBool("isSliding", false);
            animator.SetBool("afterSlide", true);
        }
    }

    private void JumpPlayer()
    {
        //----------------Jumping----------------------------------

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {

            isJumping = true;
            jumpTimeConter = jumpTime;
            playerRb.velocity = Vector3.up * jumpForce;

            playerAudio.PlayOneShot(jumpSound, 0.7f);
        }

        if (isGrounded == true)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeConter > 0)
            {
                playerRb.velocity = Vector3.up * jumpForce;
                jumpTimeConter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (isJumping == false && !isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

        //speed = (!isGrounded) ? 15 : 20;
    }

    //----------------Enenmy throwback----------------------------------
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 dir = collision.transform.position - transform.position;
            playerRb.AddForce(dir * 50f);
        }
    }

    void FellDown()
    {
        if (transform.position.y < -100)
        {
            playerAudio.PlayOneShot(fallSound, 0.4f);
        }
        //----------------Fell Down GameOver----------------------------------
        if (transform.position.y < -150)
        {
            FindObjectOfType<GameManager>().EndGame();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //StartCoroutine(FellToGameOver());
        }
    }

    //IEnumerator FellToGameOver()
    //{
    //    playerAudio.PlayOneShot(fallSound, 0.7f);
    //    yield return new WaitForSeconds(2);

    //}
}
