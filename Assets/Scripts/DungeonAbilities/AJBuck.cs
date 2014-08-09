using UnityEngine;
using System.Collections;

public class AJBuck : MonoBehaviour {

	public GameObject player, targeting;

	public LayerMask buckableObjects;

	float range = 3.0f;
	float effectRadius = 1.2f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawLine (player.transform.position, targeting.transform.position, Color.red);
	}

	public void Buck(){
		Collider[] hitObjects = Physics.OverlapSphere (targeting.transform.position, effectRadius, buckableObjects);

		foreach(Collider hit in hitObjects){
			Debug.Log (hit.transform.gameObject.name);
			if(hit.transform.gameObject.GetComponent<BreakableObject>() != null){
				if(hit.transform.gameObject.GetComponent<BreakableObject>().GetBreakDiscovered()){
					Destroy(hit.transform.gameObject);
				}
			}
		}

		/*RaycastHit hit = new RaycastHit();
		Physics.Raycast (player.transform.position, targeting.transform.position, out hit, range);
		if (hit.collider != null) {
			Debug.Log (hit.transform.gameObject.name);
			if(hit.transform.gameObject.GetComponent<BreakableObject>() != null){
				if(hit.transform.gameObject.GetComponent<BreakableObject>().GetBreakDiscovered()){
					Destroy(hit.transform.gameObject);
				}
			}
		} 
		else {
			Debug.Log ("Miss!");
		}*/
	}

	public float GetRange()
	{
		return range;
	}
}
