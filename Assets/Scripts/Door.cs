/// <summary>
/// Door.cs
/// Written by William Geroge
/// 
/// Loads new scene after going through a door
/// </summary>
using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameManager gm;			//Scenes Game Manager
	public Controller2DRB player;		//Player using door

	public string doorName = "Scene1";	//Name of door to warp to
	public Vector3 warpPosition;		//Location player should warp to after going through door
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//Checkes player has entered a door
		if (other.tag == "Player") {
			//gm.SaveLastLevel();
			Application.LoadLevel(doorName);
			//Should move player to desired location near portal, but not working...
			if (warpPosition != Vector3.zero){	
				player.TeliportPlayer(warpPosition);				
				warpPosition = Vector3.zero;				
			}
		}
	}
}
