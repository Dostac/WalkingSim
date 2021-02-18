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
    public bool balancebar;
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
    private float runningSpeedAftherRun;
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
        runningSpeedAftherRun = runningSpeed;
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

            RaycastHit leftSideHit;
            RaycastHit rightSideHit;
            //123
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f,ledgeLayer))
            {
                Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);
                transform.rotation = rotation;
                player.transform.rotation = rotation;
            }
            else
            {                                  
                if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, out leftSideHit, 1f, ledgeLayer))
                {
                    Quaternion rotation = Quaternion.LookRotation(-leftSideHit.normal, Vector3.up);
                    transform.rotation = rotation;
                    transform.position = leftSideHit.point;
                }
                else if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, out rightSideHit, 1f, ledgeLayer))
                {
                    Quaternion rotation = Quaternion.LookRotation(-rightSideHit.normal, Vector3.up);
                    transform.rotation = rotation;
                    transform.position = rightSideHit.point;
                }
                else if(!wc.lege)
                {
                    ResetValues();
                }
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
        if (Input.GetKeyDown("left ctrl"))
        {
            if (isGrounded && wc.balancingBar)
            {
                BalancingBar();
            }
            else if (!balancebar && isGrounded)
            {
                isCrouch = !isCrouch;
                Crouch();
            }
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
            }
            else if (wc.large)
            {
                BigWall();
                print("large");
            }
            else if (wc.lege&&!isGrounded&& (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer)))
            {
                LedgeGrab();
            }
            else if (!ledge&&!wc.large&&!wc.medium&&!wc.vault)
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
        isTrigger.height = 0.7743956f;
        isTrigger.center = new Vector3(-0.01670074f, 0.4888585f, 0.0531683f);
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
            player.transform.localPosition = new Vector3(0.278f, -0.233f, 0.003f);
        }
        if (wallLeft && !isGrounded)
        {
            StartWallRun();
            Physics.Raycast(transform.position, -orientation.right, out hit);
            Vector3 v = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, v.y, -wallRunAngle), desiredRoationSpeed);
            player.transform.localPosition = new Vector3(-0.123F, -0.196f, 0.2f);
        }
        if (isWallRunning&&!wallLeft && !wallRight)
        {
            EndWallRun();
        }
    }
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
        player.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        runningSpeedAftherRun -= 1;
    }
    public void Movement()
    {
        if (!ledge & !balancebar)
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !slerpValueOn)
            {
                transform.Translate(movementVec * Time.deltaTime * movementSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeedAftherRun);
            }
            if (sliding && !isWallRunning && !slerpValueOn)
            {
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * slidegSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeedAftherRun);
            }
            //var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), inputY, Input.GetAxisRaw("Vertical")).normalized;


            //rb.AddRelativeForce(movementVec * movementSpeed);
            //Vector3 v = player.transform.rotation.eulerAngles;
            //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(v), 6);
            ////movementSpeed
        }
        else if (ledge & !balancebar)
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
            anim.SetFloat("ledgeVelocity", Input.GetAxisRaw("Horizontal") + 1);
            if (!gotYValue)
            {
                GetYAxees();
            }
            var jumpVec = new Vector3(0, curentYPos, 0);
            lookitsavector = jumpVec;

            transform.Translate(movementVec * Time.deltaTime * ledgeSpeed);
            var curentPos = new Vector3(transform.position.x, curentYPos, transform.position.z);
            transform.position = curentPos;
        }
        else if (!ledge && balancebar)
        {
            if (!wc.balancingBar&&wc.balanceBegin || !wc.balancingBar && wc.balanceEnd)
            {
                ResetValues();
            }
            var movementVec = new Vector3(0 ,0, Input.GetAxisRaw("Vertical")).normalized;
            anim.SetFloat("balancingVelocity", Input.GetAxisRaw("Vertical") + 1);
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !slerpValueOn)
            {
                transform.Translate(movementVec * Time.deltaTime * crouchingSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeed);
            }
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
            isTrigger.height = 0.7743956f;
            isTrigger.center = new Vector3(-0.01670074f, 0.4888585f, 0.0531683f);
        }
        if (!isCrouch)
        {
            ResetValues();
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
        RaycastHit forwardHit;
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f, ledgeLayer))
        {
            ResetAnim();
            anim.SetBool("isLedgeGrabbing", true);
            float dist = Vector3.Distance(forwardHit.point, handRayCastPivot.transform.position);
            col.enabled = false;
            //transform.localPosition += (Vector3.right- new Vector3(0.5f, 0.0f, 0.0f)) * dist;
            ledge = true;
            freezeRot = true;
            rb.useGravity = false;
            //456
        }
    }
    public void LedgeCLimb()
    {
        ResetValues();
        print("climb");
    }
    public void BalancingBar()
    {
        freezeRot = true;
        balancebar = true;

        transform.LookAt(wc.destenation);
        player.transform.LookAt(wc.destenation);

        anim.SetBool("isBalancing", true);
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
            runningSpeedAftherRun -= 3.5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            inputY = 0;
            player.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            runningSpeedAftherRun = runningSpeed;
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
        anim.SetBool("isBalancing", false);
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
        balancebar = false;

        wc.balanceBegin = false;
        wc.balanceEnd = false;
        col.enabled = true;

        isTrigger.height = 1.592172f;
        isTrigger.center = new Vector3(-0.01670074f, 0.8977467f, 0.0531683f);
        notTrigger.height = isTrigger.height;
        notTrigger.center = isTrigger.center;
    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, Color.yellow);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, Color.cyan);
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, Color.cyan);
    }
}