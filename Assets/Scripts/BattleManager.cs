/// <summary>
/// BattleManager.cs
/// William George and Gordon Gu
/// Manages the events of a battle
/// Please don't kill me for the indents Will :(
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;			//Added to use lists

public class BattleManager : MonoBehaviour
{

    public GameObject[] enemies, 			//Array of possible enimies
                        players, 			//Array of characters [0]Twi, [1]Pinkie, [2] AJ, [3] Dash, [4] Rarity, [5]Flutters
                        enemySpawns, 		//Array of enemy spawn location on battlefield
                        playerSpawns; 		//Array of player spawn location on battlefield

    public List<GameObject> fieldEnemies, 	//List of enemies currently on battlefield
                            fieldPlayers,	//List of players currently on battlefield
                            switchPlayers,	//List to keep track of swiching before actually switching
                            turnOrder;		//List of turns in order with 0 being the current turn

	public List<Texture> turnPortraits;		//List of player turn portraits
    private List<Texture> switchPortraits;	  	//List of player switch portraits

    public int enemiesInBattle;				//Number of enemies participating in next battle

    int turn;								//turn=1=player turn; turn=2=enemy turn (needs to be expanded for both more players and enemies)
    int phase;								//phase=0=player action choose; phase=1=Attack choice; phase=2=Switch action; phase=3=Run action (also needs to be expanded)

    bool firstClick;						//used when switching to determine if first member of switch has been selected
    int switch1;							//used when switching to determine first member of switch

    // Use this for initialization
    void Start()
    {
        fieldEnemies = new List<GameObject>();
        fieldPlayers = new List<GameObject>();
        switchPlayers = new List<GameObject>();
		turnOrder = new List<GameObject>();
		turnPortraits = new List<Texture>();
        switchPortraits = new List<Texture>();
        SpawnEnemies();
        SpawnPlayers();
        CreateTurnList();
		SortTurn();
        turn = 1;
        phase = 0;
        firstClick = true;
        switch1 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (turn == 2)
        {
            EnemyTurn();
        }
    }

