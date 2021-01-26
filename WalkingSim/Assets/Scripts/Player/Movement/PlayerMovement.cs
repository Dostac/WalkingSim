using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    ///public
    public float walkingspeed = 1f;
    public float runningSpeed = 2f;
    public float downForce = -1f;
    public float jumpforce, groundedCooldown, gravity;
    public Camera cam;
    public CharacterController controller;
    public InputManager im;
    public Animator anim;
    public float crouchHeight, bodyHeight, slideHeight;
    public Vector3 centerNeutral, centerCrouch, centerSlide;
    ///private
    private Vector3 moveDir;
    private float speed;
    private bool sliding,isCrouching;

    private int jumpcount = 1;

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
        if (Input.GetKeyDown("c"))
        {
            Slide();
        }
        if (Input.GetKey("left ctrl"))
        {
            if (!sliding)
            {
                isCrouching =true;
                controller.height = crouchHeight;
                controller.center = (centerCrouch);
                anim.SetBool("IsCrouching", true);
            }
        }
        else
        {
            if (!sliding)
            {
                isCrouching = false;
                controller.height = bodyHeight;
                controller.center = (centerNeutral);
            }
        }
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
                    ResetAnim();
                    anim.SetBool("IsJumping", true);
                    Invoke("ResetAnim", 1);
                    downForce = jumpforce;
                }
            }
            jumpcount = 1;
        }
        else
        {
            downForce -= gravity * Time.deltaTime;
            //jump
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpcount > 0)
                {
                    ResetAnim();
                    anim.SetBool("IsJumping", true);
                    Invoke("ResetAnim", 1);
                    jumpcount--;
                    downForce = jumpforce;
                }
            }
        }
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        moveDir = transform.TransformDirection(moveDir);
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * walkingspeed * Time.deltaTime);
    }
    public void Slide()
    {
        sliding = true;
        controller.height = slideHeight;
        controller.center = (centerSlide);
        anim.SetBool("IsSliding", true);
        Invoke("ResetAnim", 1);
    }
    public void ResetAnim()
    {
        controller.height = bodyHeight;
        controller.center = (centerNeutral);
        anim.SetBool("IsSliding", false);
        anim.SetBool("IsJumping", false);
        sliding = false;
    }
}