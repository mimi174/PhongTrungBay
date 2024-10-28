using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gắn cho obj Player
//Xử lý di chuyển của người chơi
public class PlayerMovement1 : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public const float Gravity = 9.8f;
    public float vSpeed = 0;

    public bool canMove = true;
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            controller.Move(Vector3.zero);
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = (transform.right * x + transform.forward * z) * speed;

            //Debug.Log(controller.isGrounded);

            if (controller.isGrounded)
            {
                vSpeed = 0;
            }
            else
            {
                vSpeed -= Gravity * Time.deltaTime;
            }

            move.y = vSpeed;

            controller.Move(move * Time.deltaTime);
        }
    }
}