    void OnGUI()
	{
		DrawTurnOrder();

		//Players Turn
        if (turn == 1)
        {
            //Select action
            if (phase == 0)
            {
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Attack"))
                {
                    phase = 1;
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Switch"))
                {
                    phase = 2;
                    //refreshes player positions in temorary switch list
                    switchPlayers.Clear();
                    for (int i = 0; i < fieldPlayers.Count; i++)
                    {
                        switchPlayers.Add(fieldPlayers[i]);
                    }
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.5f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Run"))
                {
                    phase = 3;
                }
            }
            //Chose attack
            if (phase == 1)
            {
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Confirm"))
                {
                    Debug.Log("Player Attacked");
                    //turn = 2;
					EndTurn();
                    phase = 0;
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back"))
                {
                    phase = 0;
                }
            }
            //Chose switch
            if (phase == 2)
            {
                if (GUI.Button(new Rect((Screen.width * 0.50f), (Screen.height * 0.3f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[0]))
                {
                    CheckSwitch(0);
                }
                if (GUI.Button(new Rect((Screen.width * 0.50f), (Screen.height * 0.4f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[1]))
                {
                    CheckSwitch(1);
                }
                if (GUI.Button(new Rect((Screen.width * 0.50f), (Screen.height * 0.5f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[2]))
                {
                    CheckSwitch(2);
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[3]))
                {
                    CheckSwitch(3);
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[4]))
                {
                    CheckSwitch(4);
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.5f), (Screen.width * 0.25f), (Screen.height * 0.1f)), switchPortraits[5]))
                {
                    CheckSwitch(5);
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.6f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Confirm"))
                {
                    turn = 2;
                    phase = 0;
                    SwitchPlayersConfirm();
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.7f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back"))
                {
                    phase = 0;
                    //Resets portrait positions if switch is cancelled 
                    switchPortraits.Clear();
                    for (int i = 0; i < fieldPlayers.Count; i++)
                    {
                        switchPortraits.Add(fieldPlayers[i].gameObject.GetComponent<PlayerCharacter>().battlePic);
                    }
                }
            }
            //Chose run
            if (phase == 3)
            {
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Run Away"))
                {
                    Application.LoadLevel("Scene1");
                }
                if (GUI.Button(new Rect((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Back"))
                {
                    phase = 0;
                }
            }
        }
    }

    /// <summary>
    /// Function for the enemey turn.
    /// </summary>
    void EnemyTurn()
    {
        Debug.Log("Enemy Turn Occured, waiting for AI...");
        turn = 1;
    }

    /// <summary>
    /// Spawns the enemies.
    /// </summary>
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesInBattle; i++)
        {
            int index = Random.Range(0, enemies.Length);
            GameObject temp = Instantiate(enemies[index]) as GameObject;
            fieldEnemies.Add(temp);
            fieldEnemies[i].transform.position = new Vector3(enemySpawns[i].transform.position.x, enemySpawns[i].transform.position.y, enemySpawns[i].transform.position.z);
            fieldEnemies[i].transform.tag = "Enemy" + i;
        }
    }

    /// <summary>
    /// Spawns the players.
    /// </summary>
    void SpawnPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject temp = Instantiate(players[i]) as GameObject;
            fieldPlayers.Add(temp);
            BaseEntity location = fieldPlayers[i].GetComponent<BaseEntity>();
            fieldPlayers[i].transform.position = new Vector3(playerSpawns[location.battlePosition].transform.position.x, playerSpawns[location.battlePosition].transform.position.y, playerSpawns[location.battlePosition].transform.position.z);
            switchPortraits.Add(temp.GetComponent<BaseEntity>().battlePic);
        }
        SortPlayers();

    }

    /// <summary>
    /// Calculates switch resultes durring switch selection.
    /// </summary>
    /// <param name="s1">s1 - position one of the switch.</param>
    /// <param name="s2">s2 - position two of the switch.</param>
    void SwithPlayers(int s1, int s2)
    {
        GameObject switchTemp1 = switchPlayers[s1];
        GameObject switchTemp2 = switchPlayers[s2];
        switchPlayers.RemoveAt(s2);
        switchPlayers.Insert(s2, switchTemp1);
        switchPortraits.RemoveAt(s2);
        switchPortraits.Insert(s2, switchTemp1.GetComponent<BaseEntity>().battlePic);
        switchPlayers.RemoveAt(s1);
        switchPlayers.Insert(s1, switchTemp2);
        switchPortraits.RemoveAt(s1);
        switchPortraits.Insert(s1, switchTemp2.GetComponent<BaseEntity>().battlePic);
    }

    /// <summary>
    /// Called to finalize a switch.
    /// </summary>
    void SwitchPlayersConfirm()
    {
        for (int i = 0; i < switchPlayers.Count; i++)
        {
            fieldPlayers.RemoveAt(i);
            fieldPlayers.Insert(i, switchPlayers[i]);
            fieldPlayers[i].GetComponent<BaseEntity>().battlePosition = i;
            BaseEntity location = fieldPlayers[i].GetComponent<BaseEntity>();
            fieldPlayers[i].transform.position = new Vector3(playerSpawns[location.battlePosition].transform.position.x, playerSpawns[location.battlePosition].transform.position.y, playerSpawns[location.battlePosition].transform.position.z);
        }

    }

    /// <summary>
    /// Sorts the Fieldplayers by battle position.
    /// </summary>
    void SortPlayers()
    {
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < fieldPlayers.Count; i++)
        {
            tempList.Add(fieldPlayers[i]);
        }
        for (int i = 0; i < fieldPlayers.Count; i++)
        {
            int pos = tempList[i].GetComponent<BaseEntity>().battlePosition;
            GameObject player = tempList[i];
            fieldPlayers.RemoveAt(pos);
            fieldPlayers.Insert(pos, player);
            switchPortraits.RemoveAt(pos);
            switchPortraits.Insert(pos, player.GetComponent<BaseEntity>().battlePic);
        }

    }

    /// <summary>
    /// Helper function for switching.
    /// Calls SwitchPlayers once two players have been selected.
    /// </summary>
    /// <param name="s">S.</param>
    void CheckSwitch(int s)
    {
        if (firstClick)
        {
            switch1 = s;
            firstClick = false;
        }
        else
        {
            SwithPlayers(switch1, s);
            firstClick = true;
        }
    }

    void CreateTurnList()
    {
        if (turnOrder.Count < (fieldEnemies.Count + fieldPlayers.Count))
        {
			turnOrder.AddRange(fieldPlayers);
            turnOrder.AddRange(fieldEnemies);
        }
    }

    /// <summary>
    /// Sorts the sequence in which turns take place for enemies and the player characters.
    /// </summary>
    void SortTurn()
    {
        GameObject tempStorage;

        for (int i = 1; i < turnOrder.Count; i++)
        {
            int j = i;
            int a;
            int b;

            while (j > 1)
            {
                a = turnOrder[j - 1].GetComponent<BaseEntity>().bSpeed;
                b = turnOrder[j].GetComponent<BaseEntity>().bSpeed;

                if (a < b)
                {
                    tempStorage = turnOrder[j - 1];
                    turnOrder[j - 1] = turnOrder[j];
                    turnOrder[j] = tempStorage;
                    j--;
                }
                else
                {
                    break;
                }
			}
		}

		for (int i = 1; i < turnOrder.Count; i++)
		{
			turnPortraits.Add(turnOrder[i].GetComponent<BaseEntity>().battlePic);
		}
	}

    void EndTurn()
    {
        GameObject elementToRemove;
		Texture elementToRemoveTexture;

		elementToRemove = turnOrder[0];
		elementToRemoveTexture = turnOrder[0].GetComponent<BaseEntity>().battlePic;
		turnOrder.RemoveAt(0);
		turnPortraits.RemoveAt(0);
		turnOrder.Insert(turnOrder.Count - 1, elementToRemove);
		turnPortraits.Insert(turnPortraits.Count - 1, elementToRemoveTexture);

        if (turnOrder[0].CompareTag("Player"))
        {
            
            turn = 1;
        }
        else
        {
            turn = 2;
        }
    }

	void DrawTurnOrder()
	{
		for (int i = 0; i < turnOrder.Count - 1; i++)
		{
			GUI.DrawTexture(new Rect(0, turnPortraits[i].height * i / 4, turnPortraits[i].width / 4, turnPortraits[i].height / 4), turnPortraits[i]);
		}
	}

    IEnumerator Pause(int timeInSeconds)
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(timeInSeconds);
        Time.timeScale = 1;
    }
}
