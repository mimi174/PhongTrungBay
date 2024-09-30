using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
   [Header("Movement")]
   public float moveSpeed;


   public float groundDrag;


   public float jumpForce;
   public float jumpCooldown;
   public float airMultiplier;
   bool readyToJump;


   [HideInInspector] public float walkSpeed;
   [HideInInspector] public float sprintSpeed;


   [Header("Keybinds")]
   public KeyCode jumpKey = KeyCode.Space;


   [Header("Ground Check")]
   public float playerHeight;
   public LayerMask whatIsGround;
   bool grounded;


   public Transform orientation;


   float horizontalInput;
   float verticalInput;


   Vector3 moveDirection;


   Rigidbody rb;

    [Header("Climbing")]
    public float stairHeight;
    public float climbSpeed; 
    public LayerMask stairsLayer;
   
   float rotationX, rotationY = 0f;
   public float mouseSensitivity = 10f;
   private void Start()
   {
       rb = GetComponent<Rigidbody>();
       rb.freezeRotation = true;


       readyToJump = true;
   }


   private void Update()
   {
       // ground check
       grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
    

       MyInput();
       SpeedControl();
       PlayerRotation();
   }


   private void FixedUpdate()
   {
       MovePlayer();
       HandleClimbing();
   }


   private void MyInput()
   {
       horizontalInput = Input.GetAxisRaw("Horizontal");
       verticalInput = Input.GetAxisRaw("Vertical");
       //when to jump
       if (Input.GetKey(jumpKey) && readyToJump && grounded)
       {
          readyToJump = false;
            Jump();
          Invoke(nameof(ResetJump), jumpCooldown);
       }
   }


   private void MovePlayer()
   {
       // calculate movement direction
       moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


       rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
   }


   private void SpeedControl()
   {
       Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


       // limit velocity if needed
       if (flatVel.magnitude > moveSpeed)
       {
           Vector3 limitedVel = flatVel.normalized * moveSpeed;
           rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
       }
   }


   private void Jump()
   {
       // reset y velocity
       rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


       rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
   }
   private void ResetJump()
   {
       readyToJump = true;
   }


   void PlayerRotation()
   {
       rotationY += Input.GetAxis("Mouse X") * mouseSensitivity * -1;
       rotationX += Input.GetAxis("Mouse Y") * mouseSensitivity * -1;


       rotationX = Mathf.Clamp(rotationX, -90f, 90f);
       orientation.localEulerAngles = new Vector3(rotationX, rotationY, 0);
   }
    
    // private void HandleClimbing()
    // {
    //     // Check if the player is grounded and colliding with stairs
    //     if (grounded && Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, stairsLayer))
    //     {
    //         // Check for stairs
    //         RaycastHit hit;
    //         if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.3f, stairsLayer))
    //         {
    //             // If the height of the stair is less than or equal to stairHeight
    //             if (hit.distance <= stairHeight)
    //             {
    //                 // Allow player to move up the stairs
    //                 rb.AddForce(Vector3.up * climbSpeed, ForceMode.Force);
    //             }
    //         }
    //     }
    // }
    private void HandleClimbing()
    {
        // Check if the player is grounded
        if (grounded)
        {
            // Cast a ray downwards to check for stairs
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.3f, stairsLayer))
            {
                // Check if the height of the stair is less than or equal to stairHeight
                if (hit.distance <= stairHeight)
                {
                    // Allow player to move up the stairs
                    if (verticalInput < 0) // Move up if pressing forward
                    {
                        rb.AddForce(Vector3.up * climbSpeed, ForceMode.Force);
                    }
                }
            }
        }
    }
}



