/// <summary>
/// GameManager.cs
/// Written by William George
/// 
/// Maages aspects of the scene such as the menu and GUI elements
/// </summary>
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public ControllerV3 controller;			//Reference to the players controller
	public NPC npc;							//Reference to the NPC in scene (May need to be a list later...)
	public Menu menu;						//Reference to the menu script
	public GameObject cam;					//reference to game camera;

	//string level;							//name of current scene

#region Variables for temporary health bar 
	//Health Texture
	public Texture playerHealthTex;
	public Texture lostHealthTex;
	//Health Texture Position
	public float screenPosX;
	public float screenPosY;
	//Icon Size
	public int iconSizeX = 10;
	public int iconSizey = 10;

	public int maxHealth = 10;				//Max player health
	public int currentHealth = 10;			//Current player health
#endregion
	
	private bool prox;						//Checks npc proximity

	//Main menu variables
	bool menuOpen;							//Checks if menu is currently open
	bool toggleMenu;						//Toggles Menu
	string menuSelection;					//Current item selected in menu

	// Use this for initialization
	void Start () {
		menuOpen = false;
		toggleMenu = false;
	}

	// Update is called once per frame
	void Update () {
		//checks for key press to open menu
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleMenu();
		}
		if (toggleMenu) {
			if (Input.GetKeyDown(KeyCode.Space)){
				ToggleMenu();
				toggleMenu = false;
			}
		}
	}

#region OnGUI Function
	/// <summary>
	/// Draws GUI items in scene.
	/// </summary>
	void OnGUI(){
		//Draw overworl Gui
		if (this.tag == "Overworld") {
			//Draws underlining red bar
			GUI.DrawTexture (new Rect (screenPosX, screenPosY, (iconSizeX * maxHealth), iconSizey), lostHealthTex, ScaleMode.StretchToFill, true, 0);
			//Draws green bar according to health
			for (int h = 0; h < currentHealth; h++) {
				GUI.DrawTexture (new Rect (screenPosX + (h * iconSizeX), screenPosY, iconSizeX, iconSizey), playerHealthTex, ScaleMode.ScaleToFit, true, 0);
			}
		//Draw Shop/NPC GUI
		} else if (this.tag == "Shop") {
			GUI.skin.box.wordWrap = true;
			GUI.skin.box.alignment = TextAnchor.UpperLeft;
			GUI.skin.box.fontSize = (int)(Screen.height * 0.07f);
			prox = npc.GetProximity ();
			if (prox) {
				GUI.Box (new Rect ((Screen.width * 0.17f), (Screen.height * 0.03f), (Screen.width * 0.80f), (Screen.height * 0.3f)), npc.GetWords ());
				GUI.Box (new Rect ((Screen.width * 0.01f), (Screen.height * 0.03f), (Screen.width * 0.15f), (Screen.height * 0.3f)), npc.npcPicture);
			}
		}
		//Draw Menu Gui
		if (menuOpen) {
			GUI.skin.box.fontSize = (int)(Screen.height * 0.07f);
			GUI.SetNextControlName ("Exit Menu");
			if (GUI.Button (new Rect ((Screen.width * 0.25f), (Screen.height * 0.3f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Exit Menu")) {
				Debug.Log ("Exit Menu");
				toggleMenu = true;
				ToggleMenu();
			}
			GUI.SetNextControlName ("Test1");
			if (GUI.Button (new Rect ((Screen.width * 0.25f), (Screen.height * 0.4f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Test1")) {
				Debug.Log ("Test1");
			}
			GUI.SetNextControlName ("Test2");
			if (GUI.Button (new Rect ((Screen.width * 0.25f), (Screen.height * 0.5f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Test2")) {
				Debug.Log ("Test2");
			}
			GUI.SetNextControlName ("Exit Game");
			if (GUI.Button (new Rect ((Screen.width * 0.25f), (Screen.height * 0.6f), (Screen.width * 0.5f), (Screen.height * 0.1f)), "Exit Game")) {
				Debug.Log ("Exit Game");
				Application.Quit();
			}
			GUI.FocusControl (menu.GetMenuOption ());
			//menuSelection = GUI.GetNameOfFocusedControl();
		}
	}
#endregion

	/// <summary>
	/// Temporary method to damage player when they hit enemy
	/// </summary>
	/// <param name="damage">damage - amount of health player loses from hitting enemy</param>
	void PlayerDamage( int damage){
		//If player had health, takes damage
		if (currentHealth > 0) {
			currentHealth -= damage;
		}
		//If player runs out of health scene is restarted
		if (currentHealth <= 0) {
			currentHealth = 0;
			RestartScene();
		}
	}

	void StartBattle(){
		
	}

	/// <summary>
	/// Toggles the menu.
	/// </summary>
	void ToggleMenu(){
		menuOpen = !menuOpen;
		/*if (!menuOpen) {
			//Application.LoadLevelAdditive ("Menu");
		}else if (menuOpen){
			//Destroy(GameObject.Find("MenuCamera"));
			//Destroy(GameObject.Find("MenuArt"));
		}*/
	}

	/// <summary>
	/// Getter for menuOpen variable
	/// </summary>
	/// <returns><c>true</c>, if menu is open menuOpen, <c>false</c> otherwise.</returns>
	public bool GetMenuOpen(){
		return menuOpen;
	}

	/// <summary>
	/// Saves the last level. Doesn't serve desired purpose yet...
	/// </summary>
	public void SaveLastLevel(){
		//level = Application.loadedLevelName;
	}

	/// <summary>
	/// Restarts the scene.
	/// </summary>
	void RestartScene(){
		Application.LoadLevel (Application.loadedLevel);
	}

}
