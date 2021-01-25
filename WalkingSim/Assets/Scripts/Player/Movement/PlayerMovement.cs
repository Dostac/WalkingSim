using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    ///public
    public float walkingspeed = 1f;
    public float runningSpeed = 2f;
    public float downForce = -1f;
    public float jumpforce, crouchHeight, bodyHeight, groundedCooldown, gravity;
    public Camera cam;
    public CharacterController controller;
    public InputManager im;
    ///private
    private CharacterController _controller;
    private Vector3 moveDir;
    private float _directionY,speed;

    private bool _canDoubleJump = false;
    private int jumpcount;

    void Update()
    {
        //UpdateUpdate();
        InPut();
    }
    private void InPut()
    {
        //sprint
        if (im.runPressed)
        {
            speed = runningSpeed;
        }
        else
        {
            speed = walkingspeed;
        }

        //crouch
        //if (Input.GetButton("Crouch"))
        //{
        //    GetComponent<CharacterController>().height = crouchHeight;
        //}
        //else
        //{
        //    GetComponent<CharacterController>().height = bodyHeight;
        //}

        //gravity
        if (controller.isGrounded)
        {
            if (Time.time == groundedCooldown + Time.time)
            {
                downForce = -0.01f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpcount >= 0)
                {
                    downForce = jumpforce;
                }
            }
        }
        else
        {
            downForce -= gravity * Time.deltaTime;
            //jump
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpcount > 0)
                {
                    jumpcount--;
                    downForce = jumpforce;
                }
            }
        }
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        moveDir = transform.TransformDirection(moveDir);
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * speed * Time.deltaTime);

    } 
}