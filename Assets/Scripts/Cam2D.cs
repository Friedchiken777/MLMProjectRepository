/// <summary>
/// Cam2D.cs
/// Written by William George
/// 
/// Basic script to make the 2D camera follow the player
/// </summary>
using UnityEngine;
using System.Collections;

public class Cam2D : MonoBehaviour {

	Transform target;

	/*
	//Player for camera to follow
	public Transform player;

	//Camera move rate
	public float smoothRate = 0.5f;

	private Transform thisTransform;
	private Vector2 velocity;
	*/
	// Use this for initialization
	void Start () {
		/*
		thisTransform = transform;
		velocity = new Vector2 (0.5f, 0.5f);
		*/

		target = GameObject.Find ("Player").transform;
	
	}
	
	// Update is called once per frame
	void Update () {

			/*
			//Temp storage of player position
			Vector2 newPos2D = Vector2.zero;
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, ref velocity.x, smoothRate);
			newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, (player.position.y + 2), ref velocity.y, smoothRate);

			//Update camera position to center on character
			Vector3 newPos = new Vector3 (newPos2D.x, newPos2D.y, transform.position.z);
			transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
			*/


			transform.position = target.position + new Vector3 (0, 11, -12);

	}
}
