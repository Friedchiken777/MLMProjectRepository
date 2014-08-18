using UnityEngine;
using System.Collections;

public class PuzzleBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		rigidbody.velocity = Vector3.zero;
	}

	void OnCollisionExit(Collision col){
		rigidbody.velocity = Vector3.zero;
	}
}
