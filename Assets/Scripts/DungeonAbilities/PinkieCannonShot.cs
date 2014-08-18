using UnityEngine;
using System.Collections;

public class PinkieCannonShot : MonoBehaviour {

	public GameObject player, targeting;
	
	public float 	cannonSpeed, 					//100 seems good for rigidbody.velocity ,5000 when using AddForce
					delay;
	public Vector3 moveDirec;

	Transform playerTransform, targetingTransform;	//To get updated positions
	
	float range = 5.0f;
	float verticalModifierPinkie = 3.0f;
	float cooldown;

	// Use this for initialization
	void Start () {
		moveDirec = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		playerTransform = player.transform;
		targetingTransform = targeting.transform;
	}

	public void AimCannon(){
		player.GetComponent<ControllerV3> ().gm.SetCharacterMoveAllowed (false);
		targeting.GetComponent<DirectionalTargeting> ().SetEnableYMove (true);
		targeting.GetComponent<DirectionalTargeting> ().SetVerticalModifier(verticalModifierPinkie);
	}

	public void ShootCannon(){
		moveDirec.x = targetingTransform.position.x - playerTransform.position.x;
		moveDirec.y = Mathf.Abs (targetingTransform.position.y - playerTransform.position.y + 1.0f);
		moveDirec.z = targetingTransform.position.z - playerTransform.position.z;
		//Makes sure a second rigid body isn't added
		if (player.GetComponent<Rigidbody> () == null) {
			Rigidbody tempRigidBody = player.AddComponent<Rigidbody> ();
			player.rigidbody.useGravity = false;
			Destroy (tempRigidBody, 1.0f);
		}
		player.rigidbody.velocity = moveDirec * cannonSpeed;
		targeting.GetComponent<DirectionalTargeting> ().SetEnableYMove (false);
		targeting.GetComponent<DirectionalTargeting> ().SetVerticalModifier(player.GetComponent<ControllerV3>().GetDefaultVerticalModifier());
	}
	
	public float GetRange(){return range;}
}
