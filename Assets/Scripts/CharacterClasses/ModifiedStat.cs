/// <summary>
/// ModifiedStat.cs
/// William George
/// This is the base class for all stats that are modifiable by attributes
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
using System.Collections.Generic;			//Added to use lists

public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> mods;	//list of attribute that modify the stat
	private int modValue;					//the amount added to baseValue

	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat(){
		mods = new List<ModifyingAttribute> ();
		modValue = 0;
	}

	/// <summary>
	/// Adds a moddifying attribute to list of mods for this modified stat
	/// </summary>
	/// <param name="mod">Mod.</param>
	public void AddModifier (ModifyingAttribute mod){
		mods.Add (mod);
	}

	/// <summary>
	/// Resets mod value and then checks for moddifying attribute in mods list
	/// If moddifying attributes exist, list is iterated through and multiplies
	/// attribute by the ratio.
	/// </summary>
	private void CalculateModValue(){
		modValue = 0;

		if (mods.Count > 0) {
			foreach(ModifyingAttribute att in mods){
				modValue += att.attribute.AdjustedBaseValue * (int)att.ratio;
			}
		}
	}

	/// <summary>
	/// This function is overwriting the AdjustedBaseValue in BaseStat class
	/// Recalculate the AdjustedBaseValue from the BaseValue + BuffValue + modValue
	/// </summary>
	/// <value>The adjusted base value.</value>
	public new int AdjustedBaseValue {
		get { return BaseValue + BuffValue + modValue;}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update(){
		CalculateModValue ();
	}

}

/// <summary>
/// A structure to hold the attribute and ratio to be modified as a ModifiedStat
/// </summary>
public struct ModifyingAttribute {
	public Attribute attribute;		//the attribute to be used as a modifier
	public float ratio;				// the percent of the attriburtes AdjustedBaseValue to be modified by the ModifiedStat

	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name="att">Att. the attribute to be used</param>
	/// <param name="rat">Rat. the ratio to use</param>
	public ModifyingAttribute(Attribute att, float rat){
		attribute = att;
		ratio = rat;
	}
}
