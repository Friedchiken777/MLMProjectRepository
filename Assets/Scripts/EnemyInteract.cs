/// <summary>
/// Enemy2D.cs
/// Written by William George
/// 
/// Dummy script that currently just left player know when they've hit an enemy
/// </summary>
using UnityEngine;
using System.Collections;

public class EnemyInteract : MonoBehaviour {

	public GameManager gm;		//Reference to scenes Game Manager

	//int attackDamage = 1;		//How much damage player takes after hitting enenmy

	/// <summary>
	/// Checks is player hits enemy and currently deals the player damage when true
	/// </summary>
	/// <param name="col">Col - the enemy's collider</param>
	void OnTriggerEnter(Collider col){
		Application.LoadLevel("BattleScene1");
		/*if (col.gameObject.tag == "Player") {
			gm.SendMessage("PlayerDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
			//gm.controller.SendMessage("TakenDamage", SendMessageOptions.DontRequireReceiver);
		}*/
	}

}
