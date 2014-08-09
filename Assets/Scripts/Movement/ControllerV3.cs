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
	public GameObject targetingGuide;
	Door door;									//Reference to levels door (may need to be a list later...)

	public float gravity;
	public float fallGravity;
	public float jumpHeight;					//Variable for jump height
	bool jump;									//Checks if character is jumping
	int jumpGroundClear;						//Accounts for three frames where character is "grounded" after jump
	bool grounded = false;							
	
	public float walkSpeed;						//speed player walks at
	
	Vector3 moveDirec = Vector3.zero;			//Player movement vector
	float horizontal = 0;						//Horizontal key velocity
	float vertical = 0;							//Vertical key velocity
	
	public Animator anim;						//Reference to player animations
	string faceDirection;
	
	float takenDamageTimer = 0.5f;				//Damage Indicator Timer

	float defaultRange = 2.0f;
	float defaultVerticalModifier = 2.0f;
	float cooldownPinkie, delayPinkie = 2.0f;
	float cooldownDash, delayDash = 1.5f;
	bool pinkieNotification;
	bool dashNotification;

	//bool waitActive;

	
	// Use this for initialization
	void Start () {
		gravity = -21f;
		fallGravity = -4.1f;
		jumpHeight = 8;
		jump = true;
		walkSpeed = 5;
		characterControler = GetComponent<CharacterController> ();
		anim = gameObject.GetComponent<Animator> ();
		anim.SetBool ("TwilightWalk", true);
		faceDirection = "s";
		pinkieNotification = true;
		dashNotification = true;
	}
	
	// Update is called once per frame
	void Update () {
		#region Movement

		//Controls Character Controller
		characterControler.enabled = gm.GetCharacterMoveAllowed();
		//Checks if player movement is allowed before moving character
		if(characterControler.enabled){
			characterControler.Move (moveDirec * Time.deltaTime);
		}
		//Gets input directions
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Setting Variables for Animation
		anim.SetFloat ("SpeedX", horizontal);
		anim.SetFloat ("SpeedY", vertical);	
		anim.SetBool ("Disable", !gm.GetCharacterMoveAllowed());
		
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
		if(!grounded){
		//	Debug.Log ("air");
		}

		SetWalkAnim();
		#endregion

		#region Character Animation Switch
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			TwilightSetCharacter();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			ApplejackSetCharacter();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			PinkiePieSetCharacter();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			RainbowDashSetCharacter();
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			FluttershySetCharacter();
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			RaritySetCharacter();
		}
		#endregion

		#region Dungeon Abilities
		if(anim.GetBool("PinkiePieWalk") && Time.time > cooldownPinkie + delayPinkie && pinkieNotification){
			Debug.Log("Pinkie Pie Colldown Ended");
			pinkieNotification = false;
		}
		if(anim.GetBool("RainbowDashWalk") && Time.time > cooldownDash + delayDash && dashNotification){
			Debug.Log("Rainbow Dash Colldown Ended");
			dashNotification = false;
		}

		if(Input.GetKeyDown(KeyCode.B)){
			if(anim.GetBool("PinkiePieWalk")){
				if (Time.time > cooldownPinkie + delayPinkie) {
					gm.SetCharacterMoveAllowed(false);
					PerfomCharacterSpecial();
				}
			}
			else if(anim.GetBool("RainbowDashWalk")){
				if (Time.time > cooldownDash + delayDash) {
					gm.SetCharacterMoveAllowed(false);
					PerfomCharacterSpecial();
				}
			}
			else{
				PerfomCharacterSpecial();
			}
		}
		if(Input.GetKey(KeyCode.B)){
			if(anim.GetBool("TwilightWalk")){
				if(gameObject.GetComponent<TwilightTargeting>().selectedTarget != null){
					gameObject.GetComponent<TwilightTargeting>().MoveBlock(transform);
				}
			}
			else if(anim.GetBool("PinkiePieWalk")){
				if (Time.time > cooldownPinkie + delayPinkie) {
					gameObject.GetComponent<PinkieCannonShot>().AimCannon();
				}
			}
			else if(anim.GetBool("RainbowDashWalk")){
				if (Time.time > cooldownDash + delayDash) {
					gameObject.GetComponent<DashArchery>().ChooseArrowAndAim();
				}
			}
		}
		if(Input.GetKeyUp(KeyCode.B)){
			if(anim.GetBool("TwilightWalk")){
				gameObject.GetComponent<TwilightTargeting>().DeselectTarget();
				gm.SetCharacterMoveAllowed(true);
			}
			else if(anim.GetBool("PinkiePieWalk")){
				if (Time.time > cooldownPinkie + delayPinkie) {
					gameObject.GetComponent<PinkieCannonShot>().ShootCannon();
					gm.SetCharacterMoveAllowed(true);
					cooldownPinkie = Time.time;
					pinkieNotification = true;
				}
			}
			else if(anim.GetBool("RainbowDashWalk")){
				if (Time.time > cooldownDash + delayDash) {
					gameObject.GetComponent<DashArchery>().ShootArrow();
					gm.SetCharacterMoveAllowed(true);
					cooldownDash = Time.time;
					dashNotification = true;
				}
			}
			else{
				gm.SetCharacterMoveAllowed(true);
			}
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
	/// Sets the walk animation.
	/// </summary>
	void SetWalkAnim(){
		float animVertical;
		float animHorizontal;
		if (!gm.GetCharacterMoveAllowed ()) {
			animVertical = 0;
			animHorizontal = 0;
		} else{
			animVertical = vertical;
			animHorizontal = horizontal;
		}
		if (animVertical > 0.01f) {
			anim.SetBool ("North", true);
			anim.SetBool ("South", false);
			anim.SetBool ("East", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkNorth", true);
			faceDirection = "n";
		}
		else{
			anim.SetBool ("WalkNorth", false);
		}
		if (animVertical < -0.01f) {
			anim.SetBool ("South", true);
			anim.SetBool ("North", false);
			anim.SetBool ("East", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkSouth", true);
			faceDirection = "s";
		}
		else{
			anim.SetBool ("WalkSouth", false);
		}
		if (animHorizontal > 0.01f) {
			anim.SetBool ("East", true);
			anim.SetBool ("South", false);
			anim.SetBool ("North", false);
			anim.SetBool ("West", false);
			anim.SetBool ("WalkEast", true);
			if(vertical == 0){
				faceDirection = "e";
			}
			else{
				faceDirection += "e";
			}
		}
		else{
			anim.SetBool ("WalkEast", false);
		}
		if (animHorizontal < -0.01f) {
			anim.SetBool ("West", true);
			anim.SetBool ("South", false);
			anim.SetBool ("East", false);
			anim.SetBool ("North", false);
			anim.SetBool ("WalkWest", true);
			if(vertical == 0){
				faceDirection = "w";
			}
			else{
				faceDirection += "w";
			}
		}
		else{
			anim.SetBool ("WalkWest", false);
		}
	}

	void TwilightSetCharacter(){
		anim.SetBool("ApplejackWalk", false);
		anim.SetBool("TwilightWalk", true);
		anim.SetBool("PinkiePieWalk",false);
		anim.SetBool("RainbowDashWalk", false);
		anim.SetBool("FluttershyWalk", false);
		anim.SetBool("RarityWalk",false);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (defaultRange);
	}

	void ApplejackSetCharacter(){
		anim.SetBool("ApplejackWalk", true);
		anim.SetBool("TwilightWalk", false);
		anim.SetBool("PinkiePieWalk",false);
		anim.SetBool("RainbowDashWalk", false);
		anim.SetBool("FluttershyWalk", false);
		anim.SetBool("RarityWalk",false);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (this.GetComponent<AJBuck>().GetRange());
	}

	void RainbowDashSetCharacter(){
		anim.SetBool("ApplejackWalk", false);
		anim.SetBool("TwilightWalk", false);
		anim.SetBool("PinkiePieWalk",false);
		anim.SetBool("RainbowDashWalk", true);
		anim.SetBool("FluttershyWalk", false);
		anim.SetBool("RarityWalk",false);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (this.GetComponent<DashArchery>().GetRange());
	}

	void FluttershySetCharacter(){
		anim.SetBool("ApplejackWalk", false);
		anim.SetBool("TwilightWalk", false);
		anim.SetBool("PinkiePieWalk",false);
		anim.SetBool("RainbowDashWalk", false);
		anim.SetBool("FluttershyWalk", true);
		anim.SetBool("RarityWalk",false);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (defaultRange);
	}

	void PinkiePieSetCharacter(){
		anim.SetBool("ApplejackWalk", false);
		anim.SetBool("TwilightWalk", false);
		anim.SetBool("PinkiePieWalk",true);
		anim.SetBool("RainbowDashWalk", false);
		anim.SetBool("FluttershyWalk", false);
		anim.SetBool("RarityWalk",false);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (this.GetComponent<PinkieCannonShot>().GetRange());
	}

	void RaritySetCharacter(){
		anim.SetBool("ApplejackWalk", false);
		anim.SetBool("TwilightWalk", false);
		anim.SetBool("PinkiePieWalk",false);
		anim.SetBool("RainbowDashWalk", false);
		anim.SetBool("FluttershyWalk", false);
		anim.SetBool("RarityWalk",true);
		targetingGuide.GetComponent<DirectionalTargeting> ().SetTargetingOffset (defaultRange);
	}

	void PerfomCharacterSpecial(){
		if(anim.GetBool("TwilightWalk")){
			Debug.Log ("Twilight Character Special Triggered!");
			gm.SetCharacterMoveAllowed(false);
			gameObject.GetComponent<TwilightTargeting>().AddAllTargets();
			gameObject.GetComponent<TwilightTargeting>().TargetObject();
		}
		else if(anim.GetBool("ApplejackWalk")){
			Debug.Log ("Applejack Character Special Triggered!");
			gm.SetCharacterMoveAllowed(false);
			gameObject.GetComponent<AJBuck>().Buck();
		}
		else if(anim.GetBool("RainbowDashWalk")){
			//gm.SetCharacterMoveAllowed(false);
			Debug.Log ("Rainbow Dash Character Special Triggered!");
		}
		else if(anim.GetBool("FluttershyWalk")){
			Debug.Log ("Fluttershy Character Special Triggered!");
		}
		else if(anim.GetBool("PinkiePieWalk")){
			//gm.SetCharacterMoveAllowed(false);
			Debug.Log ("Pinkie Pie Character Special Triggered!");
		}
		else if(anim.GetBool("RarityWalk")){
			Debug.Log ("Rarity Character Special Triggered!");
			gameObject.GetComponent<RarityReveal>().ShowSecrets();
		}
	}

	public string GetFaceDirection(){return faceDirection;}

	public float GetDefaultVerticalModifier(){return defaultVerticalModifier;}

	/*public IEnumerator Wait(float s){
		waitActive = true;
		Debug.Log ("Hi");
		yield return new WaitForSeconds (s);
		waitActive = false;
		Debug.Log ("Bye");
	}*/

	void OnCollisionEnter(Collision col){
		if(anim.GetBool("PinkiePieWalk") && !grounded){
			if(faceDirection.Contains("n")){
				this.transform.Translate((Vector3.back * 2) * Time.deltaTime);
			}
			if(faceDirection.Contains("s")){
				this.transform.Translate((Vector3.forward * 2) * Time.deltaTime);
			}
			if(faceDirection.Contains("e")){
				this.transform.Translate((Vector3.left * 2) * Time.deltaTime);
			}
			if(faceDirection.Contains("w")){
				this.transform.Translate((Vector3.right * 2) * Time.deltaTime);
			}
		}

	}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 ///////////////////////////////WIP Functions////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
}
