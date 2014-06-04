using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	
	Transform target;
	public float shadowScaleSize;


	// Use this for initialization
	void Start () {	
		target = GameObject.Find ("ShadowRay").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		//Raycast to determine Y position of new shadow location
		RaycastHit floor;
		Ray shadowRay = new Ray (target.position, Vector3.down);
		Physics.Raycast (shadowRay, out floor);

		//variables for now shadow location
		float tempX = target.position.x;
		float tempY = floor.point.y + 0.2f;
		float tempZ = target.position.z;
		Vector3 shadowPos;

		//Checks that ray isn't hitting the shadow and set new shadow position
		if (floor.collider.tag != "Shadow") {
			shadowPos = new Vector3 (tempX, tempY, tempZ);
		}
		else{
			shadowPos = new Vector3 (tempX, this.transform.position.y, tempZ); 
		}
		transform.position = shadowPos;

		//Scale the shadow when jumping
		float scaleFactor = 1/(floor.distance+shadowScaleSize);
		Vector3 shadowScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
		this.transform.localScale = shadowScale;

	}
}
