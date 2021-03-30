using UnityEngine;
public class GrablinGun : MonoBehaviour
{
    #region public componants
    [Header("layer")]
    public LayerMask whatIsGrappleable;
    [Header("transforms")]
    [Tooltip("get the right transform" +
        "cam=camerea" +
        "player is player" +
        "guntip = begin where the line comesout")]
    public Transform gunTip, cam, player;
    [Header("crosshair")]
    public float offset;
    [Space(1)]
    public GameObject crosshair;
    #endregion
    #region private componants
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    #endregion
    #region Grapple functions
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
        RaycastHit hit;
        if (Physics.Raycast(cam.position + new Vector3(0, offset, 0), cam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            crosshair.SetActive(true);
            crosshair.transform.position = hit.point;
            Debug.DrawLine(cam.position + new Vector3(0, offset, 0), cam.forward, Color.red);
        }
        else
        {
            crosshair.SetActive(false);
        }
    }
    void LateUpdate()
    {
        DrawRope();
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position + new Vector3(0, offset, 0), cam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
    public bool IsGrappling()
    {
        return joint != null;
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
    #endregion
}