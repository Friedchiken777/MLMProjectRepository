using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	
	public Transform target;
	public float shadowScaleSize;
	public LayerMask ground;
	public float yFudger;


	// Use this for initialization
	void Start () {	

	}
	
	// Update is called once per frame
	void Update () {
	
		//Raycast to determine Y position of new shadow location
		RaycastHit floor;
		Ray shadowRay = new Ray (target.position, Vector3.down);
		Physics.Raycast (shadowRay, out floor, 30, ground);

		//variables for now shadow location
		float tempX = target.position.x;
		float tempY = floor.point.y + yFudger;
		float tempZ = target.position.z;
		Vector3 shadowPos;

		//Sets new shadow location
		shadowPos = new Vector3 (tempX, tempY, tempZ);
		transform.position = shadowPos;

		//Scale the shadow when jumping
		float scaleFactor = (3/(floor.distance+shadowScaleSize));
		if (scaleFactor < 0.25f) {
			scaleFactor = 0.25f;
		}
		Vector3 shadowScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
		this.transform.localScale = shadowScale;

		//Makes the shadow lighter when higher
		Color alphaChange = this.renderer.material.color;
		alphaChange.a = scaleFactor;
		this.renderer.material.color = alphaChange;

	}
}
