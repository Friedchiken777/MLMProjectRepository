/// <summary>
/// BaseCharacter.cs
/// William George
/// Stuff every character will need
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
using UnityEngine;
using System.Collections;
using System;				//easy access to enum class

public class BaseEntity : MonoBehaviour {
	public string charName;
	private int level;
	private uint expToSpend;
	
	public Texture battlePic;
	
	public Attribute[] primaryAttribute;
	public Vital[] vital;
	private Skill[] skill;
	
	//Variables for each base attribute that can be set in Unity
	//Using the functionality from the set up hierchy, these'll also set stuff like health and skills
	public int 	bPower,
	bAgility,
	bSpeed,
	bAccuracy,
	bMagic,
	bMelee,
	bFire,
	bLightning,
	bIce,
	bEarth,
	bHealth;

	public int battlePosition;
	
	public virtual void Awake(){
		//charName = string.Empty;
		level = 0;
		expToSpend = 0;
		
		primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
		
		//Set all the stats based on values set in Unity
		//Eventually we'll have to do this from a file to acomadate saving, but for now, this'll do
		this.GetPrimaryAttribute(0).BaseValue = bPower;
		this.GetPrimaryAttribute(1).BaseValue = bAgility;
		this.GetPrimaryAttribute(2).BaseValue = bSpeed;
		this.GetPrimaryAttribute(3).BaseValue = bAccuracy;
		this.GetPrimaryAttribute(4).BaseValue = bMagic;
		this.GetPrimaryAttribute(5).BaseValue = bMelee;
		this.GetPrimaryAttribute(6).BaseValue = bFire;
		this.GetPrimaryAttribute(7).BaseValue = bLightning;
		this.GetPrimaryAttribute(8).BaseValue = bIce;
		this.GetPrimaryAttribute(9).BaseValue = bEarth;
		this.GetPrimaryAttribute(10).BaseValue = bHealth;
	}
	
	public string CharName{
		get {return charName;}
		set {charName = value;}
	}
	
	public int Level{
		get{return level;}
		set{level = value;}
	}
	
	public void AddExp(uint exp){
		expToSpend += exp;
		CalculateLevel ();
	}
	
	//tutorial wants to add something here...
	public void CalculateLevel(){
		
	}
	
	public uint ExpToSpend{
		get{return expToSpend;}
		set{expToSpend = value;}
	}
	
	private void SetupPrimaryAttributes(){
		for (int x = 0; x < primaryAttribute.Length; x++) {
			primaryAttribute[x] = new Attribute();
		}
	}
	
	private void SetupVitals(){
		for (int x = 0; x < vital.Length; x++) {
			vital[x] = new Vital();
		}
		SetupVitalModifiers ();
	}
	
	private void SetupSkills(){
		for (int x = 0; x < skill.Length; x++) {
			skill[x] = new Skill();
		}
		SetupSkillModifiers ();
	}
	
	public Attribute GetPrimaryAttribute(int index){
		return primaryAttribute[index];
	}
	
	public Vital GetVital(int index){
		return vital[index];
	}
	
	public Skill GetSkill(int index){
		return skill[index];
	}
	
	/// <summary>
	/// Will need to be changed when actual Vital and Skills are determined...
	/// </summary>
	private void SetupVitalModifiers(){
		GetVital ((int)VitalName.HP).AddModifier (new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Health), 0.5f));
		GetVital ((int)VitalName.MP).AddModifier (new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Magic), 0.5f));
	}
	
	private void SetupSkillModifiers(){
		//Healing
		GetSkill ((int)SkillName.Healing).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 1.5f));
		//Ailment_Resist
		GetSkill ((int)SkillName.Ailment_Resist).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.5f));
		//Dodge
		GetSkill ((int)SkillName.Dodge).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Agility), 0.1f));
		//Initiative
		GetSkill ((int)SkillName.Initiative).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Speed), 0.5f));
		//Hit_Chance
		GetSkill ((int)SkillName.Hit_Chance).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Accuracy), 0.7f));
		//Crit_Chance
		GetSkill ((int)SkillName.Crit_Chance).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Accuracy), 0.1f));
		//MP_Recover
		GetSkill ((int)SkillName.MP_Recover).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Magic), 0.5f));
		//Melee_Attack
		GetSkill ((int)SkillName.Melee_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.3f));
		GetSkill ((int)SkillName.Melee_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Melee), 0.5f));
		//Melee_Defense
		GetSkill ((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Melee), 0.5f));
		//Fire_Attack
		GetSkill ((int)SkillName.Fire_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.3f));
		GetSkill ((int)SkillName.Fire_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Fire), 0.5f));
		//Fire_Defense
		GetSkill ((int)SkillName.Fire_Defense).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Fire), 0.5f));
		//Lightning_Attack
		GetSkill ((int)SkillName.Lightning_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.3f));
		GetSkill ((int)SkillName.Lightning_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Lightning), 0.5f));
		//Lightning_Defense
		GetSkill ((int)SkillName.Lightning_Defense).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Lightning), 0.5f));
		//Ice_Attack
		GetSkill ((int)SkillName.Ice_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.3f));
		GetSkill ((int)SkillName.Ice_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Ice), 0.5f));
		//Ice_Defense
		GetSkill ((int)SkillName.Ice_Defense).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Ice), 0.5f));
		//Earth_Attack
		GetSkill ((int)SkillName.Earth_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Power), 0.3f));
		GetSkill ((int)SkillName.Earth_Attack).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Earth), 0.5f));
		//Earth_Defense
		GetSkill ((int)SkillName.Earth_Defense).AddModifier(new ModifyingAttribute (GetPrimaryAttribute ((int)AttributeName.Earth), 0.5f));
	}
	
	public void StatUpdate(){
		for (int x = 0; x < vital.Length; x++) {
			vital[x].Update();
		}
		for (int x = 0; x < skill.Length; x++) {
			skill[x].Update();
		}
	}
}
