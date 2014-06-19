/// <summary>
/// GameSettings.cs
/// William George
/// Will eventually handel saving and loading
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveData(){
		//PlayerPrefs.SetString ("Player Name", );
	}

	public void LoadData(){

	}
}
