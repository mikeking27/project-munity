using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject follow;
	private GirlController followController;
	private float yOffset;
	private float lookDownAmount = 0f;
	
	public float leftMargin = 8f;
	public float rightMargin = 2f;
	
	private float topMargin = 0.5f;
	private float bottomMargin = 0.5f;
	
	public float topMarginGrounded = 1f;
	public float bottomMarginGrounded = 1f;
	public float topMarginAirborne = 5f;
	public float bottomMarginAirborne = 2f;
	public float verticalMaxMove = 0.1f;
	
	public float topMarginMax = 8f;
	public float bottomMarginMax = 8f;
	
	
//	public Transform mountains;
//	public Transform hills;
//	public Transform hills2;
//	public Transform hills3;
	private float mountainReset = 20f;
	private float hillReset = 20f;
	
	// Use this for initialization
	void Start () {
		followController = follow.GetComponent<GirlController> ();
		yOffset = transform.position.y - follow.transform.position.y;
		// init offset values
//		mountainReset = mountains.localScale.x * Mathf.Abs(mountains.GetChild(0).localPosition.x - mountains.GetChild(1).localPosition.x);
//		hillReset = hills.localScale.x * Mathf.Abs(hills.GetChild(0).localPosition.x - hills.GetChild(1).localPosition.x);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = transform.position;
		if (follow != null) {
			// use x margins
			if (follow.transform.position.x > pos.x + rightMargin) {
				pos.x = follow.transform.position.x - rightMargin;
			} else if (follow.transform.position.x < pos.x - leftMargin) {
				pos.x = follow.transform.position.x + leftMargin;
			}
			
			// maximum vertical bounds
			if (follow.transform.position.y + yOffset + lookDownAmount > pos.y + topMarginMax) {
				pos.y = follow.transform.position.y + yOffset + lookDownAmount - topMarginMax + verticalMaxMove;
			} else if (follow.transform.position.y + yOffset + lookDownAmount < pos.y - bottomMarginMax) {
				pos.y = follow.transform.position.y + yOffset + lookDownAmount + bottomMarginMax - verticalMaxMove;
			} else {
				pos = GetSmoothCameraPosition(pos);
			}
		}
		// adjust background positions based on change in camera position
//		MoveMountains (pos - transform.position);
		transform.position = pos;
	}
	
//	private void MoveMountains(Vector3 deltaPos) {
//		mountains.position += deltaPos / 1.5f;
//		hills.position += deltaPos / 2f;
//		hills2.position += deltaPos / 3f;
//		hills3.position += deltaPos / 4f;
//		if (mountains.position.x > transform.position.x) {
//			mountains.position -= new Vector3(mountainReset, 0f, 0f);
//		}
//		if (mountains.position.x < transform.position.x - mountainReset) {
//			mountains.position += new Vector3(mountainReset, 0f, 0f);
//		}
//		if (hills.position.x > transform.position.x) {
//			hills.position -= new Vector3(hillReset, 0f, 0f);
//		}
//		if (hills.position.x < transform.position.x - hillReset) {
//			hills.position += new Vector3(hillReset, 0f, 0f);
//		}
//		if (hills2.position.x > transform.position.x) {
//			hills2.position -= new Vector3(hillReset, 0f, 0f);
//		}
//		if (hills2.position.x < transform.position.x - hillReset) {
//			hills2.position += new Vector3(hillReset, 0f, 0f);
//		}
//		if (hills3.position.x > transform.position.x) {
//			hills3.position -= new Vector3(hillReset, 0f, 0f);
//		}
//		if (hills3.position.x < transform.position.x - hillReset) {
//			hills3.position += new Vector3(hillReset, 0f, 0f);
//		}
//	}
	
	private Vector3 GetSmoothCameraPosition(Vector3 pos) {
		if (followController.grounded) {
			// use grounded window y margins
			if (follow.transform.position.y + yOffset + lookDownAmount > pos.y + topMarginGrounded) {
				pos.y = Mathf.Min(follow.transform.position.y + yOffset + lookDownAmount - topMarginGrounded, pos.y + verticalMaxMove);
			} else if (follow.transform.position.y + yOffset + lookDownAmount < pos.y - bottomMarginGrounded) {
				pos.y = Mathf.Max(follow.transform.position.y + yOffset + lookDownAmount + bottomMarginGrounded, pos.y - verticalMaxMove);
			}
		} else {
			// use airborne window y margins
			if (follow.transform.position.y + yOffset + lookDownAmount > pos.y + topMarginAirborne) {
				pos.y = Mathf.Min(follow.transform.position.y + yOffset + lookDownAmount - topMarginAirborne, pos.y + verticalMaxMove);
			} else if (follow.transform.position.y + yOffset + lookDownAmount < pos.y - bottomMarginAirborne) {
				pos.y = Mathf.Max(follow.transform.position.y + yOffset + lookDownAmount + bottomMarginAirborne, pos.y - verticalMaxMove);
			}
		}
		return pos;
	}
	
	public void toggleOnGround(bool onGround) {
		// transition top and bottom bounds between Grounded and Airborne margins
		if (onGround) {
			// transition margin toward Grounded
			topMargin = Mathf.Max(topMargin - verticalMaxMove, topMarginGrounded);
			bottomMargin = Mathf.Max(bottomMargin - verticalMaxMove, bottomMarginGrounded);
		} else {
			// transition margin toward Airborne
			// since airborne margins are more relaxed, instantly set the margins
			topMargin = topMarginAirborne;
			bottomMargin = bottomMarginAirborne;
		}
	}
	
	public void toggleLookDown (bool lookDown) {
		if (lookDown) {
			lookDownAmount = -9f;
		} else {
			lookDownAmount = 0f;
		}
	}
}
