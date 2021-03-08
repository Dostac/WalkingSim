using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region public values
    [Header("Speed Values")]
    public float desiredRoationSpeed;
    public float slidingSpeed;
    public float runningSpeed;
    public float walkingSpeed;
    public float crouchingSpeed;
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
    public Camera cam;
    public Animator anim;
    [Header("wall layers")]
    public LayerMask wall;
    public LayerMask ledgeLayer;
    [Header("Action Values")]
    public float vaultingSpeed=1.5f;
    public float resetTimeVault=1;
    public float blockHopSpeed=1.5f;
    public bool ledge;
    public bool balancebar;
    public bool isWallRunning;
    public bool wallLeft;
    public bool wallRight;
    public Transform handRayCastPivot;
    [Header("Player Colliders")]
    public CapsuleCollider isTrigger;
    public CapsuleCollider notTrigger;


    public Vector3 lookitsavector;
    #endregion
    #region private values
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

    private bool hoping;


    private bool frozen;

    private bool lerpValueOn;
    private bool freezeRot;
    private bool isClimable;
    private bool canLedge;
    private bool gotYValue;

    private bool isLedgeClimbing;
    private bool isCliming;

    private Vector3 desireMovementDirection,lookAt;
    private Transform LedgeDes;

    private CapsuleCollider normalColSize;
    private RaycastHit hit;
    private WallCollision wc;
    private IKHandPlacement IKHP;
    private InputManager im;
    private Rigidbody rb;
    private Rigidbody playerRB;
    #endregion
    private void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        wc = player.GetComponent<WallCollision>();
        IKHP = player.GetComponent<IKHandPlacement>();
        jumpCount = jumps;
        playerRB =player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        runningSpeedAftherRun = runningSpeed;
        canLedge = true;
    }
    public void Update()
    {
        PlayerInput();
        CheckForWall();
        TimeUpdate();
        AtionUpdator();
    }
    public void FixedUpdate()
    {
        PlayerRotUpdate();
        Movement();
    }
    #region player rotation
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
    #endregion
    #region action updator (update)
    public void AtionUpdator()
    {
        //testing 
        if (Input.GetKeyDown("t"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown("m"))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown("n"))
        {
            Time.timeScale = 0.05f;
        }        
            RaycastHit ledgeCheck;
        if (canLedge && wc.ledge &&!lerpValueOn&&!isCliming&&!isCrouch&& !isGrounded && (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer)))
        {
            LedgeGrab();
        }
        //vaut
        if (lerpValueOn)
        {
            transform.position = Vector3.Lerp(transform.position, wc.destenation.position, vaultingSpeed * Time.deltaTime);
        }
        else if (isCliming)
        {///678
            transform.position = Vector3.Lerp(transform.position, LedgeDes.position+new Vector3(0,2,0), vaultingSpeed * Time.deltaTime*2);
        }
        if (hoping)
        {
            //  transform.position = Vector3.Lerp(transform.position, gahierheen[snelint].position, blockHopSpeed * Time.deltaTime);
            //this is for later
        }
        //ledge
        if (ledge)
        {
            RaycastHit forwardHit;

            RaycastHit forwardHitleft;
            RaycastHit forwardHitRight;

            RaycastHit leftSideHit;
            RaycastHit rightSideHit;
            //123
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f, ledgeLayer))
            {
                Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);
                transform.rotation = rotation;
                player.transform.rotation = rotation;
                var jumpVec = new Vector3(0, curentYPos, 0);
                LedgeDes = forwardHit.transform;
            }
            else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f, ledgeLayer))
            {
                if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitleft, 1f))
                {
                    if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, out rightSideHit, 1f, ledgeLayer))
                    {
                        if (rightSideHit.transform.tag != "Ledge")
                        {

                        }
                        else
                        {
                            if (im.leftPressed && Input.GetKey("space"))
                            {
                                Quaternion rotation = Quaternion.LookRotation(-rightSideHit.normal, Vector3.up);
                                transform.rotation = rotation;
                                transform.position = Vector3.Lerp(transform.position, rightSideHit.point, speed * Time.deltaTime);
                            }

                        }
                    }
                }
                else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitRight, 1f))
                {
                    if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, out leftSideHit, 1f, ledgeLayer))
                    {
                        if (leftSideHit.transform.tag != "Ledge")
                        {

                        }
                        else
                        {
                            if (im.rightPressed && Input.GetKey("space"))
                            {
                                Quaternion rotation = Quaternion.LookRotation(-leftSideHit.normal, Vector3.up);
                                transform.rotation = rotation;
                                transform.position = Vector3.Lerp(transform.position, leftSideHit.point, speed * Time.deltaTime);
                            }
                        }
                    }
                }
                if (!wc.ledge)
                {
                    ResetValues();
                    Invoke("LedgeGrabBool", 1);
                }
            }
        }
        else if (wc.isHopping)//blockhop//for later
        {
            transform.position = Vector3.Lerp(transform.position, wc.destenation.position, blockHopSpeed * Time.deltaTime);
        }
    }
    #endregion
    #region playerinput
    public void PlayerInput()
    {
        RaycastHit LedgeClimbSpace;
        if (Input.GetKeyDown("c"))
        {
            if (isGrounded && wc.balancingBar)
            {
                BalancingBar();
            }
            else if (!balancebar && isGrounded&&!sliding&& !im.runPressed)
            {
                isCrouch = !isCrouch;
                Crouch();
            }
            else if (!balancebar && !sliding&&im.runPressed&&isGrounded)
            {
                Slide();
            }
        }
        if (Input.GetKey("space") && ledge && im.forwardPressed&&isClimable)
        {
            if(!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), transform.forward, out LedgeClimbSpace, 1f))
            {
                LedgeCLimb();
            }
            
        }
        else if (Input.GetKey("space") && ledge&&im.backwardsPressed)
        {
            ResetValues();
            Invoke("LedgeGrabBool", 1);
        }
        if (im.spacebar)
        {
            if (wc.vault&&isGrounded&&!ledge)
            {
                Vault();
            }
            else if(wc.medium&&!wc.vault && !ledge&&!sliding)
            {
                if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), transform.forward, out LedgeClimbSpace, 2f))
                {
                    if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), wc.destenation.position, out LedgeClimbSpace, 2f))
                    {
                        MediumWall();
                    }
                }
            }
            else if (wc.large && !ledge&&!sliding)
            {
                if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), transform.forward, out LedgeClimbSpace, 2f))
                {
                    if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), wc.destenation.position, out LedgeClimbSpace, 2f))
                    {
                        BigWall();
                    }
                }
            }
            else if (!ledge&&!wc.large&&!wc.medium&&!wc.vault)
            {
                Jump();
            }
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
        if (!im.runPressed && !sliding && isCrouch&& getsInput)
        {
            movementSpeed = crouchingSpeed;
        }
        if(sliding&& !getsInput)
        {
            movementSpeed = walkingSpeed/2;
        }
    }
    #endregion
    #region wallrun
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
            if (wallRight)
            {
                rb.AddForce(transform.right * wallrunForce / 5 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(-transform.right * wallrunForce / 5 * Time.deltaTime);
            }
        }
        jumpCount = 2;
        IKHP.useFootIK = true;
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
        ///maybe hitnormal for player to lookat othewise get a point from the wal for player to look at just like vault
    }
    public void EndWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
        player.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        if (runningSpeedAftherRun >= 2f)
        {
            runningSpeedAftherRun -= 4;
        }
        if (runningSpeedAftherRun <= 1)
        {
            runningSpeedAftherRun = 1;
        }
    }
    #endregion
    #region player movement (update)
    public void Movement()
    {
        if (!ledge && !balancebar&& !wc.laderbool)
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !lerpValueOn && !isLedgeClimbing)
            {
                transform.Translate(movementVec * Time.deltaTime * movementSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeedAftherRun);
            }
            if (sliding && !isWallRunning && !lerpValueOn && !isLedgeClimbing)
            {
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * slidegSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeedAftherRun);
            }
        }
        else if (ledge && !balancebar && !wc.laderbool && !isLedgeClimbing)
        {
            var movementVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
            anim.SetFloat("ledgeVelocity",(Input.GetAxisRaw("Horizontal")+1));
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
        else if (!ledge && balancebar&&!wc.laderbool)
        {
            if (!wc.balancingBar&&wc.balanceBegin || !wc.balancingBar && wc.balanceEnd)
            {
                ResetValues();
            }
            var movementVec = new Vector3(0 ,0, Input.GetAxisRaw("Vertical")).normalized;
            anim.SetFloat("balancingVelocity", Input.GetAxisRaw("Vertical") + 1);
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !lerpValueOn && !isLedgeClimbing)
            {
                transform.Translate(movementVec * Time.deltaTime * crouchingSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeed);
            }
        }
        else if (!ledge && !balancebar && wc.laderbool && !isLedgeClimbing)
        {
            if (im.forwardPressed)
            {
                inputY = 1;
            }
            else if (im.backwardsPressed)
            {
                inputY = -1;
            }
            else if (!im.backwardsPressed&& !im.forwardPressed)
            {
                inputY = 0;
            }
            var jumpVec = new Vector3(0, inputY, 0);
            transform.Translate(jumpVec/30);
            if (!frozen&&!isGrounded)
            { 
                freezeRot = true;
                frozen = true;
                anim.SetBool("isLaderLaderClimbing", true);
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            else if (isGrounded)
            {
                freezeRot = false;
                frozen = false;
                anim.SetBool("isLaderClimbing", false);
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }
    #endregion
    #region player actions
    public void Slide()
    {
        isCrouch = false;
        ResetAnim();
        anim.SetBool("isSliding", true);
        sliding = true;
        float x = movementSpeed;
        slidegSpeed = slidingSpeed * x / 1.5f * 1.5f;
        Invoke("ResetValues", slidingTime);
        notTrigger.height = 0.7743956f;
        notTrigger.center = new Vector3(-0.01670074f, 0.4888585f, 0.0531683f);
    }
    public void Crouch()
    {
        if (isCrouch&& !ledge && !lerpValueOn && !balancebar)
        {
            anim.SetBool("isCrouching", true);
            notTrigger.height = 1.2743956f;
            notTrigger.center = new Vector3(-0.01670074f, 0.6888585f, 0.0531683f);
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
        Invoke("ResetValues", resetTimeVault);
        lerpValueOn = true;
        freezeRot = true;
        notTrigger.height = 0.9370761f;
        notTrigger.center = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
    }
    public void MediumWall()
    {
        ResetAnim();
        anim.SetBool("isCliming", true);
        playerRB.isKinematic = false;
        Invoke("ResetValues", 1.3f);
        lerpValueOn = true;
        notTrigger.height = 0.9370761f;
        notTrigger.center = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
    }
    public void BigWall()
    {

    }
    public void LedgeGrab()
    {
        RaycastHit forwardHit;
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f, ledgeLayer) && !ledge)
        {
            notTrigger.enabled = false; 
            ResetAnim();
            anim.SetBool("isLedgeGrabbing", true);
            transform.localPosition =  forwardHit.point-new Vector3(0,2.153f,0);
            Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);
            transform.rotation = rotation;
            freezeRot = true;
            rb.useGravity = false;
            ledge = true;
            Invoke("LedgeClimbBool", 0.5f);
            transform.rotation = rotation;
            player.transform.rotation = rotation;
        }
    }
    public void LedgeCLimb()
    {
        isLedgeClimbing = true;
        isCliming = true;
        rb.useGravity = true;
        ledge = false;
        freezeRot = false;
        //lerpValueOn = true;
        runningSpeedAftherRun = 10;
        inputY = 0;
        notTrigger.height = 0.9370761f;
        notTrigger.center = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
        Invoke("LerpValueBool", 0.6f);
        Invoke("ResetValues", 0.65f);
        ///eerst naar raycast hit dan naar destenation
        //////1234
    }
    public void LedgeClimbBool()
    {
        isClimable = true;
    }
    public void LerpValueBool()
    {
        isCliming = false;
        lerpValueOn = true;
    }
    public void LedgeGrabBool()
    {
        canLedge = true;
    }
    public void StartHop()
    {
        hoping = true;
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
    #endregion
    #region colision
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            isGrounded = true;
            canLedge = true;
            jumpCount = jumps;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            isGrounded = false;
            if (runningSpeedAftherRun >= 4)
            {
            runningSpeedAftherRun -= 4;
            }
            else if(runningSpeedAftherRun <= 1)
            {
                runningSpeedAftherRun=1;
            }
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
    #endregion
    #region player value voids
    public void TimeUpdate()
    {
        if (isWallRunning)
        {
            if (timer < wallRunningTime)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= wallRunningTime)
            {
                EndWallRun();
            }
        }
        else
        {
            timer = 0;
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
    #endregion
    #region resets
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
        lerpValueOn = false;
        rb.useGravity = true;
        freezeRot = false;
        ledge = false;
        gotYValue = false;
        balancebar = false;
        canLedge = false;
        isClimable = false;
        isLedgeClimbing = false;
        isCliming = false;

        IKHP.useFootIK = false;
        wc.balanceBegin = false;
        wc.balanceEnd = false;
        notTrigger.enabled = true;
        notTrigger.radius = isTrigger.radius;
        notTrigger.height = isTrigger.height;
        notTrigger.center = isTrigger.center;
    }
    #endregion
    #region gizoms
    public void OnDrawGizmos()
    {
        //ledge raycast

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 3f, 0.0f)), transform.forward, Color.magenta);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, Color.yellow);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);//right
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);//left

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, Color.cyan);
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, Color.cyan);
    }
    #endregion
}