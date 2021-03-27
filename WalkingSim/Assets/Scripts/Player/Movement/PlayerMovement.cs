using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    #region public values
    [Header("Speed Values")]

    [Tooltip("Set a speed value")]
    public float desiredRoationSpeed;

    [Tooltip("Set a speed value")]
    public float slidingSpeed;

    [Tooltip("Set a speed value")]
    public float runningSpeed;

    [Tooltip("Set a speed value")]
    public float walkingSpeed;

    [Tooltip("Set a speed value")]
    public float crouchingSpeed;

    [Tooltip("Set a speed value")]
    public float ledgeSpeed;

    [Tooltip("Set a speed value")]
    public float vaultingSpeed = 1.5f;

    [Header("Time Values")]
    [Tooltip("Set a time value for the slide")]
    public float slidingTime = 1;
    [Tooltip("Set a time value for the wall run to be allowed")]
    public float wallRunningTime = 2.5f;

    [Header("Gravity Values")]

    [Tooltip("Set a value for corection gravity default it is 4")]
    public float grafityCorectionValue = 4;
    [Tooltip("Set a value for minimum gravity default it is 1")]
    public float grafityCorectionMinValue = 1;

    [Header("Jump Values")]
    [Tooltip("Set a value jump force this wil change the height u wil jump")]
    public float jumpForce;
    [Tooltip("Set a value for the quantity of jumps")]
    public int jumps;

    [Header("WallRun Values")]
    [Tooltip("Max value for wallruning speed")]
    public float maxWallSpeed;
    [Tooltip("Force on the wall")]
    public float wallrunForce;
    [Tooltip("Angle that the player has on a wall while wallruning")]
    public float wallRunAngle = 60;
    [Space(20)]
    [Tooltip("This is a bool that wil be checked in a other code")]
    public bool isWallRunning;
    [Tooltip("This is a bool that wil be checked in a other code")]
    public bool wallLeft;
    [Tooltip("This is a bool that wil be checked in a other code")]
    public bool wallRight;

    [Header("Player Components")]

    [Tooltip("Select Player")]
    public GameObject player;

    [Tooltip("Select Players Rig hip")]
    public GameObject hips;

    [Tooltip("Select empty object named orientation around the hips as position")]
    public Transform orientation;

    [Tooltip("Select Third person camerea")]
    public Camera cam;

    [Tooltip("Select animator on the skinedPlayer")]
    public Animator anim;

    [Tooltip("Select empty object named handpivot")]
    public Transform handRayCastPivot;
    [Tooltip("Select empty object named footLeft")]
    public Transform footLeft;
    [Tooltip("Select empty object named footRight")]
    public Transform footRight;

    [Header("Wall layers")]
    [Tooltip("Select the right layer for the object to interact with")]
    public LayerMask wall;

    [Tooltip("Select the right layer for the object to interact with")]
    public LayerMask ledgeLayer;

    [Tooltip("Select the right layer for the object to interact with")]
    public LayerMask groundLayer;

    [Header("Action Values")]
    [Tooltip("Reset time for actions")]
    public float resetTimeVault = 1;
    [Space(5)]
    [Range(0.0005f, 0.25f)]
    [Tooltip("Ledge Offset this is the distance between the ledge and the player")]
    public float ledgeOffSet = 0.15f;
    [Range(0.0005f, 4)]
    [Tooltip("Ledge Offset this is the distance between the ledge and the player")]
    public float cornerToOtherObjectOfset = 0.8f;
    [Tooltip("Max Distance between the destenation of a action and the player this must be set because other wise it sometimes teleports to a wrong destenation")]
    public float maxActionDistance=7.50f;
    [Space(10)]
    [Tooltip("This is a bool that wil be checked in a other code")]
    public bool ledge;
    [Tooltip("This is a bool that wil be checked in a other code")]
    public bool balancebar;
    [Space(10)]
    [Tooltip("Set a value for the sliding speed to multiply with")]
    public float sldingMultiplier = 1.5f;
    [Space(10)]
    [Header("Player Colliders")]
    [Tooltip("The capsule collider that is on the component player")]
    public CapsuleCollider isTrigger;
    [Tooltip("The capsule collider that is on the component player")]
    public CapsuleCollider notTrigger;
    [Space(5)]
    [Tooltip("capsule offcenter for actions (vault,ledgeclimb,medium wall)")]
    public Vector3 capsuleCenterActions = new Vector3(-0.01670074f, 1.225295f, 0.0531683f);
    [Tooltip("capsule height for actions (vault,ledgeclimb,medium wall)")]
    public float capsuleHeightActions = 0.9370761f;
    [Space(5)]
    [Tooltip("capsule offcenter for slide")]
    public Vector3 capsuleCenterSlide = new Vector3(-0.01670074f, 0.4888585f, 0.0531683f);
    [Tooltip("capsule height for slide")]
    public float capsuleHeightSlide = 0.7743956f;
    [Space(5)]
    [Tooltip("capsule offcenter for crouching")]
    public Vector3 capsuleCenterCrouch = new Vector3(-0.01670074f, 0.4888585f, 0.0531683f);
    [Tooltip("capsule height for crouching")]
    public float capsuleHeightCrouch = 1.2743956f;

    [Header("Invoke Resets")]
    [Tooltip("Reset time for a invoke afther a action")]
    public float resetActionTime = 0.5f;
    [Space(5)]
    [Tooltip("Reset time for ledge climb lerp")]
    public float ledgeClimbLerpReset = 0.6f;
    [Tooltip("Reset time for ledge climb")]
    public float ledgeClimReset = 0.65f;
    [Tooltip("Reset time for ledge afther ledge climb")]
    public float ledeReset = 1f;
    [Space(5)]
    [Tooltip("Reset time for setting a bool so it wont instant ledgeclimb")]
    public float ledgeClimbBoolInvoke = 0.5f;
    [Space(5)]
    [Tooltip("Reset time for a invoke afther a mediumwall action")]
    public float mediumWallReset = 1.3f;
    [Space(5)]
    [Tooltip("Reset time for the ledge reset to be active again")]
    public float ToOtherObjectInvokeTime = 0.25f;
    [Tooltip("Reset time for the ledge to get reset again")]
    public float invokeStartLEdge = 0.25f;
    [Header("falling Values")]
    public float distanceForDetectionToBeGround = 0.25f;
    public float distanceForDetectionToBeInAir = 0.26f;
    [Space(5)]
    [Tooltip("the time to check if player is still falling best to set like 0.5f so it checks and also sets the idle aired anim late")]
    public float timeForFallingCheck=0.5f;
    [Space(5)]
    public float distanceFromGroundForLanding = 0.26f;
    [Header("Velocity Values")]
    public float velocityToBeAired;
    [Space(2)]
    public float fallingSpeedForRoll;
    public float fallingSpeedForLand;
    #endregion
    #region private values
    private float slidegSpeed;
    private float movementSpeed;
    private float inputX;
    private float inputY;
    private float inputZ;
    private float speed;
    private float timer;
    private float runningSpeedAftherRun;
    private float curentYPos;
    private float groundDistance;

    private float allowPlayerRotation;

    private int jumpCount;

    private bool isCrouch;
    private bool isVaulting;
    private bool sliding;
    private bool isGrounded;
    private bool getsInput;

    private bool frozen;
    private bool action;

    private bool lader;

    private bool lerpValueOn;
    private bool freezeRot;
    private bool isClimable;
    private bool canLedge;
    private bool gotYValue;

    private bool isLedgeClimbing;
    private bool wentToOtherObject;
    private bool isCliming;
    private bool cooldownActionAftherLedge;
    private bool startLedge;

    private Vector3 desireMovementDirection;
    private Transform LedgeDes;

    private WallCollision wc;

    private LedgeMainObject ledgeMainObject;

    private InputManager im;
    private Rigidbody rb;
    private Rigidbody playerRB;
    #endregion
    private void Start()
    {
        jumpCount = jumps;
        runningSpeedAftherRun = runningSpeed;

        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        wc = player.GetComponent<WallCollision>();
        playerRB = player.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        canLedge = true;
    }
    public void Update()
    {
        PlayerInput();
        CheckForWall();
        TimeUpdate();
        AtionUpdator();
        CheckIfInAired();
    }
    public void FixedUpdate()
    {
        PlayerRotUpdate();
        Movement();
        GroundDetection();
    }
    #region playerinput (update)
    public void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        RaycastHit LedgeClimbSpace;
        if (Input.GetButtonDown("C"))
        {
            if (isGrounded && wc.balancingBar)
            {
                BalancingBar();
            }
            else if (!balancebar && isGrounded && !sliding && !im.runPressed)
            {
                isCrouch = !isCrouch;
                Crouch();
            }
            else if (!balancebar && !sliding && im.runPressed && isGrounded)
            {
                Slide();
            }
        }
        if (Input.GetButton("Jump") && ledge && im.forwardPressed && isClimable)
        {
            //two raycast forward and up to check if there is a object blocking the climb
            if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), transform.forward, out LedgeClimbSpace, 1f)
                && !Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2, -0.25f)), transform.up, out LedgeClimbSpace, 1f))
            {
                LedgeCLimb();
            }
        }
        else if (Input.GetButton("Jump") && ledge && im.backwardsPressed)
        {
            ResetValues();
            Invoke("LedgeGrabBool", 1);
            Invoke("ResetLedge", ledeReset);
        }
        if (im.spacebar)
        {
            if (wc.vault && isGrounded && !ledge && !cooldownActionAftherLedge)
            {
                Vault();
            }
            else if (wc.medium && !wc.vault && !ledge && im.runPressed && !sliding && !cooldownActionAftherLedge && isGrounded && !Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), wc.destenation.position, out LedgeClimbSpace, 2f)
                && !Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 3, 0.0f)), transform.forward, out LedgeClimbSpace, 2f)
                && !Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2, -0.25f)), transform.up, out LedgeClimbSpace, 1f))
            {
                MediumWall();
            }
            else if (!ledge && !lerpValueOn)
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
        if (im.runPressed && !sliding && !isCrouch && getsInput)
        {
            movementSpeed = runningSpeed;
        }
        if (!im.runPressed && !sliding && !isCrouch && getsInput)
        {
            movementSpeed = walkingSpeed;
        }
        if (!im.runPressed && !sliding && isCrouch && getsInput)
        {
            movementSpeed = crouchingSpeed;
        }
        if (sliding && !getsInput)
        {
            movementSpeed = walkingSpeed / 2;
        }
    }
    #endregion
    #region player rotation
    void PlayerRotation()
    {
        if (!freezeRot)
        {
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

        if (speed > allowPlayerRotation)
        {
            PlayerRotation();
        }
    }
    #endregion
    #region action updator (update)
    public void AtionUpdator()
    {
        RaycastHit ledgeCheck;
        if (canLedge && wc.ledge && !lerpValueOn && !isCliming && !isCrouch && !isGrounded)
        {
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer))
            {
                LedgeGrab();
            }
            else if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.28f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer))
            {
                LedgeGrab();
            }
            else if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.31f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer))
            {
                LedgeGrab();
            }
            else if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.22f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer))
            {
                LedgeGrab();
            }
            else if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.19f, 0.0f)), transform.forward, out ledgeCheck, 1f, ledgeLayer))
            {
                LedgeGrab();
            }
        }
        //vaut
        if (lerpValueOn)
        {
            float distance = Vector3.Distance(transform.position, wc.destenation.position);
            if (distance <= maxActionDistance)
            {
                transform.position = Vector3.Lerp(transform.position, wc.destenation.position, vaultingSpeed * Time.deltaTime);
            }
        }
        else if (isCliming)
        {
            float distance = Vector3.Distance(transform.position, wc.destenation.position);
            if (distance <= maxActionDistance)
            {
                transform.position = Vector3.Lerp(transform.position, LedgeDes.position + new Vector3(0, handRayCastPivot.localPosition.y, 0), vaultingSpeed * Time.deltaTime * 2);
            }
        }
        //ledge
        if (ledge)
        {
            RaycastHit forwardHit;

            RaycastHit forwardHitleftShort;
            RaycastHit forwardHitRightShort;

            RaycastHit LedgeCheckWallLeft;
            RaycastHit LedgeCheckWallRight;

            RaycastHit forwardHitleft;
            RaycastHit forwardHitRight;

            RaycastHit leftSideHit;
            RaycastHit rightSideHit;
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 0.2f, ledgeLayer))
            {
                #region rotation ledge
                Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);
                ledgeMainObject = forwardHit.transform.GetComponent<LedgeMainObject>();
                if (forwardHit.transform.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.transform.rotation.eulerAngles.z,rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.transform.rotation.eulerAngles.x);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                else if (forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<LedgeMainObject>() != null)
                {
                    rotation.eulerAngles = new Vector3(forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.x, rotation.eulerAngles.y, forwardHit.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.transform.rotation.z);
                }
                transform.rotation = rotation;
                player.transform.rotation = rotation;
                print(rotation);
                #endregion

                LedgeDes = forwardHit.transform;

                float ledgeHeight = forwardHit.transform.position.y - handRayCastPivot.localPosition.y;

                curentYPos = ledgeHeight;

                if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.5f)), transform.right, out LedgeCheckWallRight, 0.5f))//right
                {
                    if (im.rightPressed && Input.GetButton("Jump"))
                    {
                        ledgeMainObject = null;
                        wentToOtherObject = true;
                        Quaternion Rot = Quaternion.LookRotation(-LedgeCheckWallRight.normal, Vector3.up);
                        transform.rotation = Rot;
                        transform.position = Vector3.Lerp(transform.position, LedgeCheckWallRight.point, speed * Time.deltaTime);
                        transform.position += transform.forward * cornerToOtherObjectOfset;
                        Invoke("ToOtherObjectBool", ToOtherObjectInvokeTime);
                    }
                }
                if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.5f)), -transform.right, out LedgeCheckWallLeft, 0.5f))//left
                {
                    if (im.leftPressed && Input.GetButton("Jump"))
                    {
                        ledgeMainObject = null;
                        wentToOtherObject = true;
                        Quaternion Rot = Quaternion.LookRotation(-LedgeCheckWallLeft.normal, Vector3.up);
                        transform.rotation = Rot;
                        transform.position = Vector3.Lerp(transform.position, LedgeCheckWallLeft.point, speed * Time.deltaTime);
                        transform.position += transform.forward * cornerToOtherObjectOfset;
                        Invoke("ToOtherObjectBool", ToOtherObjectInvokeTime);
                    }
                }
            }
            else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 0.2f, ledgeLayer))
            {
                if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitleft, 0.2f))
                {
                    if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, out rightSideHit, 1f, ledgeLayer))//kijken hoew  tgaat zonder transfgorm.directiuno
                    {
                        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.15f, 2.25f, 0.0f)), transform.forward, out forwardHitRightShort, 0.2f))
                        {
                            if (rightSideHit.transform.tag != "Ledge")
                            {
                            }
                            else
                            {
                                if (im.leftPressed && Input.GetButton("Jump"))
                                {
                                    ledgeMainObject = null;
                                    Quaternion rotation = Quaternion.LookRotation(-rightSideHit.normal, Vector3.up);
                                    transform.rotation = rotation;
                                    transform.position = Vector3.Lerp(transform.position, rightSideHit.point, speed * Time.deltaTime);
                                    transform.position -= transform.forward * ledgeOffSet;
                                }
                            }
                        }
                        else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.15f, 2.25f, 0.0f)), transform.forward, out forwardHitRightShort, 0.2f))
                        {
                            if (!wentToOtherObject && !startLedge)
                            {
                                ResetValues();
                                Invoke("LedgeGrabBool", 1);
                                Invoke("ResetLedge", ledeReset);
                            }
                        }
                    }
                }
                else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, out forwardHitRight, 0.2f))
                {
                    if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, out leftSideHit, 1f, ledgeLayer))
                    {
                        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.15f, 2.25f, 0.0f)), transform.forward, out forwardHitleftShort, 0.2f))
                        {
                            if (leftSideHit.transform.tag != "Ledge")
                            {
                            }
                            else
                            {
                                if (im.rightPressed && Input.GetButton("Jump"))
                                {
                                    ledgeMainObject = null;
                                    Quaternion rotation = Quaternion.LookRotation(-leftSideHit.normal, Vector3.up);
                                    transform.rotation = rotation;
                                    transform.position = Vector3.Lerp(transform.position, leftSideHit.point, speed * Time.deltaTime);
                                    transform.position -= transform.forward * ledgeOffSet;
                                }
                            }
                        }
                        else if (!Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.15f, 2.25f, 0.0f)), transform.forward, out forwardHitleftShort, 0.2f))
                        {
                            if (!wentToOtherObject && !startLedge)
                            {
                                ResetValues();
                                Invoke("LedgeGrabBool", 1);
                                Invoke("ResetLedge", ledeReset);
                            }
                        }
                    }
                }
                if (!wc.ledge && !wentToOtherObject && !startLedge)
                {
                    ResetValues();
                    Invoke("LedgeGrabBool", 1);
                    Invoke("ResetLedge", ledeReset);
                }
            }
        }
        RaycastHit groundCheck;
        if (lader && Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, 0.25f, 0.25f)), -transform.up, out groundCheck, 0.2f, groundLayer))
        {
            ResetValues();
        }
        if (!lader && wc.laderbool)
        {
            StartLader();
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
            Vector3 v = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, v.y, wallRunAngle), desiredRoationSpeed);
            player.transform.localPosition = new Vector3(0.278f, -0.233f, 0.003f);
        }
        if (wallLeft && !isGrounded)
        {
            StartWallRun();
            Vector3 v = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, v.y, -wallRunAngle), desiredRoationSpeed);
            player.transform.localPosition = new Vector3(-0.123F, -0.196f, 0.2f);
        }
        if (isWallRunning && !wallLeft && !wallRight)
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
        isWallRunning = true;
        rb.useGravity = false;
        ResetFalling();
    }
    public void EndWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
        player.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        if (runningSpeedAftherRun >= grafityCorectionValue / 2)
        {
            runningSpeedAftherRun -= grafityCorectionValue;
        }
        if (runningSpeedAftherRun <= grafityCorectionMinValue)
        {
            runningSpeedAftherRun = grafityCorectionMinValue;
        }
    }
    #endregion
    #region player movement (update)
    public void Movement()
    {
        if (!ledge && !balancebar && !wc.laderbool&&!lerpValueOn)
        {
            var movementVec = new Vector3(inputX, 0, inputZ).normalized;
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
            RaycastHit LedgeCheckWallLeft;
            RaycastHit LedgeCheckWallRight;
            float ledgeInput = inputX;
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.5f)), transform.right, out LedgeCheckWallRight, 0.5f))//right
            {
                if (ledgeInput == 1)
                {
                    ledgeInput = 0;
                }
            }
            Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.5f)), -transform.right, Color.red, 0.1f);
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.5f)), -transform.right, out LedgeCheckWallLeft, 0.5f))//left
            {
                if (ledgeInput == -1)
                {
                    ledgeInput = 0;
                }
            }
            var movementVec = new Vector3(ledgeInput, 0, 0).normalized;
            anim.SetFloat("ledgeVelocity", inputX + 1);
            if (!gotYValue)
            {
                GetYAxees();
            }
            transform.Translate(movementVec * Time.deltaTime * ledgeSpeed);
            var curentPos = new Vector3(transform.position.x, curentYPos, transform.position.z);
            transform.position = curentPos;
        }
        else if (!ledge && balancebar && !wc.laderbool)
        {
            if (!wc.balancingBar && wc.balanceBegin || !wc.balancingBar && wc.balanceEnd)
            {
                ResetValues();
            }
            var movementVec = new Vector3(0, 0, inputZ).normalized;
            anim.SetFloat("balancingVelocity", inputZ + 1);
            var jumpVec = new Vector3(0, inputY, 0);
            if (!sliding && !lerpValueOn && !isLedgeClimbing)
            {
                transform.Translate(movementVec * Time.deltaTime * crouchingSpeed);
                transform.Translate(jumpVec * Time.deltaTime * runningSpeed);
            }
        }
        else if (!ledge && !balancebar && wc.laderbool && !isLedgeClimbing && lader)
        {
            if (im.forwardPressed)
            {
                inputY = 1;
            }
            else if (im.backwardsPressed)
            {
                inputY = -1;
            }
            else if (!im.backwardsPressed && !im.forwardPressed)
            {
                inputY = 0;
            }
            var jumpVec = new Vector3(0, inputY, 0);
            transform.Translate(jumpVec / 30);
            if (!frozen && !isGrounded)
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
    public void ToOtherObjectBool()
    {
        wentToOtherObject = false;
    }
    public void Slide()
    {
        ResetAnim();
        anim.SetBool("isSliding", true);

        isCrouch = false;
        sliding = true;
        float x = movementSpeed;
        slidegSpeed = slidingSpeed * x / sldingMultiplier * sldingMultiplier;

        notTrigger.height = capsuleHeightSlide;
        notTrigger.center = capsuleCenterSlide;

        Invoke("ResetValues", slidingTime);
    }
    public void Crouch()
    {
        if (isCrouch && !ledge && !lerpValueOn && !balancebar)
        {
            anim.SetBool("isCrouching", true);

            notTrigger.height = capsuleHeightCrouch;
            notTrigger.center = capsuleCenterCrouch;
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

            inputY += jumpForce;
            jumpCount--;
            isCrouch = false;
        }
    }
    public void Vault()
    {
        ResetAnim();
        anim.SetBool("isVaulting", true);

        playerRB.isKinematic = false;
        isVaulting = true;
        lerpValueOn = true;
        action = true;
        freezeRot = true;

        notTrigger.height = capsuleHeightActions;
        notTrigger.center = capsuleCenterActions;

        Invoke("ResetValues", resetTimeVault);
    }
    public void MediumWall()
    {
        ResetAnim();
        anim.SetBool("isCliming", true);

        playerRB.isKinematic = false;
        action = true;
        lerpValueOn = true;
        freezeRot = true;
        notTrigger.height = capsuleHeightActions;
        notTrigger.center = capsuleCenterActions;

        Invoke("ResetValues", mediumWallReset);
    }
    public void LedgeGrab()
    {
        RaycastHit forwardHit;
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, out forwardHit, 1f, ledgeLayer) && !ledge)
        {
            ResetAnim();
            ResetFalling();

            anim.SetBool("isLedgeGrabbing", true);

            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            notTrigger.enabled = false;
            cooldownActionAftherLedge = true;
            action = true;
            startLedge = true;

            transform.localPosition = forwardHit.point - new Vector3(0, handRayCastPivot.localPosition.y, 0);
            Quaternion rotation = Quaternion.LookRotation(-forwardHit.normal, Vector3.up);

            transform.rotation = rotation;
            freezeRot = true;
            rb.useGravity = false;
            ledge = true;

            transform.rotation = rotation;
            player.transform.rotation = rotation;
            transform.position -= transform.forward * ledgeOffSet;

            Invoke("LedgeClimbBool", ledgeClimbBoolInvoke);
            Invoke("ResetStartLedge", invokeStartLEdge);
        }
    }
    public void LedgeCLimb()
    {
        isLedgeClimbing = true;
        lerpValueOn = false;
        isCliming = true;
        rb.useGravity = true;
        action = true;
        ledge = false;
        freezeRot = false;

        runningSpeedAftherRun = 10;
        inputY = 0;

        notTrigger.height = capsuleHeightActions;
        notTrigger.center = capsuleCenterActions;

        Invoke("LerpValueBool", ledgeClimbLerpReset);
        Invoke("ResetValues", ledgeClimReset);
        Invoke("ResetLedge", ledeReset);
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
    public void StartLader()
    {
        lader = true;
        action = true;
    }
    public void BalancingBar()
    {
        freezeRot = true;
        balancebar = true;
        action = true;
        transform.LookAt(wc.destenation);
        player.transform.LookAt(wc.destenation);

        anim.SetBool("isBalancing", true);
    }
    public void UpForce()
    {
        inputY += jumpForce;
        sliding = true;
        float x = movementSpeed;
        slidegSpeed = slidingSpeed * x / sldingMultiplier * sldingMultiplier;
    }
    #endregion
    #region colision
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walkable"))
        {
            Grounded();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walkable"))
        {
            OnGroundedExit();
            Aired();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walkable"))
        {
            EnterGround();
        }
    }
    #endregion
    #region Ground Voids( has a updat)
    public void Grounded()
    {
        isGrounded = true;
        canLedge = true;
        jumpCount = jumps;
        ResetFalling();
    }
    public void EnterGround()
    {
        inputY = 0;
        player.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        runningSpeedAftherRun = runningSpeed;
        ResetFalling();
    }
    public void OnGroundedExit()
    {
        jumpCount = jumps -1;    
    }
    public void Aired()
    {
        isGrounded = false;
        if (runningSpeedAftherRun >= grafityCorectionValue)
        {
            runningSpeedAftherRun -= grafityCorectionValue;
        }
        else if (runningSpeedAftherRun <= grafityCorectionMinValue)
        {
            runningSpeedAftherRun = grafityCorectionMinValue;
        }
    }
    public void ResetFalling()
    {
        anim.SetBool("isFalling", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isRolling", false);
        anim.SetBool("isLanding", false);
    }
    public void GroundDetection()
    {
        if (!isGrounded)
        {
            RaycastHit FallDetection;
            if (!isGrounded && !isCliming && !isLedgeClimbing && !isWallRunning && !isVaulting && !lerpValueOn &&
                Physics.Raycast(transform.position + (new Vector3(0f, 0.25f, 0f)), -transform.up, out FallDetection, 20f, groundLayer))
            {
                groundDistance = Vector3.Distance(transform.position, FallDetection.point) * 3;
            }
            if (!isGrounded && !ledge && !isCliming && !isVaulting && !lerpValueOn && !action && !sliding && !isWallRunning)
            {
                if (groundDistance <= distanceForDetectionToBeGround)
                {
                    Grounded();
                }
                else if (groundDistance > distanceForDetectionToBeGround)
                {
                    CheckIfAired();
                }
                if (groundDistance <= distanceFromGroundForLanding)
                {
                    GetLandAnim();
                }
            }
        }
    }
    public void CheckIfAired()
    {
        RaycastHit LF;
        RaycastHit RF;

        if (Physics.Raycast(footLeft.position + (new Vector3(0f, 0.5f, 0f)), -transform.up, out LF, 3, groundLayer))
        {
            float ground = Vector3.Distance(transform.position, LF.point) * 3;
            if (ground <= distanceForDetectionToBeGround)
            {
                Grounded();
            }
            else if (ground > distanceForDetectionToBeGround)
            {
                Aired();
            }
        }
        else if (Physics.Raycast(footRight.position + (new Vector3(0f, 0.5f, 0f)), -transform.up, out RF, 3, groundLayer))
        {
            float ground = Vector3.Distance(transform.position, LF.point) * 3;
            if (ground <= distanceForDetectionToBeGround)
            {
                Grounded();
            }
            else if (ground > distanceForDetectionToBeGround)
            {
                Aired();
            }
        }
        else
        {
            Aired();
        }
    }
    public void CheckIfInAired()
    {
        if (!isGrounded && !ledge && !isCliming && !isVaulting && !lerpValueOn && !action && !sliding&&!isWallRunning)
        {
            if (rb.velocity.y <= velocityToBeAired)
            {
                Invoke("CheckIfStillAired", timeForFallingCheck);
            }
        }
    }
    public void GetLandAnim()
    {
        if (!isGrounded && !ledge && !isCliming && !isVaulting && !lerpValueOn && !action && !sliding && !isWallRunning)
        {
            if (rb.velocity.magnitude <= fallingSpeedForRoll)
            {
                anim.SetBool("isRolling", true);
            }
            else if (rb.velocity.magnitude > fallingSpeedForLand)
            {
                anim.SetBool("isLanding", true);
            }
        }
    }
    public void CheckIfStillAired()
    {
        if (!isGrounded)
        {
            anim.SetBool("isFalling", true);
        }
    }
    #endregion
    #region player value voids
    public void TimeUpdate()
    {
        if (isWallRunning)//this is a reset time for the wallrun
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
        rb.isKinematic = false;
        freezeRot = false;
        ledge = false;
        gotYValue = false;
        balancebar = false;
        canLedge = false;
        isClimable = false;
        isLedgeClimbing = false;
        isCliming = false;
        lader = false;
        isVaulting = false;
        wc.balanceBegin = false;
        wc.balanceEnd = false;
        notTrigger.enabled = true;
        notTrigger.radius = isTrigger.radius;
        notTrigger.height = isTrigger.height;
        notTrigger.center = isTrigger.center;
        ledgeMainObject = null;
        Invoke("ResetAction", resetActionTime);
    }
    public void ResetLedge()
    {
        cooldownActionAftherLedge = false;
    }
    public void ResetAction()
    {
        action = false;
    }
    public void ResetStartLedge()
    {
        startLedge = false;
    }
    #endregion
    #region gizoms
    public void OnDrawGizmos()
    {
        //ledge raycast

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 3f, 0.0f)), transform.forward, Color.magenta);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, 0.0f)), transform.forward, Color.yellow);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.15f, 2.25f, 0.0f)), transform.forward, Color.magenta);//rightshort
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.15f, 2.25f, 0.0f)), transform.forward, Color.magenta);//leftshort

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);//right
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.0f)), transform.forward, Color.blue);//left

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.75f, 2.25f, 0.5f)), -transform.right, Color.cyan);
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.75f, 2.25f, 0.5f)), transform.right, Color.cyan);

        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.25f)), transform.right, Color.blue);//wall detection
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 2.25f, -0.25f)), -transform.right, Color.blue);

        //lader ground detection
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 0.25f, 1)), -transform.up, Color.magenta);

        //fall detection
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 1, 0)), -transform.up, Color.blue);

        //ground detection
        //l
        Debug.DrawRay(footLeft.position + transform.TransformDirection(new Vector3(0.0f, 0.5f, 0)), -transform.up, Color.red);
        //r
        Debug.DrawRay(footRight.position + transform.TransformDirection(new Vector3(0.0f, 0.5f, 0)), -transform.up, Color.red);
    }
    #endregion
}