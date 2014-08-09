using UnityEngine;
using System.Collections;

public class HiddenGems : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color c = this.gameObject.renderer.material.color;
		this.gameObject.renderer.material.color = new Vector4(c.r, c.g, c.b, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
