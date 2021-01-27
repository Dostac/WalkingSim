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
    public Transform orientation;
    public float wallrunForce=3000;
    public float wallGrafity;
    public LayerMask wall;
    ///private
    private Vector3 moveDir,lastMove,dubbelLastMove;
    private float speed;
    private bool sliding,isCrouching,dubbeljumping, isWallRight, isWallLeft,isWallRunning;

    private int jumpcount = 1;

    void Update()
    {
        //UpdateUpdate();
        CheckForWall();
        WallRunInput();
        InPut();
    }
    private void WallRunInput() 
    {
        //Wallrun
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();
    }
    private void StartWallrun()
    {
        print("start");

        isWallRunning = true;


            //Make sure char sticks to wall
           // if (isWallRight)
               // rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
           // else
               // rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
        //}
    }
    private void StopWallRun()
    {
        isWallRunning = false;        
    }
    private void CheckForWall() //make sure to call in void Update
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, wall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wall);

        //leave wall run
        if (!isWallLeft && !isWallRight) StopWallRun();
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
        if (Input.GetKeyDown("left ctrl"))
        {
            if (!sliding)
            {
                isCrouching = !isCrouching;
                Crouch(isCrouching);
            }
        }
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        if (isWallRight&&isWallRunning)
        {
            transform.rotation = Quaternion.Euler(cam.transform.forward.x, transform.forward.y, 15);
        }
        if (isWallLeft&& isWallRunning)
        {
            transform.rotation = Quaternion.Euler(cam.transform.forward.x, transform.forward.y, -15);
        }
        moveDir = transform.TransformDirection(moveDir);
        if (controller.isGrounded)
        {
            dubbeljumping = false;
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
            if (!dubbeljumping)
            {
            moveDir = lastMove;
            }
            downForce -= gravity * Time.deltaTime;
            //dubbeljump
            //if (Input.GetButtonDown("Jump"))
            //{
            //    if (jumpcount > 0)
            //    {
            //        dubbeljumping = true;
            //        ResetAnim();
            //        anim.SetBool("IsJumping", true);
            //        Invoke("ResetAnim", 1);
            //        jumpcount--;
            //        downForce = jumpforce;
            //        moveDir = dubbelLastMove;
            //        print("2");
            //    }
            //}
        }
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * walkingspeed * Time.deltaTime);
        lastMove = moveDir;
        if (dubbeljumping)
        {
            dubbelLastMove = moveDir;
        }
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded&& hit.gameObject.layer == 10)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ResetAnim();
                anim.SetBool("IsJumping", true);
                Invoke("ResetAnim", 1);
                downForce = jumpforce;
                moveDir = hit.normal * speed;
            }
        }
    }
    public void Crouch(bool crouching)
    {
        if (crouching==true)
        {
            isCrouching = true;
            controller.height = crouchHeight;
            controller.center = (centerCrouch);
            anim.SetBool("IsCrouching", true);
        }
        if (crouching==false)
        {
            ResetAnim();
        }
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
        anim.SetBool("IsCrouching", false);
        anim.SetBool("IsJumping", false);
        sliding = false;
    }
}