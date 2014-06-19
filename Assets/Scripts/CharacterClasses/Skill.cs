/// <summary>
/// Skill.cs
/// William George
/// Contains skill specific methods
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
public class Skill : ModifiedStat {

	private bool known;		//a bollean variable to toggle known skills

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill(){
		known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value><c>true</c> if known; otherwise, <c>false</c>.</value>
	public bool Known{
		get{return known;}
		set{ known = value;}
	}
}

/// <summary>
/// List of Skill that can be learned
/// </summary>
public enum SkillName{
	Healing,
	Ailment_Resist,
	Dodge,
	Initiative,
	Hit_Chance,
	Crit_Chance,
	MP_Recover,
	Melee_Attack,
	Melee_Defense,
	Fire_Attack,
	Fire_Defense,
	Lightning_Attack,
	Lightning_Defense,
	Ice_Attack,
	Ice_Defense,
	Earth_Attack,
	Earth_Defense
}
	