/// <summary>
/// Controller2D.cs
/// Written by William Geroge
/// 
/// Script to control 2D movment of player character
/// /// THIS SCRIPT IS NO LONGER NEEDED IN THE PROJECT BUT WILL BE LEFT AS A REMINDER THAT JUMPING IS HARD OR SOMETHING LIKE THAT
/// (It can really be deleted, I just didn't have the heart :P)
/// </summary>
using UnityEngine;
using System.Collections;

public class Controller2DRB : MonoBehaviour {

	public GameManager gm;						//Reference to scenes game manager
	//CharacterController characterControler;		//Reference to Character Controller (requires CaracterController be added to player
	Door door;									//Reference to levels door (may need to be a list later...)
	
	float gravity;								//Variable for gravity
	public float jumpHeight;					//Variable for jump height
	bool jump;									//Checks if character is jumping
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	bool moving;
	bool south;
	//Vector3 yJump;
	Vector3 pos;

	public float walkSpeed;						//speed player walks at
	
	//Vector3 moveDirec = Vector3.zero;			//Player movement vector
	int diag;
	//float horizontal = 0;						//Horizontal key velocity
	//float vertical = 0;						//Vertical key velocity

	//public Transform groundStart, groundEnd;

	public Animator anim;						//Reference to player animations
	
	float takenDamageTimer = 0.5f;				//Damage Indicator Timer

