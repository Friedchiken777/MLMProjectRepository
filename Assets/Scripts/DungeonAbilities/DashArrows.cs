using UnityEngine;
using System.Collections;

public class DashArrows : MonoBehaviour {

	public bool isFire, isIce, isEarth, isElectric;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag != "Player") {
			rigidbody.velocity = Vector3.zero;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public bool GetIsFire(){return isFire;}
	public bool GetIsIce() {return isIce;}
	public bool GetIsEarth() {return isEarth;}
	public bool GetIsElectric() {return isElectric;}
}
