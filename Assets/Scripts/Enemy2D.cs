/// <summary>
/// Enemy2D.cs
/// Written by William George
/// 
/// Dummy script that currently just left player know when they've hit an enemy
/// </summary>
using UnityEngine;
using System.Collections;

public class Enemy2D : MonoBehaviour {

	public GameManager gm;		//Reference to scenes Game Manager

	int attackDamage = 1;		//How much damage player takes after hitting enenmy

	/// <summary>
	/// Checks is player hits enemy and currently deals the player damage when true
	/// </summary>
	/// <param name="col">Col - the enemy's collider</param>
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			gm.SendMessage("PlayerDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
			gm.controller2D.SendMessage("TakenDamage", SendMessageOptions.DontRequireReceiver);
		}
	}

}
