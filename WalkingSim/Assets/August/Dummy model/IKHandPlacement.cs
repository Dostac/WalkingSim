using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IKHandPlacement : MonoBehaviour
{
	[Header("Bools")]
	public bool useIK = true;

	public bool leftHandIK = false;
	public bool rightHandIK = false;

	public bool leftFootIK = false;
	public bool rightFootIK = false;
	public bool useFootIK = false;
	[Header("Vectors for placement")]
	public Vector3 leftHandOffset;
	public Vector3 rightHandOffset;

	public Vector3 leftFootOffset;
	public Vector3 rightFootOffset;

	private Vector3 leftHandPos;
	private Vector3 rightHandPos;

	private Vector3 leftFootPos;
	private Vector3 rightFootPos;

	private Vector3 curLeftHandPos;
	private Vector3 curRightHandPos;

	private Quaternion leftHandRot;
	private Quaternion rightHandRot;

	private Quaternion leftFootRot;
	private Quaternion rightFootRot;

	public Quaternion leftFootRotOffset;
	public Quaternion rightFootRotOffset;
	[Header("layers")]
	public LayerMask layers;
    [Header("Player script reference")]
    public PlayerMovement pm;
    private Animator anim;
	private float normalizedTime;
	[Header("weight")]
	[Range(0, 5f)]
	public float leftHandWeight = 1f;
	[Range(0, 5f)]
	public float rightHandWeight = 1f;

	// Use this for initialization
	void Start()
	{

		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		RaycastHit LHit;
		RaycastHit RHit;

		RaycastHit LFHit;
		RaycastHit RFHit;

        /*NOTE :- The z axis of the Hands are not applied using IK so in order for the hands to match the obstacle, 
		  make sure that the player's z axis is applied according or player is positioned accordingly */
        if (!pm.ledge)
        {
            //Left Hand IK Check
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 1.5f, 0.5f)), transform.TransformDirection(new Vector3(-0.25f, -1.0f, 0.0f)), out LHit, 1f, layers))
            {
                Vector3 lookAt = Vector3.Cross(-LHit.normal, transform.right);
                lookAt = lookAt.y < 0 ? -lookAt : lookAt;

                //Setting true if raycast hits something
                leftHandIK = true;

                //Setting leftHandPos to raycast hit points and subtracting the offsets
                leftHandPos = LHit.point - transform.TransformDirection(leftHandOffset);
                //leftHandRot = Quaternion.FromToRotation(Vector3.forward, LHit.normal);
                leftHandRot = Quaternion.LookRotation(LHit.point + lookAt, LHit.normal);
            }
            else
            {
                leftHandIK = false;
            }

            //Right Hand IK Check
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 1.5f, 0.5f)), transform.TransformDirection(new Vector3(0.25f, -1.0f, 0.0f)), out RHit, 1f, layers))
            {

                Vector3 lookAt = Vector3.Cross(-RHit.normal, transform.right);
                lookAt = lookAt.y < 0 ? -lookAt : lookAt;

                //Setting true if raycast hits something
                rightHandIK = true;
                rightHandIK = true;

                //Setting rightHandPos to raycast hit points and subtracting the offsets
                rightHandPos = RHit.point - transform.TransformDirection(rightHandOffset);
                //rightHandRot = Quaternion.FromToRotation(Vector3.forward, RHit.normal);
                rightHandRot = Quaternion.LookRotation(RHit.point + lookAt, RHit.normal);
            }
            else
            {
                rightHandIK = false;
            }
        }
        else
        {
            //Left Hand IK Check
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.25f, 2.15f, 0.5f)), transform.TransformDirection(new Vector3(0.25f, -1.0f, 0.0f)), out LHit, 1f, layers))
            {
                Vector3 lookAt = Vector3.Cross(-LHit.normal, transform.right);
                lookAt = lookAt.y < 0 ? -lookAt : lookAt;

                //Setting true if raycast hits something
                leftHandIK = true;

                //Setting leftHandPos to raycast hit points and subtracting the offsets
                leftHandPos = LHit.point - transform.TransformDirection(leftHandOffset);
                //leftHandRot = Quaternion.FromToRotation(Vector3.forward, LHit.normal);
                leftHandRot = Quaternion.LookRotation(LHit.point + lookAt, LHit.normal);
            }
            else
            {
                leftHandIK = false;
            }

            //Right Hand IK Check
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.25f, 2.15f, 0.5f)), transform.TransformDirection(new Vector3(0.25f, -1.0f, 0.0f)), out RHit, 1f, layers))
            {

                Vector3 lookAt = Vector3.Cross(-RHit.normal, transform.right);
                lookAt = lookAt.y < 0 ? -lookAt : lookAt;

                //Setting true if raycast hits something
                rightHandIK = true;
                rightHandIK = true;

                //Setting rightHandPos to raycast hit points and subtracting the offsets
                rightHandPos = RHit.point - transform.TransformDirection(rightHandOffset);
                //rightHandRot = Quaternion.FromToRotation(Vector3.forward, RHit.normal);
                rightHandRot = Quaternion.LookRotation(RHit.point + lookAt, RHit.normal);
            }
            else
            {
                rightHandIK = false;
            }
        }
		if (useFootIK)
		{
			//Left Foot IK Check
			if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-0.35f, 0.5f, 0.0f)), transform.forward, out LFHit, 1f, layers))
			{

				leftFootIK = true;
				leftFootPos = LFHit.point - leftFootOffset;
				leftFootRot = (Quaternion.FromToRotation(Vector3.up, LFHit.normal)) * leftFootRotOffset;
			}
			else
				leftFootIK = false;

			//Right Foot IK Check
			if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.35f, 0.5f, 0.0f)), transform.forward, out RFHit, 1f, layers))
			{

				rightFootIK = true;
				rightFootPos = RFHit.point - rightFootOffset;
				rightFootRot = (Quaternion.FromToRotation(Vector3.up, RFHit.normal)) * rightFootRotOffset;
			}
			else
				rightFootIK = false;
		}
		normalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

		if (anim)
		{

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdlePose"))
			{ //Change the name to whatever your "ON LEDGE MOVING" animation is named to...

				float vel = 0.0f;

				if (normalizedTime < 0.25f)
				{

					//Smoothly changing IK Weights from current value to 0 or current value to 1 based on current hand's position (find the specific time based on animation)
					leftHandWeight = Mathf.SmoothDamp(leftHandWeight, 0.0f, ref vel, 2 * Time.deltaTime);
					rightHandWeight = Mathf.SmoothDamp(rightHandWeight, 1.0f, ref vel, 8 * Time.deltaTime);
				}

				else if (normalizedTime > 0.25f && normalizedTime < 0.5f)
				{

					//Smoothly changing IK Weights from current value to 0 or current value to 1 based on current hand's position (find the specific time based on animation)
					leftHandWeight = Mathf.SmoothDamp(leftHandWeight, 1.0f, ref vel, 8 * Time.deltaTime);
					rightHandWeight = Mathf.SmoothDamp(rightHandWeight, 0.0f, ref vel, 2 * Time.deltaTime);
				}
			}

			else
			{ //Resets the hand weights back to 1.0f, add further animation info here to make sure that weights are 0.0f when in normal player movement

				leftHandWeight = 1.0f;
				rightHandWeight = 1.0f;
			}
		}
	}

	void OnDrawGizmos()
	{

		//Left Hand IK Visual Ray
		Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 1.5f, 0.5f)), transform.TransformDirection(new Vector3(-0.25f, -1.0f, 0.0f)), Color.green);

		//Right Hand IK Visual Ray
		Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.0f, 1.5f, 0.5f)), transform.TransformDirection(new Vector3(0.25f, -1.0f, 0.0f)), Color.green);

		//Left Foot IK Visual Ray
		Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0.0f)), transform.forward, Color.red);

		//Right Foot IK Visual Ray
		Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.5f, 0.5f, 0.0f)), transform.forward, Color.red);

        //ledge grab

        //Left Hand IK Visual Ray
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-0.25f, 2.15f, 0.5f)), transform.TransformDirection(new Vector3(-0.25f, -1.0f, 0.0f)), Color.green);

        //Right Hand IK Visual Ray
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0.25f, 2.15f, 0.5f)), transform.TransformDirection(new Vector3(0.25f, -1.0f, 0.0f)), Color.green);
    }

	void OnAnimatorIK()
	{

		//Setting up IK Weights and positions
		if (useIK)
		{

			curLeftHandPos = anim.GetIKPosition(AvatarIKGoal.LeftHand);
			curRightHandPos = anim.GetIKPosition(AvatarIKGoal.RightHand);

			if (leftHandIK)
			{

				anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
				anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);

				anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
				anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);
			}

			if (rightHandIK)
			{

				anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
				anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);

				anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
				anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
			}

			if (leftFootIK)
			{

				anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
				anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

				anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
				anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
			}

			if (rightFootIK)
			{

				anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
				anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

				anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
				anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
			}
		}
	}
}
