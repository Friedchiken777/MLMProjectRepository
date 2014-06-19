/// <summary>
/// Attribute.cs
/// William George
/// This is the class for all of the character attributes in game
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
public class Attribute : BaseStat{

	new public const int STARTING_EXP_COST = 50;	//the starting cost for all attributes

	string name;									//the name of the attribute

	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute(){
		name = "";
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}

	/// <summary>
	/// Gets or sets the name (of attribute).
	/// </summary>
	/// <value>The name.</value>
	public string Name{
		get{return name;}
		set{name = value;}
	}

}

/// <summary>
/// List of all the attributes in the game
/// </summary>
public enum AttributeName{
	Power,
	Agility,
	Speed,
	Accuracy,
	Magic,
	Melee,
	Fire,
	Lightning,
	Ice,
	Earth,
	Health
}
