/// <summary>
/// NPC.cs
/// Written by William George
/// 
/// Handels NPCs
/// Currently just dialouge
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class NPC : MonoBehaviour {

	//Used to hold dialouge tree
	string path;						//path to text file containing dialouge
	public string dilougeFileName;		//name of file containing dialouge
	public List<string> words;			//List of lines from the dialouge file
	int wordsIndex;						//Current index within words aka the index for the current line of dialouge from the file

	private bool proximity;				//checks if player is close to NPC

	public Texture npcPicture;			//Picture to use for NPC dialouge

	// Use this for initialization
	void Start () {
		path = Application.dataPath;	//gets the local path
		words = new List<string>();
		LoadDialouge();
	}
	
	// Update is called once per frame
	void Update () {
		//checks for key press to advance dialouge
		if(Input.GetKeyDown(KeyCode.Space)){
			if(wordsIndex < (words.Count - 1)){
				wordsIndex++;
			}
		}
	}

	//Checkes if player is near NPC labled object 
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			proximity = true;
			Debug.Log("count" + words.Count);
			Debug.Log("index" + wordsIndex);
		}

	}

	//Checks if player leaves NPC labeled object
	void OnTriggerExit(Collider other){
		proximity = false;
		wordsIndex = 0;
	}

	/// <summary>
	/// Load of the dialouge tree from file into the list
	/// </summary>
	public void LoadDialouge(){
		StreamReader reader = new StreamReader(path +"/"+ dilougeFileName); //opens dialouge file
		string s = reader.ReadLine();										//gets first line of dialouge
		//Reads through text file adding lines to the List one line at a time
		while (s != null) {
			words.Add(s);
			s = reader.ReadLine();
		}
		/*
		 * An alternate approach I tried...
		using (FileStream fs = File.OpenRead(path +"/"+ dilougeFileName)) {
			byte[] b = new byte[1024];
			UTF8Encoding temp = new UTF8Encoding(true);
			while (fs.Read(b,0,b.Length) > 0)
			{
				words.Add(temp.GetString(b));
			}
		}
		*/
	}

	/// <summary>
	/// Gets the proximity.
	/// </summary>
	/// <returns><c>true</c>, if proximity is true, <c>false</c> otherwise.</returns>
	public bool GetProximity(){
		return proximity;
	}

	/// <summary>
	/// Gets the current dialouge line.
	/// </summary>
	/// <returns>The words at the current index.</returns>
	public string GetWords(){
		return words[wordsIndex];
	}
}
