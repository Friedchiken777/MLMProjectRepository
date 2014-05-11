public class Vital : ModifiedStat {
	private int currValue;

	public Vital(){
		currValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}

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

public enum VitalName{
	HP,
	MP
}
