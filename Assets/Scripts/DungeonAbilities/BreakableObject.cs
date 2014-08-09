using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {

	public bool breakDiscovered;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GetBreakDiscovered(){
		return breakDiscovered;
	}

	public void SetBreakDiscovered(bool t){
		breakDiscovered = t;
	}
}
