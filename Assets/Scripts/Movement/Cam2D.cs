/// <summary>
/// Cam2D.cs
/// Written by William George
/// 
/// Basic script to make the 2D camera follow the player
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cam2D : MonoBehaviour {
	
	Transform target;
	public GameObject targetsTargeter;
	public float cameraAdjustRD;
	RaycastHit obstructor;
	public List <RaycastHit> obstructions;
	
	public LayerMask seeThrough;

	Vector3 cameraPos = Vector3.zero;
	Vector3 velocity;
	public float smoothRate = 0.5f;


	// Use this for initialization
	void Start () {
		velocity = new Vector3 (0.5f,0.5f,0.5f);

		target = GameObject.Find ("Player").transform;
		obstructions = new List <RaycastHit> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (target.gameObject.GetComponent<ControllerV3> ().anim.GetBool ("RainbowDashWalk") && target.gameObject.GetComponent<DashArchery> ().GetPowerActive ()) {
			string fDirection = targetsTargeter.GetComponent<DirectionalTargeting>().GetFDirection();
			if(fDirection.Contains("n")){
				//transform.position = target.position + new Vector3 (0, 15, -8);
				cameraPos.x = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothRate);
				cameraPos.y = Mathf.SmoothDamp(transform.position.y, target.position.y + 15, ref velocity.y, smoothRate);
				cameraPos.z = Mathf.SmoothDamp(transform.position.z, target.position.z - 8, ref velocity.z, smoothRate);
			}
			if(fDirection.Contains("s")){
				//transform.position = target.position + new Vector3 (0, 7, -15);
				cameraPos.x = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothRate);
				cameraPos.y = Mathf.SmoothDamp(transform.position.y, target.position.y + 7, ref velocity.y, smoothRate);
				cameraPos.z = Mathf.SmoothDamp(transform.position.z, target.position.z - 15, ref velocity.z, smoothRate);
			}
			if(fDirection.Contains("e")){
				//transform.position = target.position + new Vector3 (11, 11, -12);
				cameraPos.x = Mathf.SmoothDamp(transform.position.x, target.position.x + 11, ref velocity.x, smoothRate);
				cameraPos.y = Mathf.SmoothDamp(transform.position.y, target.position.y + 11, ref velocity.y, smoothRate);
				cameraPos.z = Mathf.SmoothDamp(transform.position.z, target.position.z - 12, ref velocity.z, smoothRate);
			}
			if(fDirection.Contains("w")){
				//transform.position = target.position + new Vector3 (-11, 11, -12);
				cameraPos.x = Mathf.SmoothDamp(transform.position.x, target.position.x - 11, ref velocity.x, smoothRate);
				cameraPos.y = Mathf.SmoothDamp(transform.position.y, target.position.y + 11, ref velocity.y, smoothRate);
				cameraPos.z = Mathf.SmoothDamp(transform.position.z, target.position.z - 12, ref velocity.z, smoothRate);
			}
		} else {
			//transform.position = target.position + new Vector3 (0, 11, -12);
			cameraPos.x = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothRate);
			cameraPos.y = Mathf.SmoothDamp(transform.position.y, target.position.y + 11, ref velocity.y, smoothRate);
			cameraPos.z = Mathf.SmoothDamp(transform.position.z, target.position.z - 12, ref velocity.z, smoothRate);
		}

		Vector3 newPos = new Vector3 (cameraPos.x, cameraPos.y, cameraPos.z);
		transform.position = Vector3.Slerp (transform.position, newPos, Time.time);

		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast (transform.position, fwd, out obstructor, Vector3.Distance (transform.position, target.position), seeThrough)) {
			//print (obstructor.collider.gameObject.name);
			obstructions.Add (obstructor);
			//Debug.Log (obstructions.Count);
			Color c = obstructor.collider.gameObject.renderer.material.color;
			obstructor.collider.gameObject.renderer.material.color = new Vector4 (c.r, c.g, c.b, 0.6f);
		} 
		else {
			if(obstructions.Count > 0){
				foreach (RaycastHit block in obstructions) {
					Color c = block.collider.gameObject.renderer.material.color;
					block.collider.gameObject.renderer.material.color = new Vector4 (c.r, c.g, c.b, 1.0f);
				}
				obstructions.Clear();
			}
		}
	}
}
