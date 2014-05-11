/// <summary>
/// Menu.cs
/// Written by William George
/// 
/// Script handels functions for the menu
/// </summary>
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	string[] menuOptions = {"Exit Menu", "Test1", "Test2", "Exit Game"};	//Menu options
	int selectedIndex = 0;													//Index of currently highlighted menu item



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Moves selection of menu item with arrow keys
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			selectedIndex = menuSelection(menuOptions, selectedIndex, "up");
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			selectedIndex = menuSelection(menuOptions, selectedIndex, "down");
		}
	}

	/// <summary>
	/// Menus the selection.
	/// </summary>
	/// <returns>The index of the newly highlighted item.</returns>
	/// <param name="menuItems">menuItems - array of menu item names</param>
	/// <param name="selectedItem">selectedItem - index of highlighted item</param>
	/// <param name="direction">direction - direction to move in the menu</param>
	int menuSelection (string[] menuItems, int selectedItem, string direction) {		
		int length = menuItems.Length;
		if (direction == "up") {			
			if (selectedItem == 0) {				
				selectedItem = length - 1;				
			} else {				
				selectedItem -= 1;				
			}			
		}		
		if (direction == "down") {			
			if (selectedItem == length - 1) {				
				selectedItem = 0;				
			} else {				
				selectedItem += 1;				
			}			
		}
		return selectedItem;		
	}

	/// <summary>
	/// Gets the menu option currently highlighted.
	/// </summary>
	/// <returns>The highlighted menu option.</returns>
	public string GetMenuOption(){
		return menuOptions [selectedIndex];
	}


}