	// Use this for initialization
	void Start () {
		//gravity = 100;
		jumpHeight = 5500;
		jump = true;
		walkSpeed = 2000;
		diag = 0;
		moving = false;
		south = true;
		pos = transform.position;
		//yJump = new Vector3 (0,-1,0);
		//characterControler = GetComponent<CharacterController> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		#region First movement method
		/*
		//Controls Character Controller
		characterControler.Move (moveDirec * Time.deltaTime);
		//Gets input directions
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Setting Variables for Animation
		anim.SetFloat ("SpeedX", horizontal);			//float
		anim.SetFloat ("SpeedY", vertical);				//float
		anim.SetBool ("Disable", gm.GetMenuOpen ());	//bool

		//Sets left and right movement to default keys
		if ((horizontal > 0.01f || horizontal < 0.01f) && !gm.GetMenuOpen ()) {
			moveDirec.x = horizontal * walkSpeed;
		}
		//removes sliding
		else {
			moveDirec.x = 0;
		}
		//Sets up and down movement to default keys
		if ((vertical > 0.01f || vertical < 0.01f) && !gm.GetMenuOpen ()) {
			moveDirec.y = vertical * walkSpeed;
		}
		//removes sliding
		else {
			moveDirec.y = 0;
		}

		//Jump
		if(Input.GetKeyDown(KeyCode.Space)){
			if (jump == false) {
				moveDirec.z = jumpHeight;
				//moveDirec.y = jumpHeight;
				//pos = transform.position;
				//transform.position = Vector3.Lerp(transform.position ,transform.position + yJump, 1);
				jump = true;
				anim.SetBool ("Jump", jump);
			}
		}
		//gives player gravity
		if (jump == true){
			//if(transform.position.y != pos.y){
				//Vector3 temp = new Vector3(transform.position.x, pos.y, transform.position.z);
				//transform.Translate(temp);
				//transform.position = Vector3.Lerp(transform.position ,transform.position + yJump, 1);
			//}
			//moveDirec.y += (gravity + Time.deltaTime);
			moveDirec.z += (gravity + Time.deltaTime);
		}
		if(transform.position.z > 0){
			jump = false;
			anim.SetBool ("Jump", jump);
			moveDirec.z = 0;
			SetTransformZ(0);
		}
		*/
		#endregion
				
		//Setting Variables for Animation
		anim.SetBool ("Disable", gm.GetMenuOpen ());	//bool
		moving = false;
		//Sets left and right movement to default keys
		if(!gm.GetMenuOpen ()){
			if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) {
				diag++;
				moving = true;
				south = false;
				rigidbody.AddForce(Vector3.forward * walkSpeed);
				anim.SetBool ("North", true);
				anim.SetBool ("South", false);
				anim.SetBool ("East", false);
				anim.SetBool ("West", false);
				anim.SetBool ("WalkNorth", true);
			}
			else{
				anim.SetBool ("WalkNorth", false);
			}
			if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) {
				diag++;
				moving = true;
				south = true;
				rigidbody.AddForce(-Vector3.forward * walkSpeed);
				anim.SetBool ("South", true);
				anim.SetBool ("North", false);
				anim.SetBool ("East", false);
				anim.SetBool ("West", false);
				anim.SetBool ("WalkSouth", true);
			}
			else{
				anim.SetBool ("WalkSouth", false);
			}
			if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)) {
				moving = true;
				south = false;
				//Takes out diagonal double speed
				diag++;
				Debug.Log(diag);
				if (diag > 1){
					Vector3 temp = new Vector3(Vector3.right.x,0,0);
					rigidbody.AddForce(temp * walkSpeed);
				}else{
					rigidbody.AddForce(Vector3.right * walkSpeed);
				}
				anim.SetBool ("East", true);
				anim.SetBool ("South", false);
				anim.SetBool ("North", false);
				anim.SetBool ("West", false);
				anim.SetBool ("WalkEast", true);
			}else{
				anim.SetBool ("WalkEast", false);
			}
			if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) {
				moving = true;
				south = false;
				//Takes out diagonal double speed
				diag++;
				Debug.Log(diag);
				if (diag > 1){
					Vector3 temp = new Vector3(Vector3.right.x,0,0);
					rigidbody.AddForce(-temp * walkSpeed);
				}else{
					rigidbody.AddForce(-Vector3.right * walkSpeed);
				}
				anim.SetBool ("West", true);
				anim.SetBool ("South", false);
				anim.SetBool ("East", false);
				anim.SetBool ("North", false);
				anim.SetBool ("WalkWest", true);
			}
			else{
				anim.SetBool ("WalkWest", false);
			}
			diag = 0;
			//jump = Physics.Linecast (groundStart.position, groundEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
			//Debug.Log ("Jump: " + jump);
			//anim.SetBool ("Jump", jump);
			if (Input.GetKeyDown(KeyCode.Space) && grounded){
				//jump = false;
				//anim.SetBool ("Jump", jump);
				//SetTransformY(transform.position.y - jumpHeight);
				//rigidbody.AddForce(0,jumpHeight,0);
				anim.SetBool("Ground", false);
				rigidbody.AddForce(Vector3.up * jumpHeight);
			}
		}
	}

	void FixedUpdate () {
		grounded = Physics.CheckSphere (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rigidbody.velocity.y);
	}

	/// <summary>
	/// Teliports the palyer.
	/// Should mave player to a given position, doesn't work for some reason...
	/// </summary>
	/// <param name="pos">Position to be teliported to.</param>
	public void TeliportPlayer(Vector3 pos){
		transform.position = new Vector3 (pos.x,pos.y,pos.z);
		Debug.Log (pos.ToString());
	}

	/// <summary>
	/// Sets the transform x.
	/// </summary>
	/// <param name="n">N.</param>
	void SetTransformX(float n){		
		transform.position = new Vector3(n, transform.position.y, transform.position.z);		
	}

	/// <summary>
	/// Sets the transform y.
	/// </summary>
	/// <param name="n">N.</param>
	void SetTransformY(float n){		
		transform.position = new Vector3(transform.position.x, n, transform.position.z);		
	}

	/// <summary>
	/// Sets the transform z.
	/// </summary>
	/// <param name="n">N.</param>
	void SetTransformZ(float n){		
		transform.position = new Vector3(transform.position.x, transform.position.y, n);		
	}

	/// <summary>
	/// Makes player blink after hitting enemy
	/// </summary>
	/// <returns>Rendering info for player sprite</returns>
	public IEnumerator TakenDamage(){
		renderer.enabled = false;
		yield return new WaitForSeconds (takenDamageTimer);
		renderer.enabled = true;
		yield return new WaitForSeconds (takenDamageTimer);
		renderer.enabled = false;
		yield return new WaitForSeconds (takenDamageTimer);
		renderer.enabled = true;
	}



}
