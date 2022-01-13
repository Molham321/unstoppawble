using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody playerRb;
    [Space]
    [SerializeField] private float speed = 20;
    [SerializeField] private float jumpForce = 200;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip jumpSound;

    private Vector3 playerMovementInput;

    private bool gameOver = false;

    private float groundDistance = 0.4f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Start()
    {
        if (!TryGetComponent<Rigidbody>(out playerRb))
        {
            Destroy(this);
        }

        try
        {
            playerRb = GetComponent<Rigidbody>();
        } 
        catch (Exception e) 
        {
            Debug.LogError(e);
        };
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized; 

        MovePlayer();
        FellDown();
    }

    private void MovePlayer()
    {
        //----------------Movement----------------------------------
        if (playerMovementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(playerMovementInput.x, playerMovementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 MoveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            playerRb.MovePosition(transform.position + MoveVector.normalized * speed * Time.deltaTime);
        }

        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        //----------------Jumping----------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJumping", true);
                animator.SetBool("isIdle", false);
                playerAudio.PlayOneShot(jumpSound, 0.7f);
            }

        }

        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isIdle", true);
        }

        //----------------Falling----------------------------------
       if (playerRb.velocity.y < -0.5f)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isFalling", false);
        }
    }

    //----------------Enenmy throwback----------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 dir = collision.transform.position - transform.position;
            playerRb.AddForce(dir * 100f);
        }
    }

    void FellDown()
    {
        //----------------Fell Down GameOver----------------------------------
        if (transform.position.y < -100)
        {
            if (!gameOver)
            {
                gameOver = true;
                Debug.Log("GameOver");
                Restart();
            }
        }
    }

    void Restart()
    {
        //----------------Restart Game----------------------------------name
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
