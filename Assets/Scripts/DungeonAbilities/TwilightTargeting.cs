/// <summary>
/// TwilightTargeting.cs
/// William George
/// 7/3/2014
/// Script for Twilights dungeon ability. Targets available objects to move.
/// Taken from tutorial by BurgZerg Arcade.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwilightTargeting : MonoBehaviour {
	public List <Transform> targets;
	
	public Transform selectedTarget;
	
	private Transform myTransform;

	public float effectRadius;
	public float characterRadius;
	public float blockRebound;

	public LayerMask twiBlocks;

	public float moveSpeed;

	public float 	xRange,
					yRange,
					zRange;
	
	// Use this for initialization
	void Start () {
		targets  = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		AddAllTargets ();
		effectRadius = 10.5f;
		moveSpeed = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddAllTargets(){
		targets.Clear ();

		Collider[] TwiBoxes = Physics.OverlapSphere(transform.position, effectRadius, twiBlocks);

		foreach (Collider target in TwiBoxes) {
			AddTarget (target.transform.root.gameObject.transform);
		}
		//lets player move if no blocks are available to be selected
		if (targets.Count == 0) {
			selectedTarget = null;
		}
	}

	public void AddTarget(Transform target){
		targets.Add (target);
	}
	
	private void SortTargetsByDistance(){
		targets.Sort(delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
		});
		
	}
	
	public void TargetObject(){
		if (targets.Count > 0) {
			if (selectedTarget == null) {
				SortTargetsByDistance ();
				selectedTarget = targets [0];
			} else {
				int index = targets.IndexOf (selectedTarget);
				if (index < targets.Count - 1) {
					index++;
				} else {
					index = 0;
				}
				DeselectTarget ();
				selectedTarget = targets [index];
			}	
			SelectTarget ();
		}
	}
	
	private void SelectTarget(){
		//glow enable
		Transform glow = selectedTarget.FindChild("Glow");
		if(glow == null){
			Debug.LogError("Glow not found " + selectedTarget.name);
			return;
		}		
		glow.GetComponent<SpriteRenderer>().enabled = true;
	}
	
	public void DeselectTarget(){
		//glow disable
		if(selectedTarget != null){
			selectedTarget.FindChild("Glow").GetComponent<SpriteRenderer>().enabled = false;
			selectedTarget.rigidbody.useGravity = true;
		}

	}
	
	public void MoveBlock(Transform player)
	{
		if(selectedTarget != null){
			Vector3 tempPos = selectedTarget.transform.position;
			//Keeps Block from clipping through player
			if(!(tempPos.z < player.position.z + characterRadius && tempPos.z > player.position.z - characterRadius) ||
			   !(tempPos.x < player.position.x + characterRadius && tempPos.x > player.position.x - characterRadius) ||
			   !(tempPos.y < player.position.y + (characterRadius * 2) && tempPos.y > player.position.y - (characterRadius * 2))){
				selectedTarget.rigidbody.useGravity = false;
				//Each outer if statement keeps block within an effect range
				if (tempPos.z < player.position.z + zRange && tempPos.z >= player.position.z - zRange - 0.5f) {
					if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
						selectedTarget.transform.Translate((Vector3.forward * moveSpeed) * Time.deltaTime);
					}
				}
				if (tempPos.z <= player.position.z + zRange + 0.5f && tempPos.z > player.position.z - zRange) {
					if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
						selectedTarget.transform.Translate((Vector3.back * moveSpeed) * Time.deltaTime);
					}
				}
				if (tempPos.x < player.position.x + xRange && tempPos.x >= player.position.x - xRange - 0.5f) {
					if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
						selectedTarget.transform.Translate((Vector3.right * moveSpeed) * Time.deltaTime);
					}
				}
					if (tempPos.x <= player.position.x + xRange + 0.5f && tempPos.x > player.position.x - xRange) {
					if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
						selectedTarget.transform.Translate((Vector3.left * moveSpeed) * Time.deltaTime);
					}
				}
				if (tempPos.y <= player.position.y + yRange) {
					if (Input.GetKey (KeyCode.Space)) {
						selectedTarget.transform.Translate((Vector3.up * moveSpeed) * Time.deltaTime);
					}
				}
			}
			//knocks the block away from character
			else{
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
					selectedTarget.transform.Translate((Vector3.back * moveSpeed * blockRebound) * Time.deltaTime);
				}			
				if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					selectedTarget.transform.Translate((Vector3.forward * moveSpeed * blockRebound) * Time.deltaTime);
				}
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
					selectedTarget.transform.Translate((Vector3.left * moveSpeed * blockRebound) * Time.deltaTime);
				}
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
					selectedTarget.transform.Translate((Vector3.right * moveSpeed * blockRebound) * Time.deltaTime);
				}
			}
		}
	}
}



















