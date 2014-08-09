using UnityEngine;
using System.Collections;

public class DashArchery : MonoBehaviour {

	public GameObject player, targeting;
	public GameObject[] arrows;

	GameObject currentArrow;
	Transform playerTransform, targetingTransform;	//To get updated positions

	int arrowIndex;

	float range = 2.0f;
	float verticalModifierDash = 1.5f;
	float arrowSpeed = 5.0f;

	bool powerActive, arrowPresent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(powerActive && player.GetComponent<ControllerV3> ().gm.enabled){
			if(!arrowPresent){
				currentArrow = Instantiate(arrows[arrowIndex], player.transform.position, targeting.transform.rotation) as GameObject;
				currentArrow.rigidbody.useGravity = false;
				currentArrow.transform.eulerAngles = new Vector3(
															currentArrow.transform.eulerAngles.x + 90,
															currentArrow.transform.eulerAngles.y - 90,
															currentArrow.transform.eulerAngles.z + 0);
				arrowPresent = true;
			}
			if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){
				Destroy(currentArrow);
				arrowPresent = false;
				arrowIndex++;
				if(arrowIndex > arrows.Length -1){
					arrowIndex = 0;
				}
			}
			else if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)){
				Destroy(currentArrow);
				arrowPresent = false;
				arrowIndex--;
				if(arrowIndex < 0){
					arrowIndex = arrows.Length - 1;
				}
			}
		}
		playerTransform = player.transform;
		targetingTransform = targeting.transform;
	}

	public void ChooseArrowAndAim(){
		player.GetComponent<ControllerV3> ().gm.SetCharacterMoveAllowed (false);
		powerActive = true;
		targeting.GetComponent<DirectionalTargeting> ().SetEnableYMove (true);
		targeting.GetComponent<DirectionalTargeting> ().SetVerticalModifier(verticalModifierDash);
	}

	public void ShootArrow(){
		string fDirection = targeting.GetComponent<DirectionalTargeting> ().GetFDirection ();
		Vector3 arrowDirection = Vector3.zero;
		if(fDirection.Contains("n")){
			arrowDirection += Vector3.forward;
		}
		if(fDirection.Contains("s")){
			arrowDirection += Vector3.back;
		}
		if(fDirection.Contains("e")){
			arrowDirection += Vector3.right;
		}
		if(fDirection.Contains("w")){
			arrowDirection += Vector3.left;
		}
		arrowDirection.x += targetingTransform.position.x - playerTransform.position.x;
		arrowDirection.y += targetingTransform.position.y - playerTransform.position.y;
		arrowDirection.z += targetingTransform.position.z - playerTransform.position.z;
		currentArrow.rigidbody.velocity = arrowDirection * arrowSpeed;
		Destroy (currentArrow, 3.0f);
		arrowPresent = false;
		targeting.GetComponent<DirectionalTargeting> ().SetEnableYMove (false);
		targeting.GetComponent<DirectionalTargeting> ().SetVerticalModifier(player.GetComponent<ControllerV3>().GetDefaultVerticalModifier());
		powerActive = false;
		currentArrow.rigidbody.useGravity = true;
	}

	public float GetRange(){return range;}

	public bool GetPowerActive(){return powerActive;}
}
