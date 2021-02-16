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
    public float ledgeSpeed;

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
    public GameObject hips;
    public Transform orientation;
    public CapsuleCollider col;
    public Camera cam;
    public Animator anim;
    [Header("wall layers")]
    public LayerMask wall;
    public LayerMask ledgeLayer;
    [Header("Action Values")]
    public float vaultingSpeed=1.5f;
    public bool ledge;
    public Transform handRayCastPivot;
    [Header("Player Colliders")]
    public CapsuleCollider isTrigger;
    public CapsuleCollider notTrigger;


    public Vector3 lookitsavector;
    //private
    private float slidegSpeed;
    private float movementSpeed;
    private float inputX;
    private float inputY;
    private float inputZ;
    private float speed;
    private float allowPlayerRotation;
    private float timer;

    private float curentYPos;

    private int jumpCount;

    private bool isCrouch;
    private bool sliding;
    private bool isGrounded;
    private bool getsInput;

    private bool isWallRunning;
    private bool wallLeft;
    private bool wallRight;

    private bool slerpValueOn;
    private bool freezeRot;

    private bool gotYValue;

    private Vector3 desireMovementDirection,lookAt;

    private CapsuleCollider normalColSize;
    private RaycastHit hit;
    private WallCollision wc;
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
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        PlayerRotUpdate();
        PlayerInput();
        CheckForWall();
        TimeUpdate();
        AtionUpdator();
    }
    public void FixedUpdate()
    {
        Movement();
    }
    void PlayerRotation()
    {
        if (!freezeRot)
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
    }
    public void AtionUpdator()
    {
        //vaut
        if (slerpValueOn)
        {
            transform.position = Vector3.Lerp(transform.position, wc.destenation.position, vaultingSpeed * Time.deltaTime);
        }
        //ledge
        if (ledge)
        {
            RaycastHit forwardHit;
            RaycastHit forwardHitL;
            RaycastHit forwardHitR;

            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f,ledgeLayer))
            {
                Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);
                transform.rotation = rotation;
                player.transform.rotation = rotation;
                float dist = Vector3.Distance(forwardHit.point, handRayCastPivot.transform.position);
             //   transform.position += Vector3..*dist;
                //123
                //hier distance fixen disstance + dinges murtje herres
            }
            else
            {
                ResetValues();
            }
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitL, 1f, ledgeLayer))
            {
                print("hasright");
            }
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitR, 1f, ledgeLayer))
            {
                print("hasleft");
            }
        }
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
        RaycastHit ledgeCheck;
        if (Input.GetKeyDown("left ctrl")&&isGrounded)
        {
            isCrouch = !isCrouch;
            Crouch();
        }
        if (im.spacebar)
        {
            if (wc.vault&&isGrounded)
            {
                Vault();
                print("Vault");
            }
            else if (ledge&&!im.forwardPressed)
            {
                ResetValues();
            }
            else if (ledge && im.forwardPressed)
            {
                LedgeCLimb();
            }
            else if(wc.medium)
            {
                MediumWall();
                print("medium");
            }
            else if (wc.large)
            {
                BigWall();
                print("large");
            }
            else if (wc.lege&&!isGrounded&& (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer)))
            {
                LedgeGrab();
                print("bam");
            }
            else if (!ledge&&!wc.large&&!wc.medium&&!wc.vault)
            {
                Jump();
                print("bamfasfa");
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
        rb.useGravity = true;
    }
    public void Movement()
    {
        if (!ledge)
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !slerpValueOn)
            {
                transform.Translate(movementVec * Time.deltaTime * movementSpeed);
                transform.Translate(jumpVec * Time.deltaTime * walkingSpeed);
            }
            if (sliding && !isWallRunning && !slerpValueOn)
            {
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * slidegSpeed);
                transform.Translate(jumpVec * Time.deltaTime * walkingSpeed);
            }
        }
        else
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
            anim.SetFloat("ledgeVelocity", Input.GetAxisRaw("Horizontal")+1);
            if (!gotYValue)
            {
            GetYAxees();
            }
            var jumpVec = new Vector3(0, curentYPos, 0);
            lookitsavector = jumpVec;

            transform.Translate(movementVec * Time.deltaTime * ledgeSpeed);
            var curentPos =new Vector3(transform.position.x, curentYPos, transform.position.z);
            transform.position = curentPos;
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
            anim.SetBool("isJumping", true);
            inputY +=jumpForce;
            jumpCount--;
            isCrouch = false;
        }
    }
    public void Vault()
    {
        ResetAnim();
        anim.SetBool("isVaulting", true);
        playerRB.isKinematic = false;
        Invoke("ResetValues", 1f);
        slerpValueOn = true;
        freezeRot = true;
        isTrigger.height = 0.9370761f;
        isTrigger.center = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
        notTrigger.height = isTrigger.height;
        notTrigger.center= isTrigger.center;
    }
    public void MediumWall()
    {
        ResetAnim();
        anim.SetBool("isCliming", true);
        playerRB.isKinematic = false;
        Invoke("ResetValues", 1.3f);
        slerpValueOn = true;
        isTrigger.height = 0.9370761f;
        isTrigger.center = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
        notTrigger.height = isTrigger.height;
        notTrigger.center = isTrigger.center;
    }
    public void BigWall()
    {

    }
    public void LedgeGrab()
    {
        ResetAnim();
        anim.SetBool("isLedgeGrabbing", true);
        ledge = true;
        freezeRot = true;
        rb.useGravity = false;
    }
    public void LedgeCLimb()
    {
        ResetValues();
        print("climb");
    }
    public void UpForce()
    {
        inputY += jumpForce;
        sliding = true;
        float x = movementSpeed;
        slidegSpeed = slidingSpeed * x / 1.5f * 1.5f;
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
    public void GetYAxees()
    {
        gotYValue = true;
        curentYPos = transform.position.y;
    }
    public void ResetAnim()
    {
        anim.SetFloat("ledgeVelocity", 1);
        anim.SetBool("isJumping", false);
        anim.SetBool("isCrouching", false);
        anim.SetBool("isSliding", false);
        anim.SetBool("isVaulting", false);
        anim.SetBool("isCrouching", false);
        anim.SetBool("isCliming", false);
        anim.SetBool("isLedgeGrabbing", false);
    }
    public void ResetValues()
    {
        ResetAnim();
        sliding = false;
        playerRB.isKinematic = true;
        slerpValueOn = false;
        rb.useGravity = true;
        freezeRot = false;
        ledge = false;
        gotYValue = false;

        isTrigger.height = 1.592172f;
        isTrigger.center = new Vector3(-0.01670074f, 0.8977467f, 0.0531683f);
        notTrigger.height = isTrigger.height;
        notTrigger.center = isTrigger.center;
    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.15f, 0.0f)), transform.forward, Color.yellow);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.15f, 0.0f)), transform.forward, Color.blue);
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.15f, 0.0f)), transform.forward, Color.blue);

    }
}