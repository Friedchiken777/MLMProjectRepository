/// <summary>
/// ControllerV3.cs
/// Written by William Geroge
/// 
/// Script to control movment of player character
/// Jumping and movement working :)
/// </summary>
using UnityEngine;
using System.Collections;

public class ControllerV3 : MonoBehaviour {
	
	public GameManager gm;						//Reference to scenes game manager
	CharacterController characterControler;		//Reference to Character Controller (requires CaracterController be added to player
	Door door;									//Reference to levels door (may need to be a list later...)

	public float gravity;
	public float fallGravity;
	public float jumpHeight;					//Variable for jump height
	bool jump;									//Checks if character is jumping
	int jumpGroundClear;						//Accounts for three frames where character is "grounded" after jump
	bool grounded = false;

	Vector3 pos;								
	
	public float walkSpeed;						//speed player walks at
	
	Vector3 moveDirec = Vector3.zero;			//Player movement vector
	float horizontal = 0;						//Horizontal key velocity
	float vertical = 0;							//Vertical key velocity
	
	public Animator anim;						//Reference to player animations
	
	float takenDamageTimer = 0.5f;				//Damage Indicator Timer
	
	// Use this for initialization
	void Start () {
		gravity = -21f;
		fallGravity = -4.1f;
		jumpHeight = 7;
		jump = true;
		walkSpeed = 5;
		pos = transform.position;
		characterControler = GetComponent<CharacterController> ();
		anim = gameObject.GetComponent<Animator> ();
		anim.SetBool ("TwilightWalk", true);
	}
	
	// Update is called once per frame
	void Update () {
		#region Movement

		//Controls Character Controller
		characterControler.Move (moveDirec * Time.deltaTime);
		//Gets input directions
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Setting Variables for Animation
		anim.SetFloat ("SpeedX", horizontal);			//float
		anim.SetFloat ("SpeedY", vertical);				//float
		anim.SetBool ("Disable", gm.GetMenuOpen ());	//bool

		if(!gm.GetMenuOpen ()){
			//Sets left and right movement to default keys
			if ((horizontal > 0.01f || horizontal < 0.01f)) {
				moveDirec.x = horizontal * walkSpeed;
			}
			//removes sliding
			else {
				moveDirec.x = 0;
			}
			//Sets up and down movement to default keys
			if ((vertical > 0.01f || vertical < 0.01f)) {
				moveDirec.z = vertical * walkSpeed;
			}
			//removes sliding
			else {
				moveDirec.z = 0;
			}

			//checks for jump
			if (Input.GetButton ("Jump") && grounded) {
				moveDirec.y = jumpHeight;
				jump = true;
				jumpGroundClear = 0;
			}
			anim.SetBool("Jump", jump);
			if (jump && grounded){
				if(jumpGroundClear++ > 3){
					jump = false;
				}
			}
			if (!jump && !grounded){
				moveDirec.y = fallGravity;
			}

			SetWalkAnim();
		}
		#endregion

		#region Character Animation Switch
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			anim.SetBool("AppleJackWalk", false);
			anim.SetBool("TwilightWalk", true);
			anim.SetBool("PinkiePieWalk",false);
			anim.SetBool("RainbowDashWalk", false);
			anim.SetBool("FluttershyWalk", false);
			anim.SetBool("RarityWalk",false);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			anim.SetBool("AppleJackWalk", true);
			anim.SetBool("TwilightWalk", false);
			anim.SetBool("PinkiePieWalk",false);
			anim.SetBool("RainbowDashWalk", false);
			anim.SetBool("FluttershyWalk", false);
			anim.SetBool("RarityWalk",false);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			anim.SetBool("AppleJackWalk", false);
			anim.SetBool("TwilightWalk", false);
			anim.SetBool("PinkiePieWalk",true);
			anim.SetBool("RainbowDashWalk", false);
			anim.SetBool("FluttershyWalk", false);
			anim.SetBool("RarityWalk",false);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			anim.SetBool("AppleJackWalk", false);
			anim.SetBool("TwilightWalk", false);
			anim.SetBool("PinkiePieWalk",false);
			anim.SetBool("RainbowDashWalk", true);
			anim.SetBool("FluttershyWalk", false);
			anim.SetBool("RarityWalk",false);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			anim.SetBool("AppleJackWalk", false);
			anim.SetBool("TwilightWalk", false);
			anim.SetBool("PinkiePieWalk",false);
			anim.SetBool("RainbowDashWalk", false);
			anim.SetBool("FluttershyWalk", true);
			anim.SetBool("RarityWalk",false);
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			anim.SetBool("AppleJackWalk", false);
			anim.SetBool("TwilightWalk", false);
			anim.SetBool("PinkiePieWalk",false);
			anim.SetBool("RainbowDashWalk", false);
			anim.SetBool("FluttershyWalk", false);
			anim.SetBool("RarityWalk",true);
		}
		#endregion
	}
	
	void FixedUpdate () {
		// Apply gravity
		moveDirec.y += gravity * Time.deltaTime;
		//Check if character is on the ground
		grounded = characterControler.isGrounded;

		anim.SetBool ("Ground", grounded);
		//anim.SetFloat ("vSpeed", rigidbody.velocity.y);
	}
	
	/// <summary>
	/// Teliports the palyer.
	/// Should mave player to a given position, doesn't work for some reason...
	/// </summary>
	/// <param name="pos">Position to be teliported to.</param>
	public void TeliportPlayer(Vector3 pos){
		characterControler.transform.position = new Vector3 (pos.x,pos.y,pos.z);
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

	/// <summary>
	/// Sets the walk animation.
	/// </summary>
	void SetWalkAnim(){
		if (vertical > 0.01f) {
			anim.SetBool ("North", true);
			anim.SetBool ("South", false);
			anim.SetBool ("East", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkNorth", true);
		}
		else{
			anim.SetBool ("WalkNorth", false);
		}
		if (vertical < -0.01f) {
			anim.SetBool ("South", true);
			anim.SetBool ("North", false);
			anim.SetBool ("East", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkSouth", true);
		}
		else{
			anim.SetBool ("WalkSouth", false);
		}
		if (horizontal > 0.01f) {
			anim.SetBool ("East", true);
			anim.SetBool ("South", false);
			anim.SetBool ("North", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkEast", true);
		}else{
			anim.SetBool ("WalkEast", false);
		}
		if (horizontal < -0.01f) {
			anim.SetBool ("West", true);
			anim.SetBool ("South", false);
			anim.SetBool ("East", false);
			anim.SetBool ("North", false);
			anim.SetBool ("WalkWest", true);
		}
		else{
			anim.SetBool ("WalkWest", false);
		}
	}
	
}
