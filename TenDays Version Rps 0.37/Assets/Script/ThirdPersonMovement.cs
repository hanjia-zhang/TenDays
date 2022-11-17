using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    Vector3 velocity;
    bool isGrounded;
    public bool LockCursor = true;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float jumpTime = 1.0f;


    
    //Interaction Components
    PlayerInteraction playerInteraction;

    

    void Start()
    {
        // Lock the cursor
        if (LockCursor){
            Cursor.lockState = CursorLockMode.Locked;
        }
     
        // Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteraction>();     
        
    }


    void Update()
    {
        // Runs the function that handles all interaction
        Interact();

        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
            
        }
        if (Input.GetButtonDown("Jump")&&isGrounded)
        {
            Invoke("Jump",jumpTime);
            
            
        }
        

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    public void Interact()
    {
        // Farming tool interaction
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerInteraction.Interact();
        }

        // Item interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInteraction.ItemInteract();
        }

    }

    


}
