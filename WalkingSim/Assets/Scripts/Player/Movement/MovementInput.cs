using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    //public
    public float walkingspeed = 1f;
    public float runningSpeed = 2f;
    public float downForce = -1f;
    public float InputX;
    public float InputZ;
    public float jumpforce= 2.1f;
    public float gravity= 4.68f;
    public float groundedCooldown= 4;
    public Vector3 desireMovementDirection;
    public bool blockRotationPlayer;
    public float desiredRoationSpeed= 0.1f;
    public Animator anim;
    public InputManager im;
    public float speed;
    public float Movspeed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    public float crouchHeight, bodyHeight, slideHeight;
    public Vector3 centerNeutral, centerCrouch, centerSlide;
    public Transform orientation;
    public float wallGravity;
    public LayerMask wall;
    ///private
    private bool sliding, isCrouching, isWallRight, isWallLeft, isWallRunning;
    private Vector3 moveVector, lastMove;
    private int jumpcount = 1;
    void Update()
    {
        CheckForWall();
        WallRunInput();
        InputMagnitude();
    }
    private void WallRunInput()
    {
        //Wallrun
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();
    }
    private void StartWallrun()
    {
        isWallRunning = true;
        //Make sure char sticks to wall
        // if (isWallRight)
        // rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
        // else
        // rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
    }
    private void StopWallRun()
    {
        isWallRunning = false;
    }
    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, wall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wall);

        //leave wall run
        if (!isWallLeft && !isWallRight) StopWallRun();
    }
    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ= Input.GetAxisRaw("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desireMovementDirection = forward * InputZ + right * InputX;
        if (!blockRotationPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desireMovementDirection),desiredRoationSpeed); 
        }
    }
    void InputMagnitude()
    {
        if (im.runPressed)
        {
            Movspeed = runningSpeed;
        }
        else
        {
            Movspeed = walkingspeed;
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
        if (isWallRight && isWallRunning)
        {
            Vector3 v = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, v.y, 15);
        }
        if (isWallLeft && isWallRunning)
        {
            Vector3 v = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, v.y, -15);
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
                    anim.SetBool("isJumping", true);
                    Invoke("ResetAnim", 1);
                    downForce = jumpforce;
                }
            }
        }
        else
        {
           moveVector = lastMove;

            downForce -= gravity * Time.deltaTime;
        }
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ = Input.GetAxisRaw("Vertical");
        moveVector = new Vector3(InputX, 0, InputZ);
        ///0.0 kan naar 0.3 als je niet meteen animatie wil
        //anim.SetFloat("inputz",InputZ,0.0f,Time.deltaTime*2f);
        //anim.SetFloat("inputx", InputX, 0.0f, Time.deltaTime * 2f);

        speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (speed > allowPlayerRotation)
        {
            //anim.SetFloat("inputMagnitude", speed,0.0f,Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            //anim.SetFloat("inputMagnitude", speed, 0.0f, Time.deltaTime);
        }
        if (InputX == 0 && InputZ == 0)
        {
            desireMovementDirection = new Vector3(0, 0, 0);
        }
        moveVector = desireMovementDirection;
        controller.Move(moveVector.normalized * Movspeed * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * walkingspeed * Time.deltaTime);
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.gameObject.layer == 10)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ResetAnim();
                anim.SetBool("isJumping", true);
                Invoke("ResetAnim", 1);
                downForce = jumpforce;
                moveVector = hit.normal * speed;
            }
        }
    }
    public void Crouch(bool crouching)
    {
        if (crouching == true)
        {
            isCrouching = true;
            controller.height = crouchHeight;
            controller.center = (centerCrouch);
            anim.SetBool("isCrouching", true);
        }
        if (crouching == false)
        {
            ResetAnim();
        }
    }
    public void Slide()
    {
        sliding = true;
        controller.height = slideHeight;
        controller.center = (centerSlide);
        anim.SetBool("isSliding", true);
        Invoke("ResetAnim", 1);
    }
    public void ResetAnim()
    {
        controller.height = bodyHeight;
        controller.center = (centerNeutral);
        anim.SetBool("isSliding", false);
        anim.SetBool("isCrouching", false);
        anim.SetBool("isJumping", false);
        sliding = false;
    }
}
