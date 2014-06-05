/// <summary>
/// Vital.cs
/// William Geroge
/// Class contatins all the character vital specific functions
/// It has been taken from a tutorial by BurgZerg Arcade
/// </summary>
public class Vital : ModifiedStat {
	private int currValue;			//current value of the vital

	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital(){
		currValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}

	/// <summary>
	/// When getting the currVale, checks that it is not greater than the AdjustedBaseValue
	/// If it exceeds, make it equal to the AdjustedBaseValue
	/// </summary>
	/// <value>The curr value.</value>
	public int CurrValue {
		get{
			if(currValue > AdjustedBaseValue){
				currValue = AdjustedBaseValue;
			}
			return currValue;
		}
		set{ currValue = value;}
	}

}

/// <summary>
/// Liat of Vitals
/// </summary>
public enum VitalName{
	HP,
	MP
}
