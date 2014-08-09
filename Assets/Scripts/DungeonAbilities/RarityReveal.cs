using UnityEngine;
using System.Collections;

public class RarityReveal : MonoBehaviour {

	public float effectRadius;
	public LayerMask rarityShow, rarityHide;
	public float durration, delay;

	float cooldown;
	bool powerActive, rarityNotification;

	// Use this for initialization
	void Start () {
		effectRadius = 10.5f;
		powerActive = false;
		rarityNotification = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > cooldown + delay && rarityNotification){
			Debug.Log("Rarity Colldown Ended");
			rarityNotification = false;
		}
	}

	public void ShowSecrets(){
		if (Time.time > cooldown + delay) {
			powerActive = true;
			Collider[] makeVisable = Physics.OverlapSphere (transform.position, effectRadius, rarityShow);
			Collider[] makeInvisable = Physics.OverlapSphere (transform.position, effectRadius, rarityHide);
		
			foreach (Collider target in makeVisable) {
				if(target.gameObject.tag.Equals("Breakable")){
					target.renderer.material.color = Color.red;
					target.gameObject.GetComponent<BreakableObject>().SetBreakDiscovered(true);
				}else{
					Color c = target.gameObject.renderer.material.color;
					target.gameObject.renderer.material.color = new Vector4 (c.r, c.g, c.b, 0.2f);
				}
			}
			foreach (Collider target in makeInvisable) {
				Color c = target.gameObject.renderer.material.color;
				target.gameObject.renderer.material.color = new Vector4 (c.r, c.g, c.b, 0.2f);

			}

			StartCoroutine (HideSecrets (makeVisable, makeInvisable));
		}
	}

	IEnumerator HideSecrets(Collider[] invisable, Collider[] visable) {
		yield return new WaitForSeconds(durration);
		foreach (Collider target in invisable) {
			Color c = target.gameObject.renderer.material.color;
			target.gameObject.renderer.material.color = new Vector4(c.r, c.g, c.b, 0.0f);
			if(target.gameObject.tag.Equals("Breakable")){
				target.renderer.material.color = Color.white;
			}
		}
		foreach (Collider target in visable) {
			Color c = target.gameObject.renderer.material.color;
			target.gameObject.renderer.material.color = new Vector4 (c.r, c.g, c.b, 1.0f);
		}
		cooldown = Time.time;
		powerActive = false;
		rarityNotification = true;
	}

	public bool GetPowerActive(){return powerActive;}
}
