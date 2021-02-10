using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ///public
     [Header("Speed Values")]
    public float desiredRoationSpeed;
    public float slidingSpeed;
    public float runningSpeed;
    public float walkingSpeed;
    public float crouchingSpeed;
    public float runningCrouchingSpeed;

    [Header("Jump Values")]
    public float jumpForce;
    public int jumps;
    [Header("Time Values")]
    public float slidingTime=1;
    public float wallRunningTime=2.5f;

    [Header("Wall Values")]
    public float maxWallSpeed;
    public float wallrunForce;
    public float wallRunAngle=60;

    [Header("Player Components")]
    public GameObject player;
    public Transform orientation;
    public CapsuleCollider col;
    public Camera cam;
    public Animator anim;
    [Header("wall layers")]
    public LayerMask wall;

    //private
    private float slidegSpeed;
    private float movementSpeed;
    private float inputX;
    private float inputY;
    private float inputZ;
    private float speed;
    private float allowPlayerRotation;
    private float timer;

    private int jumpCount;

    private bool isCrouch;
    private bool sliding;
    private bool isGrounded;
    private bool getsInput;

    private bool isWallRunning;
    private bool wallLeft;
    private bool wallRight;

    private Vector3 desireMovementDirection,lookAt;

    private WallCollision wc;
    private RaycastHit hit;
    private InputManager im;
    private Rigidbody rb;
    private Rigidbody playerRB;
    private void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        jumpCount = jumps;
        wc=player.GetComponent<WallCollision>();
        playerRB=player.GetComponent<Rigidbody>();
    }
    public void Update()
    {
        PlayerRotUpdate();
        PlayerInput();
        CheckForWall();
        TimeUpdate();
    }
    public void FixedUpdate()
    {
        Movement();
    }
    void PlayerRotation()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desireMovementDirection = forward * inputZ + right * inputX;
        if (!isWallRunning)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(desireMovementDirection), desiredRoationSpeed);
        }
        transform.forward = new Vector3(cam.transform.forward.x, cam.transform.forward.y, cam.transform.forward.z);
        Vector3 v = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, v.y, 0);
    }
    public void PlayerRotUpdate()
    {
        speed = new Vector2(inputX, inputZ).sqrMagnitude;
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        if (speed > allowPlayerRotation)
        {
            PlayerRotation();
        }
    }
    public void PlayerInput()
    {
        if (Input.GetKeyDown("left ctrl")&&isGrounded)
        {
            isCrouch = !isCrouch;
            Crouch();
        }
        if (im.spacebar)
        {
            if (wc.vault)
            {
                Vault();
                print("Vault");
            }
            else if(wc.medium)
            {
                print("medium");
                MediumWall();
            }
            else if (wc.large)
            {
                print("large");
                BigWall();
            }
            else if (wc.lege)
            {
                print("ledge");
                LedgeGrab();
            }
            else if (!wc.lege&&!wc.large&&!wc.medium&&!wc.vault)
            {
                Jump();
            }
        }
        if (Input.GetKeyDown("c"))
        {
            Slide();
        }
        if (!isCrouch && !im.forwardPressed && !im.backwardsPressed && !im.leftPressed && !im.rightPressed)
        {
            getsInput = false;
        }
        else
        {
            getsInput = true;
        }
        if (im.runPressed&&!sliding&&!isCrouch&& getsInput)
        {
            movementSpeed = runningSpeed;      
        }
        if (!im.runPressed && !sliding&&!isCrouch&& getsInput)
        {
            movementSpeed = walkingSpeed;
        }
        if (im.runPressed&&!sliding && isCrouch&& getsInput)
        {
            movementSpeed = runningCrouchingSpeed;
        }
        if (!im.runPressed && !sliding && isCrouch&& getsInput)
        {
            movementSpeed = crouchingSpeed;
        }
        if(sliding&& !getsInput)
        {
            movementSpeed = walkingSpeed/2;
        }
    }
    public void Slide()
    {
        isCrouch = false;
        ResetAnim();
        anim.SetBool("isSliding", true);
        sliding = true;
        float x = movementSpeed;
        slidegSpeed =slidingSpeed * x / 1.5f*1.5f;
        Invoke("ResetValues", slidingTime);
    }
    public void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, 1f, wall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wall);

        if (wallRight && !isGrounded)
        {
            StartWallRun();
            Physics.Raycast(transform.position, orientation.right, out hit);
            Vector3 v = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, v.y, wallRunAngle), desiredRoationSpeed);
        }
        if (wallLeft && !isGrounded)
        {
            StartWallRun();
            Physics.Raycast(transform.position, -orientation.right, out hit);
            Vector3 v = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, v.y, -wallRunAngle), desiredRoationSpeed);
        }
        if (isWallRunning&&!wallLeft && !wallRight)
        {
            EndWallRun();
        }
    }//-72.45
    public void StartWallRun()
    {
        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(transform.forward * wallrunForce * Time.deltaTime);
            if (wallRight)
            {
                rb.AddForce(transform.right * wallrunForce / 5 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(-transform.right * wallrunForce / 5 * Time.deltaTime);
            }
        }
        ///transfrom kan oritation moeten tereug dingens
        jumpCount = 2;
        isWallRunning = true;
        rb.useGravity = false;
        if (wallRight)
        {
            lookAt = Vector3.Cross(-hit.normal, transform.right);
        }
        else if (wallLeft)
        {
            lookAt = Vector3.Cross(hit.normal, transform.right);
        }
        //player.transform.LookAt(lookAt);
        ///ditte normal ding
    }
    public void EndWallRun()
    {
        isWallRunning = false;
        print("endwallrunning");
        rb.useGravity = true;
    }
    public void Movement()
    {      
        var movementVec= new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        var jumpVec = new Vector3(0, inputY, 0);
        if (!sliding)
        {
        transform.Translate(movementVec * Time.deltaTime * movementSpeed);
        transform.Translate(jumpVec * Time.deltaTime * walkingSpeed);
        }
        if (sliding && !isWallRunning)
        {
            transform.Translate(new Vector3(0,0,1) * Time.deltaTime * slidegSpeed);
            transform.Translate(jumpVec * Time.deltaTime * walkingSpeed);
        }
    }
    public void TimeUpdate()
    {
        if (isWallRunning)
        {
            if(timer< wallRunningTime)
            {
                timer +=Time.deltaTime;
            }
            else if(timer >= wallRunningTime)
            {
                 EndWallRun();
            }
        }
        else
        {
            timer = 0;
        }
    }
    public void Crouch()
    {
        if (isCrouch)
        {
            anim.SetBool("isCrouching", true);
        }
        if (!isCrouch)
        {
            anim.SetBool("isCrouching", false);
        }
    }
    public void Jump()
    {
        if (jumpCount >= 1)
        {
            ResetAnim();
            anim.SetBool("isjumping", true);
            inputY +=jumpForce;
            jumpCount--;
        }
    }
    public void LedgeGrab()
    {
        //rb.useGravity = false;
        //inputY = 0;
    }
    public void BigWall()
    {

    }
    public void MediumWall()
    {
       
    }
    public void Vault()
    {
   
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            isGrounded = true;
            jumpCount = jumps;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            inputY = 0;
        }
    }
    public void FreezePlayerRot()
    {
        player.GetComponent<Rigidbody>().isKinematic = playerRB.isKinematic;
    }
    public void UNFreezePlayerRot()
    {
        player.GetComponent<Rigidbody>().isKinematic = rb.isKinematic;
    }
    public void ResetAnim()
    {
        anim.SetBool("isjumping", false);
        anim.SetBool("isCrouching", false);
        anim.SetBool("isSliding", false);
    }
    public void ResetValues()
    {
        ResetAnim();
        sliding = false;
    }
}