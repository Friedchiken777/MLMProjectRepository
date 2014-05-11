public class BaseStat {
	private int baseValue;			//base value of stat
	private int buffValue;			//amount stat is buffed
	private int expToLevel;			//total experience to next level
	private float levelModifier;	//cost for next level

	public BaseStat(){
		baseValue = 0;
		buffValue = 0;
		levelModifier = 1.1f;
		expToLevel = 100;
	}

#region Basic Setters and Getters
	public int BaseValue{
		get{ return baseValue;}
		set{ baseValue = value; }
	}

	public int BuffValue{
		get{ return buffValue;}
		set{ buffValue = value; }
	}

	public int ExpToLevel{
		get{ return expToLevel;}
		set{ expToLevel = value; }
	}

	public float LevelModifier{
		get{ return levelModifier;}
		set{ levelModifier = value; }
	}
#endregion

	private int CalculateExpToLevel(){
		return expToLevel * (int)levelModifier;
	}

	public void LevelUp(){
		expToLevel = CalculateExpToLevel ();
		baseValue++;
	}

	public int AdjustedBaseValue{
		get { return baseValue + buffValue;}
	}
}
