using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public AudioClip jumpSound;

    public float counter = 3;

    public Animator animator;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpForce = 200;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Rigidbody playerRb;
    public bool gameOver;
    bool gameHasEnded = false;

    Vector3 velocity;
    bool isGrounded;

    private AudioSource playerAudio;

    // Start is called before the first frame update
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
            // rigidbody fehlt
        };
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            playerRb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerRb.AddForce(Vector3.up * jumpForce);
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);
            playerAudio.PlayOneShot(jumpSound, 0.7f);

        } else if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isIdle", true);
        }

        //velocity.y += gravity * Time.deltaTime;

        //controller.Move(velocity * Time.deltaTime); 

        if (transform.position.y < -100)
        {
            if (!gameHasEnded)
            {
                gameHasEnded = true;
                Debug.Log("GameOver");
                Restart();
            }
        }
    }

    void Restart()
    {
        if(counter > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            counter--;
        }
    }
}
