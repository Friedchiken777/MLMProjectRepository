using UnityEngine;
using System.Collections;
using System.Collections.Generic;			//Added to use lists

public class BattleManager : MonoBehaviour {

	public GameObject[] enemies, 			//Array of possible enimies
						players, 			//Array of characters [0]Twi, [1]Pinkie, [2] AJ, [3] Dash, [4] Rarity, [5]Flutters
						enemySpawns, 		//Array of enemy spawn location on battlefield
						playerSpawns; 		//Array of player spawn location on battlefield

	public List<GameObject> fieldEnemies, 	//List of enemies currently on battlefield
							fieldPlayers,	 //List of players currently on battlefield
							switchPlayers;	//List to keep track of swiching before actually switching

	private List<Texture> portraits;	  	//List of player portraits
	
	public int enemiesInBattle;				//Number of enemies participating in next battle

	int turn;								//turn=1=player turn; turn=2=enemy turn (needs to be expanded for both more players and enemies)
	int phase;								//phase=0=player action choose; phase=1=Attack choice; phase=2=Switch action; phase=3=Run action (also needs to be expanded)

	bool firstClick;						//used when switching to determine if first member of switch has been selected
	int switch1;							//used when switching to determine first member of switch
	
	// Use this for initialization
	void Start () {	
		fieldEnemies = new List<GameObject>();
		fieldPlayers = new List<GameObject>();
		switchPlayers = new List<GameObject>();
		portraits = new List<Texture>();
		SpawnEnemies ();
		SpawnPlayers ();
		turn = 1;
		phase = 0;
		firstClick = true;
		switch1 = 0;
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
					//refreshes player positions in temorary switch list
					switchPlayers.Clear ();
					for (int i=0; i<fieldPlayers.Count; i++) {
						switchPlayers.Add(fieldPlayers[i]);
					}
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
				if(GUI.Button(new Rect ((Screen.width * 0.50f), (Screen.height * 0.3f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[0])){
					CheckSwitch(0);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.50f), (Screen.height * 0.4f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[1])){
					CheckSwitch(1);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.50f), (Screen.height * 0.5f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[2])){
					CheckSwitch(2);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[3])){
					CheckSwitch(3);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[4])){
					CheckSwitch(4);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.5f), (Screen.width * 0.25f), (Screen.height * 0.1f)), portraits[5])){
					CheckSwitch(5);
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.6f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Confirm")){
					turn = 2;
					phase = 0;
					SwitchPlayersConfirm();
				}
				if(GUI.Button(new Rect ((Screen.width * 0.25f), (Screen.height * 0.7f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back")){
					phase = 0;
					//Resets portrait positions if switch is cancelled 
					portraits.Clear ();
					for (int i=0; i<fieldPlayers.Count; i++) {
						portraits.Add(fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>().battlePic);
					}
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

	/// <summary>
	/// Function for the enemey turn.
	/// </summary>
	void EnemyTurn(){
		Debug.Log ("Enemy Turn Occured, waiting for AI...");
		turn = 1;
	}

	/// <summary>
	/// Spawns the enemies.
	/// </summary>
	void SpawnEnemies(){
		for (int i=0; i<enemiesInBattle; i++) {
			int index = Random.Range(0,enemies.Length);
			GameObject temp = Instantiate(enemies[index]) as GameObject;
			fieldEnemies.Add(temp);
			fieldEnemies[i].transform.position = new Vector3(enemySpawns[i].transform.position.x,enemySpawns[i].transform.position.y,enemySpawns[i].transform.position.z);
			fieldEnemies[i].transform.tag = "Enemy"+i;
		}
	}

	/// <summary>
	/// Spawns the players.
	/// </summary>
	void SpawnPlayers(){
		for (int i=0; i<players.Length; i++) {
			GameObject temp = Instantiate(players[i]) as GameObject;
			fieldPlayers.Add(temp);
			PlayerCharacter location = fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>();
			fieldPlayers[i].transform.position = new Vector3(playerSpawns[location.battlePosition].transform.position.x,playerSpawns[location.battlePosition].transform.position.y,playerSpawns[location.battlePosition].transform.position.z);
			portraits.Add(temp.gameObject.GetComponent<PlayerCharacter>().battlePic);
		}
		SortPlayers ();

	}

	/// <summary>
	/// Calculates switch resultes durring switch selection.
	/// </summary>
	/// <param name="s1">s1 - position one of the switch.</param>
	/// <param name="s2">s2 - position two of the switch.</param>
	void SwithPlayers(int s1, int s2){
		GameObject switchTemp1 = switchPlayers [s1];
		GameObject switchTemp2 = switchPlayers [s2];
		switchPlayers.RemoveAt(s2);
		switchPlayers.Insert(s2, switchTemp1);
		portraits.RemoveAt(s2);
		portraits.Insert(s2, switchTemp1.GetComponent<PlayerCharacter>().battlePic);
		switchPlayers.RemoveAt(s1);
		switchPlayers.Insert(s1, switchTemp2);
		portraits.RemoveAt(s1);
		portraits.Insert(s1, switchTemp2.GetComponent<PlayerCharacter>().battlePic);		
	}

	/// <summary>
	/// Called to finalize a switch.
	/// </summary>
	void SwitchPlayersConfirm(){
		for (int i=0; i<switchPlayers.Count; i++) {
			fieldPlayers.RemoveAt(i);
			fieldPlayers.Insert(i, switchPlayers[i]);
			fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>().battlePosition = i;
			PlayerCharacter location = fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>();
			fieldPlayers[i].transform.position = new Vector3(playerSpawns[location.battlePosition].transform.position.x,playerSpawns[location.battlePosition].transform.position.y,playerSpawns[location.battlePosition].transform.position.z);
		}

	}

	/// <summary>
	/// Sorts the Fieldplayers by battle position.
	/// </summary>
	void SortPlayers(){
		List<GameObject> tempList = new List<GameObject>();
		for (int i=0; i<fieldPlayers.Count; i++) {
			tempList.Add(fieldPlayers[i]);
		}
		for (int i=0; i<fieldPlayers.Count; i++) {
			int pos = tempList[i].gameObject.GetComponent<PlayerCharacter>().battlePosition;
			GameObject player = tempList[i];
			fieldPlayers.RemoveAt(pos);
			fieldPlayers.Insert(pos, player);
			portraits.RemoveAt(pos);
			portraits.Insert(pos, player.GetComponent<PlayerCharacter>().battlePic);
		}

	}

	/// <summary>
	/// Helper function for switching.
	/// Calls SwitchPlayers once two players have been selected.
	/// </summary>
	/// <param name="s">S.</param>
	void CheckSwitch(int s){
		if(firstClick){
			switch1 = s;
			firstClick = false;
		}else{
			SwithPlayers(switch1, s);
			firstClick = true;
		}
	}
	
	
}
