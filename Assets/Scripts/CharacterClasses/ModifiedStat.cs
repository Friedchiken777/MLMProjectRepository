using System.Collections.Generic;

public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> mods;	//list of attribute that modify the stat
	private int modValue;

	public ModifiedStat(){
		mods = new List<ModifyingAttribute> ();
		modValue = 0;
	}

	public void AddModifier (ModifyingAttribute mod){
		mods.Add (mod);
	}

	private void CalculateModValue(){
		modValue = 0;

		if (mods.Count > 0) {
			foreach(ModifyingAttribute att in mods){
				modValue += att.attribute.AdjustedBaseValue * (int)att.ratio;
			}
		}
	}

	public new int AdjustedBaseValue {
		get { return BaseValue + BuffValue + modValue;}
	}

	public void Update(){
		CalculateModValue ();
	}

}

public struct ModifyingAttribute {
	public Attribute attribute;
	public float ratio;
}
