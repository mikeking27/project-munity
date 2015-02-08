using UnityEngine;
using System.Collections;
using Types;

public class GirlController : MonoBehaviour
{
	public float maxSpeed = 8f;
	private float walkSpeed = 8f;
	public float runSpeed = 13f;
	private bool facingRight = true;
	
	private Animator anim;
	private CameraController cam;
	
	public bool grounded = false;
	public bool grab = false;
	int groundedTrigger = 0;
	public Transform groundCheck;
	public Transform grabTopCheck;
	public Transform grabBottomCheck;
	float groundRadius = 0.01f;
	public LayerMask whatIsGround;
	public float jumpforce = 15f;

	private bool hit = false;
	private int hitCounter = 0;

	private float grabTimer = 0f;
	
	private InputCollection lastCollection =  new InputCollection();
	
	// Use this for initialization
	void Start ()
	{
		Globals.girl = this;
		anim = GetComponent<Animator>();
		cam = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		grab = (!Physics2D.OverlapCircle (grabTopCheck.position, groundRadius, whatIsGround)) && Physics2D.OverlapCircle (grabBottomCheck.position, groundRadius, whatIsGround);

		//cam.toggleOnGround (grounded);
		groundedTrigger++;
		
		if (groundedTrigger > 30)
			anim.SetBool("OnGround", grounded);
		
		if (hit && hitCounter > 0)
			hitCounter--;
		else
			hit = false;
		
		
//		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = InputHandler.GetAxis ();
		float absMove = Mathf.Abs (move * maxSpeed);

		if (grab && Mathf.Abs (rigidbody2D.velocity.y) < 0.2f && grabTimer < Time.time) {
			grabTimer = Time.time + 1.5f;
			anim.SetTrigger ("WallGrab");
		}

		anim.SetFloat("Speed", absMove);
		
		if (!hit) {
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		}
		if(move > 0.05f &&!facingRight)
			Flip ();
		else if(move < -0.05f && facingRight)
			Flip ();
	}
	
	void Update()
	{
		InputCollection input = InputHandler.GetCollection ();

		if (groundedTrigger > 50 && grounded && input.jump) {
			groundedTrigger = 0;
			anim.SetBool ("OnGround", false);
			anim.SetTrigger ("Jump");
			Invoke ("Jump", 0.2f);
		}

		if (grounded) {
			if (cam != null)
				cam.toggleLookDown(input.down);
		}
		
		lastCollection = input;
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void Hit(int frames = 20) {
		hit = true;
		hitCounter = frames;
		anim.SetTrigger("Hit");
	}

	public void Jump() {
		rigidbody2D.AddForce (new Vector2 (0, jumpforce));
	}
	
}




