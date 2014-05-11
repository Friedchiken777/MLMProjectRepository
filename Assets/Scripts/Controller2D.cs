/// <summary>
/// Controller2D.cs
/// Written by William Geroge
/// 
/// Script to control 2D movment of player character
/// </summary>
using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {

	public GameManager gm;						//Reference to scenes game manager
	CharacterController characterControler;		//Reference to Character Controller
	Door door;									//Reference to levels door (may need to be a list later...)
	
	//public float gravity = 0.7f;				//Variable for gravity
	//public float jumpHeight = 15;				//Variable for jump height

	public float walkSpeed = 4;					//speed player walks at
	
	Vector3 moveDirec = Vector3.zero;			//Player movement vector
	float horizontal = 0;						//Horizontal key velocity
	float vertical = 0;							//Vertical key velocity
	
	public Animator anim;						//Reference to player animations
	
	float takenDamageTimer = 0.2f;				//Damage Indicator Timer

	// Use this for initialization
	void Start () {
		characterControler = GetComponent<CharacterController> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		//Controls Character Controller
		characterControler.Move (moveDirec * Time.deltaTime);
		//Gets input directions
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Setting Variables for Animation
		anim.SetFloat ("SpeedX", horizontal);			//float
		anim.SetFloat ("SpeedY", vertical);				//float
		anim.SetBool ("Disable", gm.GetMenuOpen ());	//bool

		//gives player gravity
		//moveDirec.y -= (gravity + Time.deltaTime);

		//Sets left and right movement to default keys
		if ((horizontal > 0.01f || horizontal < 0.01f) && !gm.GetMenuOpen()) {
			moveDirec.x = horizontal * walkSpeed;
		}
		//removes sliding
		else {
			moveDirec.x = 0;
		}
		//Sets up and down movement to default keys
		if ((vertical > 0.01f || vertical < 0.01f) && !gm.GetMenuOpen()) {
			moveDirec.y = vertical * walkSpeed;
		}
		//removes sliding
		else {
			moveDirec.y = 0;
		}
		//Jump
		//if (characterControler.isGrounded) {
		//	if(Input.GetKeyDown(KeyCode.Space)){
		//		moveDirec.y = jumpHeight;
		//	}
		///}
	}

	/// <summary>
	/// Teliports the palyer.
	/// Should mave player to a given position, doesn't work for some reason...
	/// </summary>
	/// <param name="pos">Position to be teliported to.</param>
	public void TeliportPalyer(Vector3 pos){
		//this.transform.position.x = pos.x;
		//this.transform.position.y = pos.y;
		//this.transform.position.z = pos.z;
		Debug.Log (pos.ToString());
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
