﻿using UnityEngine;
using System.Collections;
using System;				//easy access to enum class

public class BaseCharacter : MonoBehaviour {
	private string name;
	private int level;
	private uint expToSpend;

	private Attribute[] primaryAttribute;
	private Vital[] vital;
	private Skill[] skill;

	public void Awake(){
		name = string.Empty;
		level = 0;
		expToSpend = 0;

		primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

	public string Name{
		get {return name;}
		set {name = value;}
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
	}

	private void SetupSkills(){
		for (int x = 0; x < skill.Length; x++) {
			skill[x] = new Skill();
		}
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
		GetVital ((int)VitalName.HP).AddModifier (new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Physical_Defense), ratio = 0.5f});
		GetVital ((int)VitalName.MP).AddModifier (new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Magic), ratio = 0.5f});
	}

	private void SetupSkillModifiers(){

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