using UnityEngine;
using System.Collections;

public class DirectionalTargeting : MonoBehaviour {

	Transform player;
	string fDirection;
	float targetingOffset = 2.0f;
	bool enableYMove;
	public float vertical, verticalModifier;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
		verticalModifier = 0;
	}
	
	// Update is called once per frame
	void Update () {
		fDirection = player.GetComponent<ControllerV3>().GetFaceDirection();
		DetermineFaceDirection ();
	}

	void DetermineFaceDirection(){
		//Debug.Log (fDirection);
		Vector3 targetingPosition = new Vector3(0,0,0);
		if(fDirection.Contains("n")){
			//Debug.Log("North");
			targetingPosition += new Vector3(0,0,targetingOffset);
			this.transform.LookAt(player);
		}
		if(fDirection.Contains("s")){
			//Debug.Log("South");
			targetingPosition += new Vector3(0,0,-targetingOffset);
			this.transform.LookAt(player);
		}
		if(fDirection.Contains("e")){
			//Debug.Log("East");
			targetingPosition += new Vector3(targetingOffset,0,0);
			this.transform.LookAt(player);
		}
		if(fDirection.Contains("w")){
			//Debug.Log("Weast");
			targetingPosition += new Vector3(-targetingOffset,0,0);
			this.transform.LookAt(player);
		}
		if (enableYMove) {
			vertical = Input.GetAxis ("Vertical");
			targetingPosition += new Vector3(0, vertical * verticalModifier, 0);
		}
		this.transform.position = player.position + targetingPosition;
	}

	public void SetTargetingOffset(float offset){
		targetingOffset = offset;
	}

	public string GetFDirection(){return fDirection;}

	public bool GetEnableYMove(){return enableYMove;}
	public void SetEnableYMove(bool b){enableYMove = b;}

	public void SetVerticalModifier(float m){verticalModifier = m;}
}
