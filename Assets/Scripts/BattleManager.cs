using UnityEngine;
using System.Collections;
using System.Collections.Generic;			//Added to use lists

public class BattleManager : MonoBehaviour {

	public GameObject[] enemies, players, enemySpawns, playerSpawns; 
	public List<GameObject> fieldEnemies, fieldPlayers;
	public int enemiesInBattle;

	int turn;
	int phase;

	// Use this for initialization
	void Start () {	
		SpawnEnemies ();
		SpawnPlayers ();
		turn = 1;
		phase = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (turn == 2) {
			EnemyTurn ();
		}
	}

	void OnGUI(){
		//Players Turn
		if (turn == 1) {
			//Select action
			if(phase == 0){
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Attack")){
					phase = 1;
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Switch")){
					phase = 2;
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.5f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Run")){
					phase = 3;
				}
			}
			//Chose attack
			if(phase == 1){
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Confirm")){
					Debug.Log("Player Attacked");
					turn = 2;
					phase = 0;
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back")){
					phase = 0;
				}
			}
			//Chose switch
			if (phase == 2){
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Menue")){
					Debug.Log ("Switch still needs to be implemented...");
					turn = 2;
					phase = 0;
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back")){
					phase = 0;
				}
			}
			//Chose run
			if(phase == 3){
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Run Away")){
					Application.LoadLevel("Scene1");
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back")){
					phase = 0;
				}
			}
		}
	}

	void EnemyTurn(){
		Debug.Log ("Enemy Turn Occured, waiting for AI...");
		turn = 1;
	}

	void SpawnEnemies(){
		for (int i=0; i<enemiesInBattle; i++) {
			int index = Random.Range(0,enemies.Length);
			GameObject temp = Instantiate(enemies[index]) as GameObject;
			fieldEnemies.Add(temp);
			fieldEnemies[i].transform.position = new Vector3(enemySpawns[i].transform.position.x,enemySpawns[i].transform.position.y,enemySpawns[i].transform.position.z);
			fieldEnemies[i].transform.tag = "Enemy"+i;
		}
	}

	void SpawnPlayers(){
		for (int i=0; i<players.Length; i++) {
			GameObject temp = Instantiate(players[i]) as GameObject;
			fieldPlayers.Add(temp);
			PlayerCharacter location = fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>();
			fieldPlayers[i].transform.position = new Vector3(playerSpawns[location.battlePosition].transform.position.x,enemySpawns[location.battlePosition].transform.position.y,enemySpawns[location.battlePosition].transform.position.z);
		}
	}


}
