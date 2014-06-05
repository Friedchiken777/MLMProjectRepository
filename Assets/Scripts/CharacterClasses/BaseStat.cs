/// <summary>
/// BaseStat.cs
/// William George
/// 
/// This is the base class for stats in the game.
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
//using UnityEngine;

public class BaseStat {
	public const int STARTING_EXP_COST = 100;	//publicly accessable value for all base stats to start at
	private int baseValue;						//base value of stat
	private int buffValue;						//amount stat is buffed
	private int expToLevel;						//total experience to next level
	private float levelModifier;				//cost for next level

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat(){
		baseValue = 0;
		buffValue = 0;
		levelModifier = 1.1f;
		expToLevel = STARTING_EXP_COST;
	}

#region Basic Setters and Getters
	/// <summary>
	/// Gets or sets the baseValue.
	/// </summary>
	/// <value>The baseValue.</value>
	public int BaseValue{
		get{ return baseValue;}
		set{ baseValue = value; }
	}
	/// <summary>
	/// Gets or sets the buffValue.
	/// </summary>
	/// <value>The buffValue.</value>
	public int BuffValue{
	    get{ return buffValue;}
		set{ buffValue = value; }
	}
	/// <summary>
	/// Gets or sets the expToLevel.
	/// </summary>
	/// <value>The expToLevel.</value>
	public int ExpToLevel{
		get{ return expToLevel;}
		set{ expToLevel = value; }
	}
	/// <summary>
	/// Gets or sets the levelModifier.
	/// </summary>
	/// <value>The levelModifier.</value>
	public float LevelModifier{
		get{ return levelModifier;}
		set{ levelModifier = value; }
	}
#endregion

	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>The exp to level.</returns>
	private int CalculateExpToLevel(){
		return expToLevel * (int)levelModifier;
	}

	/// <summary>
	/// Assign the new value to expToLevel and then increase the baseValue by one.
	/// </summary>
	public void LevelUp(){
		expToLevel = CalculateExpToLevel ();
		baseValue++;
	}

	/// <summary>
	/// Recalculate the adjusted base value and return it
	/// </summary>
	/// <value>The adjusted base value.</value>
	public int AdjustedBaseValue{
		get { return baseValue + buffValue;}
	}
}
